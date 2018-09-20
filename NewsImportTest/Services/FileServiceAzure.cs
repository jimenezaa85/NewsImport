using System;
using System.IO;
using System.Threading.Tasks;

using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;

namespace NewsImportTest.Services
{
    public class FileServiceAzure
    {
        private readonly CloudBlobContainer cloudBlobContainer = null;

        public FileServiceAzure(IConfiguration configuration, bool SAS = false)
        {
            // Check whether the connection string can be parsed.
            CloudStorageAccount storageAccount = null;
            string storageConnectionString = configuration["ConnectionStrings:AzureStorage"];

            if (SAS)
            {
                cloudBlobContainer = new CloudBlobContainer(new Uri(storageConnectionString));
            }
            else if (CloudStorageAccount.TryParse(storageConnectionString, out storageAccount))
            {
                try
                {
                    string sContainerName = configuration["AzureStorage:Container"];
                    CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();
                    cloudBlobContainer = blobClient.GetContainerReference(sContainerName);
                }
                catch (StorageException ex)
                {
                    string sErrorMessage = $"Error returned from FileServiceAzure: {Environment.NewLine} {ex.Message}";

                    System.Diagnostics.Trace.TraceError(sErrorMessage);
                    throw new StorageException(sErrorMessage);
                }
            }
            else
            {
                string sErrorMessage = $"Error returned from FileServiceAzure: { Environment.NewLine}" +
                "A connection string for Azure Storage has not been defined in appsettings.json file. " +
                "Add a variable named 'ConnectionStrings: AzureStorage' with your storage connection string as a value.";

                System.Diagnostics.Trace.TraceError(sErrorMessage);
                throw new StorageException(sErrorMessage);
            }
        }


        public async Task<MemoryStream> Read(string sFileName)
        {
            MemoryStream ms = new MemoryStream();

            try
            {
                CloudBlockBlob blob = cloudBlobContainer.GetBlockBlobReference(sFileName);
                await blob.DownloadToStreamAsync(ms);
                ms.Seek(0, System.IO.SeekOrigin.Begin);
            }
            catch (StorageException)
            {
                ms = null;
            }

            return ms;
        }

        public async Task Delete(string sFileName)
        {
            CloudBlockBlob blob = cloudBlobContainer.GetBlockBlobReference(sFileName);
            await blob.DeleteAsync();
        }

        public async Task<string> Create(string sFileName, MemoryStream ms)
        {
            string sImageKey = Guid.NewGuid().ToString() + Path.GetExtension(sFileName);
            ms.Seek(0, System.IO.SeekOrigin.Begin);
            CloudBlockBlob blob = cloudBlobContainer.GetBlockBlobReference(sImageKey);
            await blob.UploadFromStreamAsync(ms);
            return sImageKey;
        }
    }
}