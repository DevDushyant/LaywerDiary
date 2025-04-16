using CourtApp.Application.Interfaces.Shared;
using CourtApp.Web.Models;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Drive.v3;
using Google.Apis.Services;
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
        private readonly DriveService _driveService;
        public GoogleDriveUploaderService(IOptions<UploadSettings> options)
        {
            this._settings = options.Value;
            var credential = GoogleCredential.FromFile(_settings.GoogleDrive.ServiceAccountKeyFilePath)
           .CreateScoped(DriveService.ScopeConstants.Drive);

            _driveService = new DriveService(new BaseClientService.Initializer
            {
                HttpClientInitializer = credential,
                ApplicationName = _settings.GoogleDrive.ApplicationName
            });
        }
        public async Task<string> UploadCompressedFileAsync(Stream zipStream, string zipFileName, string documentType)
        {
            var folderName = documentType switch
            {
                "Draft" => _settings.Folders["DraftDocuments"],
                "Order" => _settings.Folders["OrderDocuments"],
                "Profile" => _settings.Folders["ProfileImage"],
                _ => throw new ArgumentException("Invalid document type")
            };
            var subFolderId = await GetOrCreateFolderAsync(folderName, _settings.GoogleDrive.BaseFolderId);

            var fileMetadata = new Google.Apis.Drive.v3.Data.File
            {
                Name = zipFileName,
                Parents = new List<string> { subFolderId }
            };

            var request = _driveService.Files.Create(fileMetadata, zipStream, "application/zip");
            request.Fields = "id, webViewLink";

            await request.UploadAsync(CancellationToken.None);

            var uploadedFile = request.ResponseBody;

            return uploadedFile.WebViewLink;
        }
        private async Task<string> GetOrCreateFolderAsync(string folderName, string parentFolderId)
        {
            // Check if folder exists
            var listRequest = _driveService.Files.List();
            listRequest.Q = $"mimeType='application/vnd.google-apps.folder' and name='{folderName}' and '{parentFolderId}' in parents and trashed=false";
            listRequest.Fields = "files(id, name)";
            var result = await listRequest.ExecuteAsync();

            if (result.Files.Count > 0)
            {
                return result.Files[0].Id;
            }

            // Create folder
            var fileMetadata = new Google.Apis.Drive.v3.Data.File
            {
                Name = folderName,
                MimeType = "application/vnd.google-apps.folder",
                Parents = new List<string> { parentFolderId }
            };

            var request = _driveService.Files.Create(fileMetadata);
            request.Fields = "id";
            var folder = await request.ExecuteAsync();

            return folder.Id;
        }
    }
}
