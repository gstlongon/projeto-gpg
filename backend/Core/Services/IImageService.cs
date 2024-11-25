using Core.Models;

namespace Core.Services
{
    public interface IImageService
    {
        public Task<string> UploadImage(FileData file, string folderName, string fileName);
    }
}