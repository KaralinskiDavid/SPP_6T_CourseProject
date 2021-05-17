using AutoMapper;
using Dao;
using Dao.Impl.DaoModels;
using Domain.Impl.Models.Request;
using Domain.Impl.Models.Response;
using Dto.Identity;
using Dto.Security;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace Service.Impl
{
    public class AuthService : IAuthService
    {
        private readonly JwtOptions _jwtOptions;
        private readonly UserManager<LearningAssistantUser> _userManager;
        private readonly IStudentDao<Student> _studentDao;
        private readonly IGroupDao<Group> _groupDao;
        private readonly IRefreshTokenDao<RefreshToken> _refreshTokenDao;
        private readonly IMapper _mapper;

        public AuthService(IOptions<JwtOptions> jwtOptions, UserManager<LearningAssistantUser> userManager, 
            IStudentDao<Student> studentDao, IGroupDao<Group> groupDao, IMapper mapper, IRefreshTokenDao<RefreshToken> refreshTokenDao)
        {
            _jwtOptions = jwtOptions.Value;
            _userManager = userManager;
            _mapper = mapper;
            _studentDao = studentDao;
            _groupDao = groupDao;
            _refreshTokenDao = refreshTokenDao;
        }

        public string GenerateJwtToken(LearningAssistantUser user)
        {
            var securityKey = _jwtOptions.GetSymmetricSecurityKey();
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>()
            {
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.GivenName, user.UserName)
            };

            var token = new JwtSecurityToken(
                _jwtOptions.Issuer,
                _jwtOptions.Audience,
                claims,
                notBefore: DateTime.UtcNow,
                expires: DateTime.UtcNow.AddSeconds(_jwtOptions.TokenLifeTime),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public async Task<PostLoginResponseModel> LoginAsync(PostLoginRequestModel request, string ip)
        {
            var user = await _userManager.FindByEmailAsync(request.Email);
            if (user != null)
            {
                if (await _userManager.CheckPasswordAsync(user, request.Password))
                {
                    var refresh = await CreateRefreshToken(request.Email, ip);
                    return new PostLoginResponseModel
                    {
                        Token = GenerateJwtToken(user),
                        Email = user.Email,
                        UserName = user.UserName,
                        Role = (await _userManager.GetRolesAsync(user))[0],
                        RefreshToken = refresh
                    };
                }
            }
            return null;
        }

        public async Task<bool> RegisterAsync(PostRegisterRequestModel request)
        {
            try
            {
                if (await _userManager.FindByEmailAsync(request.Email) != null)
                    return false;
                if (String.IsNullOrWhiteSpace(request.RoleName))
                    request.RoleName = "Student";
                var model = _mapper.Map<LearningAssistantUser>(request);
                model.UserName = model.Email;
                var registrationResult = await _userManager.CreateAsync(model, request.Password);
                if (registrationResult.Succeeded)
                    await _userManager.AddToRoleAsync(await _userManager.FindByEmailAsync(request.Email), request.RoleName);
                if (!request.RoleName.Equals("Admin", StringComparison.OrdinalIgnoreCase))
                    await _studentDao.CreateAsync(new Student
                    {
                        GroupId = (await _groupDao.GetGroupByNumber(request.GroupNumber)).Id,
                        UserId = (await _userManager.FindByEmailAsync(request.Email)).Id
                    });
                return registrationResult.Succeeded;
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        public async Task<string> CreateRefreshToken(string email, string ipAddress)
        {
            var now = DateTime.UtcNow;
            string token ="";

            var randomNumber = new byte[32];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomNumber);
                token = Convert.ToBase64String(randomNumber);
            }

            var refreshToken = new RefreshToken()
            {
                IpAddress = ipAddress,
                Key = email,
                RefreshTokenValue = token,
                ExpirationDate = now.AddHours(_jwtOptions.RefreshTokenLifeTime)
            };

            var resultRemoveOldTokens = await _refreshTokenDao.RemoveOldTokens(email);

            var createTokenResult = await _refreshTokenDao.CreateAsync(refreshToken);

            if (createTokenResult == 1)
            {
                return refreshToken.RefreshTokenValue;
            }

            return null;
        }

        public async Task<RefreshToken> GetRefreshToken(string refreshToken, string ipAddress)
        {
            return await _refreshTokenDao.GetItemByToken(refreshToken, ipAddress);
        }

        public async Task<PostLoginResponseModel> RefreshToken(string refreshToken, string ip)
        {
            var token = await GetRefreshToken(refreshToken, ip);
            if (token == null)
                return null;

            var user = await _userManager.FindByEmailAsync(token.Key);
            var jwt = GenerateJwtToken(user);
            var refresh = await CreateRefreshToken(user.Email, ip);

            return new PostLoginResponseModel
            {
                Token = jwt,
                Email = user.Email,
                UserName = user.UserName,
                Role = (await _userManager.GetRolesAsync(user))[0],
                RefreshToken = refresh
            };

        }

        public async Task<bool> CheckUserEmail(string userEmail) => (await _userManager.FindByEmailAsync(userEmail)) != null;
    }
}
