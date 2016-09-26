using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

using Google.Apis.Services;
using Google.Apis.Storage.v1;
using Google.Apis.Download;
using System.Net.Http;

namespace Namesearch
{
    public class StorageSample
    {
        // [START create_storage_client]
        public static StorageService CreateStorageClient()
        {
            var credentials = Google.Apis.Auth.OAuth2.GoogleCredential.GetApplicationDefaultAsync().Result;

            if (credentials.IsCreateScopedRequired)
            {
                credentials = credentials.CreateScoped(new[] { StorageService.Scope.DevstorageFullControl });
            }

            var serviceInitializer = new BaseClientService.Initializer()
            {
                ApplicationName = "tvTranscribe",
                HttpClientInitializer = credentials
            };

            return new StorageService(serviceInitializer);
        }
        // [END create_storage_client]

        // [START upload_stream]
        public static void UploadStream(string bucketName, string fileNameDest, string content)
        {
            StorageService storage = CreateStorageClient();

            //string content = File.ReadAllText(fileNameSrc);
            var uploadStream = new MemoryStream(Encoding.UTF8.GetBytes(content));

            storage.Objects.Insert(
                bucket: bucketName,
                stream: uploadStream,
                contentType: "text/plain",
                body: new Google.Apis.Storage.v1.Data.Object() { Name = fileNameDest }
            ).Upload();

            Console.WriteLine("Uploaded {0}", fileNameDest);
        }
        // [END upload_stream]
    }
}
