# VMs Dashboard App

A Windows desktop application for secure file transfer and remote management of virtual machines (VMs) over SSH/SFTP.

## Features

### VM Management
- **VM List**: Load and display a list of VMs from a configurable `hosts.json` file.
- **Ping & SSH Status**: Check connectivity and SSH availability for each VM, with visual status icons.
- **Remote SSH Console**: Open an SSH console to any VM with a single click.

### File Transfer
- **Transfer Modes**: Transfer files to or from remote VMs using SFTP.
- **Drag & Drop**: Drag and drop files for upload in "Transfer to" mode.
- **Directory Browsing**: Browse and select local and remote directories/files using UI dialogs and tree views.
- **Progress Bar**: Visual progress for file transfers.
- **Transfer Status**: Status list for ongoing and completed transfers, with color-coded feedback.

### Jenkins Pipeline Integration
- **Pipeline List**: Load available Jenkins pipelines from `pipelines.json`.
- **Remote Download**: Download pipeline artifacts directly to a selected VM directory via SSH.

### Application Version Management
- **Installed Apps Overview**: Display a table of installed applications and their versions for each VM.
- **Reload VM Information**: Refresh and retrieve the latest installed app versions and VM details (hostname, OS, IP).
- **Automatic App Version Detection**:
  - Uses PowerShell registry queries to detect installed application versions.
  - Handles both 64-bit and 32-bit registry locations.

## Configuration Files
- `hosts.json`: List of VMs/hosts to manage.
- `apps.json`: List of applications to check for version info on each VM.
- `pipelines.json`: Jenkins pipeline definitions for remote downloads.

## Example Configuration Files

### hosts.json
```json
[
  {
    "Name": "vm1",
    "Host": "192.168.1.101",
    "Username": "admin",
    "Password": "your_password"
  },
  {
    "Name": "vm2",
    "Host": "192.168.1.102",
    "Username": "user",
    "Password": "your_password"
  }
]
```

### apps.json
```json
[
  { "DisplayName": "App1", "WingetName": "App1 full name" },
  { "DisplayName": "App2", "WingetName": "App2 full name" },
  { "DisplayName": "App3", "WingetName": "App3 full name" },
  { "DisplayName": "App4", "WingetName": "App4 full name" },
  { "DisplayName": "App5", "WingetName": "App5 full name" }
]
```

### pipelines.json
```json
{
  "Pipeline1": "https://jenkins.example.com/job/pipeline1/lastSuccessfulBuild/artifact/output.zip",
  "Pipeline2": "https://jenkins.example.com/job/pipeline2/lastSuccessfulBuild/artifact/app.exe"
}
```

## Requirements
- Windows 10/11
- .NET 8.0 or newer
- SSH access to target VMs
- For app version detection: PowerShell access

## How It Works
1. **Load Hosts**: Reads `hosts.json` and populates the VM list.
2. **Select VM**: Checks ping/SSH, displays installed apps, and enables file transfer controls.
3. **Transfer Files**: Choose transfer mode, select files/directories, and start transfer with progress feedback.
4. **Jenkins Download**: Select a pipeline and target directory, then download artifacts directly to the VM.
5. **App Version Detection**: On reload, retrieves app versions using PowerShell registry queries.

## Troubleshooting
- **Missing App Data**: Ensure the remote VM has PowerShell and registry access enabled.
- **SSH/Network Issues**: Check VM network/firewall settings and SSH credentials.

## Extending
- Add new VMs to `hosts.json`.
- Add new apps to `apps.json` for version tracking.
- Add new Jenkins pipelines to `pipelines.json`.

## License
MIT License
