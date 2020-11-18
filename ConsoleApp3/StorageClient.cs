using Azure.Storage.Blobs;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StorageFileUploader
{
    public class StorageClient
    {
        public async static Task UploadFileToStorageAsync(string storageAccountName, string saskey, string filePath, string containerName="", string blobName = "") 
        {
            if (string.IsNullOrEmpty(storageAccountName))
                throw new ArgumentException("Storage account name is invalid (null or empty)", nameof(storageAccountName));
            if (string.IsNullOrEmpty(saskey))
                throw new ArgumentException("Saskey is ivalid (null or empty)", nameof(saskey));
            if (string.IsNullOrEmpty(filePath))
                throw new ArgumentException("filePath is ivalid (null or empty)", nameof(filePath));

            string sharedConnectionString = ConstructSharedConnectionString(storageAccountName, saskey);
            var storageBlobClient = CreateBlobClientService(sharedConnectionString, false);
            if (string.IsNullOrEmpty(containerName))
                containerName = Path.GetFileNameWithoutExtension(filePath)+ Guid.NewGuid();


            BlobContainerClient containerClient = storageBlobClient.GetBlobContainerClient(containerName);
            await containerClient.CreateIfNotExistsAsync();
            if(string.IsNullOrEmpty(blobName))
                blobName = Path.GetFileNameWithoutExtension(filePath);
            BlobClient blobClient = containerClient.GetBlobClient(blobName);

            using FileStream uploadFileStream = File.OpenRead(filePath);
            await blobClient.UploadAsync(uploadFileStream);
        }

        public static string ConstructSharedConnectionString(string storageAccountName, string sasKey)
        {
            if (string.IsNullOrEmpty(storageAccountName))
                throw new ArgumentException("Storage account name is invalid (null or empty)", nameof(storageAccountName));
            if (string.IsNullOrEmpty(sasKey))
                throw new ArgumentException("Saskey is ivalid (null or empty)", nameof(sasKey));

            string sharedConnectionString = $"BlobEndpoint=https://{storageAccountName}.blob.core.windows.net;SharedAccessSignature={sasKey}";
            return sharedConnectionString;
        }

        public static BlobServiceClient CreateBlobClientService(string sharedConnectionString, bool isStorageLocal = false)
        {
            if (isStorageLocal)
                return new BlobServiceClient("UseDevelopmentStorage=true");

            else
                return new BlobServiceClient(sharedConnectionString);

        }
    }
}
