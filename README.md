# Simple WebGL Server Tools

Editor tools to run a WebGL build in a simple HTTP server right from Unity. Shows a small control window with a link to the served build. Closing the window stops the server. The server also stops automatically when the Unity Editor exits, and starting a new server shuts down any existing one.

## Features
- Start serving any selected WebGL build folder via a local HTTP server.
- Small control window with the build path and a clickable URL.
- Window close (X) stops the server; no extra “stop” button needed.
- “LAN” mode to serve the build to devices on the local network.
- Automatically stops the server on Unity Editor quit.
- Detects an already running server and stops it before starting a new one.

## Requirements
- Unity `2020.3+` (tested in Windows Editor).
- A `SimpleWebServer` implementation available in your project, providing:
  - `Start(string rootPath, string baseUrl)`
  - `Stop()`
  - `GetUnusedPort(): int`
  - `GetLocalIPAddress(): string`
- Firewall permissions to allow inbound connections for LAN mode.

## Installation

### Via Unity Package Manager (Git URL)
- Open `Window > Package Manager`.
- Click `+` and choose `Add package from Git URL...`.
- Enter: `https://github.com/LovorDev/SimpleWebBuildRun.git#1.0.0`

### From disk (local folder)
- Place this folder in your project under `Packages/` or use `Add package from disk...` and select this folder (it contains `package.json`).

## Usage

- `Tools > Run WebGL Build in Simple Web Server`
  - Select your WebGL build folder (the folder containing `index.html`).
  - A small window appears with the URL and controls.
  - The URL opens in your default browser.

- `Tools > Run WebGL Build in Simple Web Server (LAN)`
  - Same as above, but uses your LAN IP and accepts connections from other devices on the network.
  - Make sure your firewall allows inbound connections on the chosen port.

- `Tools > Stop WebGL Build in Simple Web Server`
  - Explicitly stops the server (closing the control window does the same).

### Control Window
- Displays:
  - `Build folder`: absolute path to the selected WebGL build.
  - `URL`: link to open the served build.
- Buttons:
  - `Open`: opens the URL in your default browser.
  - `Copy link`: copies the URL to the clipboard.
- Closing the window (X) immediately stops the server.

## LAN Notes
- The server binds to `+` (all interfaces) and your URL uses `http://<your LAN IP>:<port>/`.
- Ensure Windows Firewall (or your OS firewall) allows inbound access to the selected port.
- The port is chosen automatically; if blocked, the page may be unreachable from other devices.

## Troubleshooting
- “Server already running” dialog:
  - The tool stops the previous server before starting a new one.
- 404 or blank page:
  - Verify you selected the correct WebGL build folder (containing `index.html` and `Build`/`TemplateData`).
- Port issues or unreachable from LAN:
  - Check firewall rules and that both devices are on the same network.
- Server doesn’t stop:
  - Close the control window (X) or use `Tools > Stop WebGL Build in Simple Web Server`.

## Development
- Editor scripts live under `Editor/`:
  - `WebGLServerMenu.cs`: menu entries and refactored host selection.
  - `SimpleWebServerWindow.cs`: the control window.
  - `SimpleWebServerManager.cs`: lifecycle management and Editor quit hook.
- Tag releases (`1.0.0`) for UPM consumption via Git URL (`#<tag>`).

## License
Choose and add a license file appropriate for your project (e.g., MIT).