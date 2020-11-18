using Azure.Storage;
using Azure.Storage.Blobs;
using System;
using System.IO;
using System.Reflection.Metadata;
using System.Threading.Tasks;
using CommandLine;
using System.Collections.Generic;

namespace StorageFileUploader
{
    public class Program
    {
        public class Options
        {
            [Option('s', "storageaccount", Required = true, HelpText = "Storage account name.")]
            public string StorageAccountName { get; set; }

            [Option('f', "filepath", Required = true, HelpText = "File to upload path.")]
            public string FilePath { get; set; }


            [Option('k', "saskey", Required = true, HelpText = "Storage sas key.")]
            public string Saskey { get; set; }


            [Option('c', "containername", Required = false, HelpText = "Container name.")]
            public string ContainerName { get; set; }

            [Option('b', "blobname", Required = false, HelpText = "Blob name.")]
            public string BlobName { get; set; }

        }

        static void Main(string[] args)
        {
            CommandLine.Parser.Default.ParseArguments<Options>(args)
              .WithParsed(RunOptions)
              .WithNotParsed(HandleParseError);
        }
        static void RunOptions(Options opts)
        {
            //handle options

            StorageClient.UploadFileToStorageAsync(opts.StorageAccountName, opts.Saskey, opts.FilePath, opts.ContainerName, opts.BlobName).GetAwaiter().GetResult();
        }
        static void HandleParseError(IEnumerable<Error> errs)
        {
            //handle errors
        }

        //static async Task Main(string[] args)
        //{
        //    var accountName = "storageacounttest1";
        //    var saskey = @"?sv=2019-12-12&ss=bfqt&srt=sco&sp=rwdlacupx&se=2020-11-19T04:21:38Z&st=2020-11-18T20:21:38Z&spr=https&sig=BT%2BuwyrzBnBT8P4Crzsm2%2FKQJzWbsiPvHpii7O%2BJgK0%3D";
        //    var containerName = "test-container";
        //    var blobName = "file.txt";
        //    var FileToUploadPath = @"D:\Code\Test\index.html";
        //    await StorageClient.UploadFileToStorageAsync(accountName, saskey, FileToUploadPath);


        //}
    }
}
