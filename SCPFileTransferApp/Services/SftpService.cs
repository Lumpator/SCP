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
                var taskCompletionSource = new TaskCompletionSource<bool>();

                Action<ulong> uploadCallback = bytesUploaded =>
                {
                    double progress = (double)bytesUploaded / fileSize * 100;
                    progressCallback(progress);
                };

                IAsyncResult asyncResult = sftpClient.BeginUploadFile(fileStream, remoteDirectoryPath + "/" + Path.GetFileName(localFilePath), (result) =>
                {
                    try
                        {
                        sftpClient.EndUploadFile(result);
                        taskCompletionSource.TrySetResult(true);
                        }
                    catch (Exception ex)
                        {
                        taskCompletionSource.TrySetException(ex);
                        }
                }, null, uploadCallback);

                await taskCompletionSource.Task;
                }
            }
        public async Task DownloadFileAsync(string remoteFilePath, string localDirectoryPath, Action<double> progressCallback)
            {
            var localFilePath = Path.Combine(localDirectoryPath, Path.GetFileName(remoteFilePath));
            using (var fileStream = new FileStream(localFilePath, FileMode.Create))
                {
                var fileSize = sftpClient.GetAttributes(remoteFilePath).Size;
                var taskCompletionSource = new TaskCompletionSource<bool>();

                Action<ulong> downloadCallback = bytesDownloaded =>
                {
                    double progress = (double)bytesDownloaded / fileSize * 100;
                    progressCallback(progress);
                };

                IAsyncResult asyncResult = sftpClient.BeginDownloadFile(remoteFilePath, fileStream, (result) =>
                {
                    try
                        {
                        sftpClient.EndDownloadFile(result);
                        taskCompletionSource.TrySetResult(true);
                        }
                    catch (Exception ex)
                        {
                        taskCompletionSource.TrySetException(ex);
                        }
                }, null, downloadCallback);

                await taskCompletionSource.Task;
                }
            }
        public SftpFileAttributes GetFileAttributes(string remoteFilePath)
            {
            return sftpClient.GetAttributes(remoteFilePath);
            }

        public static async Task<bool> CheckSSHConnectionAsync(HostInfo host)
            {
            try
                {
                return await Task.Run(() =>
                {
                    var client = new SftpService(host);
                    if (client.Connect())
                        {
                        client.Disconnect();
                        return true;
                        }
                    return false;
                });
                }
            catch
                {
                return false;
                }
            }
        }
    }