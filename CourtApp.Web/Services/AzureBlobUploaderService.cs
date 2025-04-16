using CourtApp.Application.Interfaces.Shared;
using System.IO;
using System.Threading.Tasks;

namespace CourtApp.Web.Services
{
    public class AzureBlobUploaderService : IDocumentUploadService
    {
        public Task<string> UploadCompressedFileAsync(Stream zipStream, string zipFileName, string documentType)
        {
            throw new System.NotImplementedException();
        }
    }
}
