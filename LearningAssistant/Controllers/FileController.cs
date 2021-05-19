using Domain.Impl.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LearningAssistant.Controllers
{
    [Route("api/[controller]s")]
    [ApiController]
    public class FileController : ControllerBase
    {
        private readonly IFileService _fileService;

        public FileController(IFileService fileService)
        {
            _fileService = fileService;
        }

        [HttpDelete]
        [Route("{fileId}")]
        public async Task<IActionResult> DeleteFile([FromRoute] int fileId)
        {
            var result = await _fileService.DeleteFile(fileId);
            if (result != null)
                return Ok();
            return BadRequest();
        }

        [HttpPost]
        public async Task<ActionResult<FileModel>> UploadFile(IFormFile uploadedFile, [FromQuery] FileModel fileModel)
        {
            var result = await _fileService.UploadFile(uploadedFile, fileModel);
            if (result != null)
                return Ok(result);
            return BadRequest();
        }

        [HttpGet]
        [Route("{fileId}")]
        public async Task<ActionResult<DownloadFileModel>> DownloadFile([FromRoute] int fileId)
        {
            var result = await _fileService.DownloadFile(fileId);
            if (result != null)
                return new FileStreamResult(result.Stream, "application/octet-stream") { FileDownloadName = result.FileModel.Name };
            return BadRequest();
        }

    }
}
