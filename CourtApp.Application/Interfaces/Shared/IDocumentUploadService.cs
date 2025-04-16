using System.IO;
using System.Threading.Tasks;

namespace CourtApp.Application.Interfaces.Shared
{
    public interface IDocumentUploadService
    {
        Task<string> UploadCompressedFileAsync(Stream zipStream, string zipFileName, string documentType);
    }
}
