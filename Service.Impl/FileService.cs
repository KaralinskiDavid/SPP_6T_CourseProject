using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Dao;
using Dao.Impl.DaoModels;
using Domain.Impl;
using Domain.Impl.Models;
using Dto;
using Microsoft.AspNetCore.Http;
using System.Linq;

namespace Service.Impl
{
    public class FileService : IFileService
    {
        private readonly ISpecialityFileDao<SpecialityFile> _fileDao;
        private readonly IMapper _mapper;
        public FileService(ISpecialityFileDao<SpecialityFile> filedao, IMapper mapper)
        {
            _fileDao = filedao;
            _mapper = mapper;
        }

        public async Task<DownloadFileModel> DownloadFile(int fileId)
        {
            if (fileId <= 0)
                return null;

            var file = await _fileDao.GetItemByIdAsync(fileId);
            if (file == null)
                return null;

            try
            {
                var path = string.Format(ConstantStrings.FileStructurePath, file.SpecialityFileSectionId);
                var directory = Path.Combine(Environment.CurrentDirectory, path);

                if (!File.Exists(directory + file.Name))
                    return null;

                return new DownloadFileModel()
                {
                    Stream = File.OpenRead(directory + file.Name),
                    FileModel = _mapper.Map<FileModel>(file)
                };
            }
            catch (Exception ex)
            {
                return null;
            }
        }


        public async Task<int?> DeleteFile(int fileId)
        {
            var file = await _fileDao.GetItemByIdAsync(fileId);

            if (file == null)
                return null;

            try
            {
                var path = string.Format(ConstantStrings.FileStructurePath, file.SpecialityFileSectionId);
                var directory = Path.Combine(Environment.CurrentDirectory, path);

                var result = await _fileDao.RemoveAsync(file);

                if (result > 0)
                {
                    if (File.Exists(directory + file.Name))
                        File.Delete(directory + file.Name);

                    return result;
                }
            }
            catch
            {
                return null;
            }

            return null;
        }

        public async Task<FileModel> UploadFile(IFormFile uploadedFile, FileModel input)
        {
            if (uploadedFile == null)
                return null;

            try
            {
                var resultCreateFileRecord = await _fileDao.CreateAsync(_mapper.Map<SpecialityFile>(input));

                if (resultCreateFileRecord > 0)
                {
                    var path = string.Format(ConstantStrings.FileStructurePath, input.SpecialityFileSectionId);
                    var directory = Path.Combine(Environment.CurrentDirectory, path);
                    input.Path = directory;

                    if (!Directory.Exists(directory))
                        Directory.CreateDirectory(directory);

                    await using (var fileStream = new FileStream(directory + input.Name, FileMode.CreateNew))
                    {
                        await uploadedFile.CopyToAsync(fileStream);
                    }
                }
                return _mapper.Map<FileModel>((await _fileDao.GetItemsAsync()).Where(f=>f.Name==input.Name && f.SpecialityFileSectionId==input.SpecialityFileSectionId));
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
