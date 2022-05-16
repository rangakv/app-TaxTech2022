using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;

namespace TTS.Business
{
    public class BlobService : IBlobService
    {        
        public async Task<byte[]> DownLoadBlobStream(string containerName, string blobName, string conString)
        {           
            BlobServiceClient client = new BlobServiceClient(conString);

            BlobContainerClient containerClient = client.GetBlobContainerClient(containerName);

            BlobClient blobClient =  containerClient.GetBlobClient(blobName);

            using (var ms = new MemoryStream())
            {
                if (blobClient.Exists())
                {
                    await blobClient.DownloadToAsync(ms);
                }
                return ms.ToArray();
            }
        }
    }
}
