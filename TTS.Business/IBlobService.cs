using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace TTS.Business
{
    public interface IBlobService
    {
        Task<byte[]> DownLoadBlobStream(string containerName, string blobName, string conString);
    }
}
