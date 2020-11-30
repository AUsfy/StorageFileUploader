# StorageFileUploader

Simple .NET 5 console application to upload files to storage. The application can be used directly once downloaded and copied, you don't need to install anything. The application is cross-plateform.

## How to use StorageFileUploader:

- Open a commandline within the directory where you copied the executable file.
- Type ```StorageFileUploader.exe --help``` to get details about the arguments.
- Run StorageFileUploader.exe with the needed arguments. 

## Example
```StorageFileUploader.exe --storage  "storage-name" --file "file-path" --saskey "storage-saskey" -containername "container_example" --blobname "example"```

For now the application supports only sas keys.


