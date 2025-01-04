using Renci.SshNet;
using Renci.SshNet.Sftp;
using SCPFileTransferApp.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace SCPFileTransferApp.Services
    {
    public class SftpService
        {
        private SftpClient sftpClient;

        public SftpService(HostInfo hostInfo)
            {
            sftpClient = new SftpClient(hostInfo.Host, hostInfo.Username, hostInfo.Password);
            }

        public bool Connect()
            {
            try
                {
                sftpClient.ConnectionInfo.Timeout = TimeSpan.FromMilliseconds(3000);
                sftpClient.Connect();
                return true;
                }
            catch (Exception)
                {
                return false;
                }
            }
        public void Disconnect()
            {
            if (sftpClient.IsConnected)
                {
                sftpClient.Disconnect();
                }
            }

        public IEnumerable<SftpFile> ListDirectory(string path)
            {
            return sftpClient.ListDirectory(path).Cast<SftpFile>();
            }

        public async Task UploadFileAsync(string localFilePath, string remoteDirectoryPath, Action<double> progressCallback)
            {
            using (var fileStream = File.OpenRead(localFilePath))
                {
                long fileSize = fileStream.Length;

                await Task.Run(() =>
                {
                    var stopwatch = new System.Diagnostics.Stopwatch();
                    stopwatch.Start();

                    sftpClient.UploadFile(fileStream, remoteDirectoryPath + "/" + Path.GetFileName(localFilePath), (uploaded) =>
                    {
                        if (stopwatch.ElapsedMilliseconds >= 1) // Update UI every 1 millisecond to avoid freezing UI
                            {
                            double progress = (double)uploaded / fileSize * 100;
                            progressCallback(progress);
                            stopwatch.Restart();
                            }
                    });
                });
                }
            }
        public async Task DownloadFileAsync(string remoteFilePath, string localDirectoryPath, Action<double> progressCallback)
            {
            var localFilePath = Path.Combine(localDirectoryPath, Path.GetFileName(remoteFilePath));
            using (var fileStream = new FileStream(localFilePath, FileMode.Create))
                {
                var fileSize = sftpClient.GetAttributes(remoteFilePath).Size;
                var buffer = new byte [1024 * 1024]; // 1MB buffer
                int bytesRead;
                double totalBytesRead = 0;

                using (var remoteStream = sftpClient.OpenRead(remoteFilePath))
                    {
                    while ((bytesRead = await remoteStream.ReadAsync(buffer, 0, buffer.Length)) > 0)
                        {
                        await fileStream.WriteAsync(buffer, 0, bytesRead);
                        totalBytesRead += bytesRead;
                        progressCallback?.Invoke((totalBytesRead / fileSize) * 100);
                        }
                    }
                }
            }
        public SftpFileAttributes GetFileAttributes(string remoteFilePath)
            {
            return sftpClient.GetAttributes(remoteFilePath);
            }
        }
    }