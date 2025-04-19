using CourtApp.Application.Interfaces.Shared;
using CourtApp.Web.Models;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Drive.v3;
using Google.Apis.Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace CourtApp.Web.Services
{
    public class GoogleDriveUploaderService : IDocumentUploadService
    {
        private readonly UploadSettings _settings;
        private readonly IWebHostEnvironment _environment;
        private DriveService _driveService;

        public GoogleDriveUploaderService(IOptions<UploadSettings> options,
            IWebHostEnvironment environment)
        {
            _settings = options.Value;
            _environment = environment;
            _driveService = CreateDriveService();
        }

        private DriveService CreateDriveService()
        {
            var servicePath = Path.Combine(_environment.WebRootPath, "service-account-key.json");
            GoogleCredential credentials;

            using (var stream = new FileStream(servicePath, FileMode.Open, FileAccess.Read))
            {
                credentials = GoogleCredential.FromStream(stream);
            }

            if (credentials.IsCreateScopedRequired)
            {
                credentials = credentials.CreateScoped(new[]
                {
                    DriveService.Scope.Drive // ✅ Full Drive access
                });
            }

            return new DriveService(new BaseClientService.Initializer
            {
                HttpClientInitializer = credentials,
                ApplicationName = "LawyerDiaryUpload",
                HttpClientFactory = new CustomHttpClientFactory() // ✅ Apply custom factory
            });
        }

        public async Task<string> UploadFileAsync(Stream zipStream, string zipFileName, string documentType)
        {
            var folderName = documentType switch
            {
                "Draft" => _settings.Folders["DraftDocuments"],
                "Order" => _settings.Folders["OrderDocuments"],
                "Profile" => _settings.Folders["ProfileImage"],
                _ => throw new ArgumentException("Invalid document type")
            };

            var subFolderId = await GetOrCreateFolderAsync(folderName, _settings.GoogleDrive.BaseFolderId);
            if (subFolderId == null) throw new Exception("Failed to access or create target folder.");

            var fileMetadata = new Google.Apis.Drive.v3.Data.File
            {
                Name = zipFileName,
                Parents = new List<string> { subFolderId }
            };

            string contentType = documentType == "ProfileImage"
                ? GetContentType(zipFileName)
                : "application/zip";

            var request = _driveService.Files.Create(fileMetadata, zipStream, contentType);
            request.Fields = "id, webViewLink";

            using var cts = new CancellationTokenSource(TimeSpan.FromMinutes(10));
            await request.UploadAsync(cts.Token);

            return request.ResponseBody.WebViewLink;
        }

        private async Task<string> GetOrCreateFolderAsync(string folderName, string parentFolderId)
        {
            try
            {
                // ✅ Short timeout for metadata check
                //using var cts = new CancellationTokenSource(TimeSpan.FromSeconds(10));
                var parent = await _driveService.Files.Get(parentFolderId).ExecuteAsync();

                if (parent == null || parent.MimeType != "application/vnd.google-apps.folder")
                    throw new Exception("Invalid or inaccessible parent folder.");

                var listRequest = _driveService.Files.List();
                listRequest.Q = $"mimeType='application/vnd.google-apps.folder' and name='{folderName}' and '{parentFolderId}' in parents and trashed=false";
                listRequest.Fields = "files(id)";
                listRequest.Spaces = "drive";

                var result = await listRequest.ExecuteAsync();

                if (result.Files.Count > 0)
                    return result.Files[0].Id;

                // Create new folder if not found
                var fileMetadata = new Google.Apis.Drive.v3.Data.File
                {
                    Name = folderName,
                    MimeType = "application/vnd.google-apps.folder",
                    Parents = new List<string> { parentFolderId }
                };

                var createRequest = _driveService.Files.Create(fileMetadata);
                createRequest.Fields = "id";
                var newFolder = await createRequest.ExecuteAsync();

                return newFolder.Id;
            }
            catch (Google.GoogleApiException gex)
            {
                Console.WriteLine($"Google API error: {gex.Message}");
                throw;
            }
            catch (TaskCanceledException)
            {
                Console.WriteLine("Folder existence check timed out.");
                throw;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Unexpected error: {ex.Message}");
                throw;
            }
        }

        private string GetContentType(string fileName)
        {
            var extension = Path.GetExtension(fileName).ToLowerInvariant();
            return extension switch
            {
                ".jpg" or ".jpeg" => "image/jpeg",
                ".png" => "image/png",
                ".gif" => "image/gif",
                _ => "application/octet-stream"
            };
        }

        public async Task<bool> DeleteFileAsync(string fileId)
        {
            try
            {
                var deleteRequest = _driveService.Files.Delete(fileId);
                await deleteRequest.ExecuteAsync();
                return true;
            }
            catch (Google.GoogleApiException gex)
            {
                Console.WriteLine($"Google API error during delete: {gex.Message}");
                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Unexpected error during delete: {ex.Message}");
                return false;
            }
        }
    }


}
