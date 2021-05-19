using System.IO;

namespace Domain.Impl.Models
{
    public class DownloadFileModel
    {
        public FileModel FileModel { get; set; }
        public FileStream Stream { get; set; }
    }
}
