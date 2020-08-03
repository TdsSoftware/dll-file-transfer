using Renci.SshNet;
using System;
using System.IO;

namespace TDS.FileTransfer
{
    public static class FileTransferService
    {
        [DllExport]
        public static string UploadSFTP(string host, int port, string username, string password, string remoteDirectory, string uploadfile)
        {
            using (SftpClient sftp = new SftpClient(host, port, username, password))
            {
                try
                {
                    sftp.Connect();

                    using (var fileStream = new FileStream(uploadfile, FileMode.Open))
                    {
                        var nomeArquivo = Path.GetFileName(fileStream.Name);
                        var destino = $"{remoteDirectory}/{nomeArquivo}";
                        sftp.UploadFile(fileStream, destino);
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
