using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Dto.Security
{
    public class JwtOptions
    {
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public string Secret { get; set; }
        public int TokenLifeTime { get; set; }
        public int RefreshTokenLifeTime { get; set; }

        public SymmetricSecurityKey GetSymmetricSecurityKey() => new  SymmetricSecurityKey(Encoding.ASCII.GetBytes(Secret));
    }
}
