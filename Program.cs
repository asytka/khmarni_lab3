using Azure.Data.Tables;
using Azure.Storage.Blobs;

namespace khmarni_lab3
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();
            Application.Run(new Form1());
        }
    }

    public class AzureTableStorageService
    {
        private readonly TableClient _tableClient;

        public AzureTableStorageService(string connectionString, string tableName)
        {
            _tableClient = new TableClient(connectionString, tableName);
            _tableClient.CreateIfNotExists(); // Ensure the table exists
        }

        public async Task AddContactAsync(ContactEntity contact)
        {
            await _tableClient.AddEntityAsync(contact);
        }

        public async Task<List<ContactEntity>> GetAllContactsAsync()
        {
            var contacts = new List<ContactEntity>();
            await foreach (var entity in _tableClient.QueryAsync<ContactEntity>())
            {
                contacts.Add(entity);
            }
            return contacts;
        }

        public async Task DeleteContactAsync(string partitionKey, string rowKey)
        {
            await _tableClient.DeleteEntityAsync(partitionKey, rowKey);
        }

    }
    public class AzureBlobStorageService
    {
        private readonly BlobContainerClient _blobContainerClient;

        public AzureBlobStorageService(string connectionString, string containerName)
        {
            // Initialize the BlobContainerClient
            var blobServiceClient = new BlobServiceClient(connectionString);
            _blobContainerClient = blobServiceClient.GetBlobContainerClient(containerName);

            // Create the container if it doesn't exist
            _blobContainerClient.CreateIfNotExists();
        }
        public async Task<Stream> DownloadBlobAsync(string blobUrl)
        {
            string sasToken = "sp=r&st=2024-11-17T14:49:52Z&se=2024-11-17T22:49:52Z&spr=https&sv=2022-11-02&sr=c&sig=ghtgCOhf2%2F2MuEIevnarJnPmqcJ4hgFxfj3bhbBWSMw%3D";
            BlobClient blobClient = new BlobClient(new Uri($"{blobUrl}?{sasToken}"));
            var memoryStream = new MemoryStream();
            await blobClient.DownloadToAsync(memoryStream);
            memoryStream.Position = 0; // Reset the stream position to the beginning
            return memoryStream;
        }
        // Uploads a file and returns the blob URL
        public async Task<string> UploadFileAsync(string filePath, string blobName)
        {
            var blobClient = _blobContainerClient.GetBlobClient(blobName);
            await blobClient.UploadAsync(filePath, overwrite: true);
            return blobClient.Uri.ToString();
        }

        // Deletes a blob by its URL
        public async Task DeleteBlobAsync(string blobName)
        {
            var blobClient = _blobContainerClient.GetBlobClient(blobName);
            await blobClient.DeleteIfExistsAsync();
        }
    }

}