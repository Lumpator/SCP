# SCP File Transfer App

SCP File Transfer App is a Windows Forms application that allows users to transfer files between a local machine and a remote server using SCP (Secure Copy Protocol). The app supports both uploading files to a remote server and downloading files from a remote server.

![image](https://github.com/user-attachments/assets/c6edda04-b596-4f08-84bc-de41fdfa52ab)

## Features

- **Transfer Modes**: Supports two transfer modes - Transfer to (upload) and Transfer from (download).
- **Host Management**: Load and manage multiple remote hosts from a JSON configuration file.
- **Ping and SSH Check**: Automatically checks the connectivity and SSH availability of the selected host.
- **Remote Directory Browsing**: Browse remote directories and files using a tree view.
- **Drag and Drop**: Supports drag and drop for selecting local files in "Transfer to" mode.
- **Progress Bar**: Displays the progress of file transfer operations.
- **SSH Console**: Open an SSH console to the selected host.

## Prerequisites

- .NET Framework 4.7.2 or later
- A JSON configuration file (`hosts.json`) containing the remote host information

## Usage

1. Run the application.

2. Load the hosts by ensuring the `hosts.json` file is in the application's base directory. The file should have the following structure:
```  
[
    {
        "Name": "Host1",
        "Host": "hostname1",
        "Username": "user1",
        "Password": "password1"
    },
    {
        "Name": "Host2",
        "Host": "hostname2",
        "Username": "user2",
        "Password": "password2"
    }
]
```
4. Select a host from the dropdown list. The app will automatically check the connectivity and SSH availability of the selected host.

5. Choose the transfer mode (Transfer to or Transfer from) using the mode dropdown.

6. Select the local file or directory:
    - For "Transfer to" mode, click "Browse Local Files" to select a file.
    - For "Transfer from" mode, click "Browse Local Directories" to select a directory.

7. Select the remote directory or file:
    - For "Transfer to" mode, browse the remote directories and select a directory.
    - For "Transfer from" mode, browse the remote files and select a file.

8. Click the "Transfer" button to start the file transfer. The progress bar will display the transfer progress.

9. Optionally, open an SSH console to the selected host by clicking the "SSH Console" button.

## License

This project is licensed under the MIT License. See the [LICENSE](LICENSE) file for details.

## Acknowledgements

- [Renci.SshNet](https://github.com/sshnet/SSH.NET) - A Secure Shell (SSH) library for .NET, used for SSH and SFTP operations.
- [Newtonsoft.Json](https://www.newtonsoft.com/json) - A popular high-performance JSON framework for .NET.
- [FlatIcon](https://www.flaticon.com/free-icons/transfer) - Transfer icons created by Design Circle - Flaticon
