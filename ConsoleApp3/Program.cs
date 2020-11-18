using Azure.Storage;
using Azure.Storage.Blobs;
using System;
using System.IO;
using System.Reflection.Metadata;
using System.Threading.Tasks;

namespace StorageFileUploader
{
    public class Program
    {
        static async Task Main(string[] args)
        {
            var accountName = "storageacounttest1";
            var saskey = @"?sv=2019-12-12&ss=bfqt&srt=sco&sp=rwdlacupx&se=2020-11-19T04:21:38Z&st=2020-11-18T20:21:38Z&spr=https&sig=BT%2BuwyrzBnBT8P4Crzsm2%2FKQJzWbsiPvHpii7O%2BJgK0%3D";
            var containerName = "test-container";
            var blobName = "file.txt";
            var FileToUploadPath = @"D:\Code\Test\index.html";

            string sharedConnectionString = ConstructSharedConnectionString(accountName, saskey);
            var storageBlobClient = CreateBlobClientService(sharedConnectionString, false);
            BlobContainerClient containerClient = storageBlobClient.GetBlobContainerClient(containerName);
            await containerClient.CreateIfNotExistsAsync();
            BlobClient blobClient = containerClient.GetBlobClient(blobName);

            using FileStream uploadFileStream = File.OpenRead(FileToUploadPath);
            await blobClient.UploadAsync(uploadFileStream);
        }

     
        static string ConstructSharedConnectionString(string storageAccountName, string sasKey)
        {
            if (string.IsNullOrEmpty(storageAccountName))
                throw new ArgumentException("Storage account name is invalid (null or empty)");
            if (string.IsNullOrEmpty(sasKey))
                throw new ArgumentException("Saskey is ivalid (null or empty)");

            string sharedConnectionString = $"BlobEndpoint=https://{storageAccountName}.blob.core.windows.net;SharedAccessSignature={sasKey}";
            return sharedConnectionString;
        }

        static BlobServiceClient CreateBlobClientService(string sharedConnectionString, bool isStorageLocal = false)
        {
            if (isStorageLocal)
                return new BlobServiceClient("UseDevelopmentStorage=true");

            else
                return new BlobServiceClient(sharedConnectionString);

        }
    }
}
