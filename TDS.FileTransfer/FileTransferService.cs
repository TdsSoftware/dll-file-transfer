using Renci.SshNet;
using System;
using System.IO;

namespace TDS.FileTransfer
{
    public static class FileTransferService
    {
        [DllExport]
        public static string UploadSFTP(string host, int port, string username, string password, string remoteFileLocation, string uploadfile)
        {
            using (var sftp = new SftpClient(host, port, username, password))
            {
                try
                {
                    sftp.Connect();

                    using (var fileStream = new FileStream(uploadfile, FileMode.Open))
                    {
                        sftp.UploadFile(fileStream, remoteFileLocation);
                    }

                    sftp.Disconnect();
                }
                catch (Exception e)
                {
                    return e.ToString();
                }
            }

            return "OK";
        }
    }
}
