using Domain.Impl;
using Domain.Impl.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public interface IFileService
    {
        Task<FileModel> UploadFile(IFormFile uploadedFile, FileModel input);
        Task<DownloadFileModel> DownloadFile(int fileId);
        Task<int?> DeleteFile(int fileId);
    }
}
