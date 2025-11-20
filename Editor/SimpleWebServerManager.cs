using System;
using System.IO;
using Unity.Automation.Players.WebGL;
using UnityEditor;
using UnityEngine;

namespace SimpleWebGLServer
{
	internal static class SimpleWebServerManager
	{
		private static bool _quitHooked;
		public static bool IsRunning { get; private set; }
		public static int Port { get; private set; }
		public static string RootPath { get; private set; }
		public static string ClientUrl { get; private set; }

		public static void StartServer(string buildFolderPath, string clientHostname, string acceptedHostname)
		{
			if (string.IsNullOrEmpty(buildFolderPath) || !Directory.Exists(buildFolderPath))
			{
				Debug.LogWarning("WebGL build folder not selected or does not exist");
				return;
			}

			if (IsRunning)
			{
				EditorUtility.DisplayDialog("Server already running", "Current server will be stopped before starting a new one.", "OK");
				StopServer();
			}

			SimpleWebServer.Stop();

			Port = SimpleWebServer.GetUnusedPort();
			RootPath = buildFolderPath;

			var baseUrl = $"http://{acceptedHostname}:{Port}/";
			SimpleWebServer.Start(RootPath, baseUrl);

			ClientUrl = $"http://{clientHostname}:{Port}/";
			IsRunning = true;

			SimpleWebServerWindow.Show(ClientUrl, RootPath);
			SimpleWebServerWindow.OnDisabled += StopServer;

			Application.OpenURL(ClientUrl);

			Debug.Log($"Simple Web Server started at {baseUrl} serving {RootPath}");

			if (!_quitHooked)
			{
				_quitHooked = true;
				EditorApplication.quitting += OnEditorQuitting;
			}
		}

		public static void StopServer()
		{
			if (!IsRunning)
				return;

			SimpleWebServer.Stop();

			Debug.Log($"Simple Web Server stopped at {ClientUrl} serving {RootPath}");
			
			IsRunning = false;
			Port = 0;
			RootPath = null;
			ClientUrl = null;

			SimpleWebServerWindow.OnDisabled -= StopServer;
		}

		private static void OnEditorQuitting()
		{
			try
			{
				StopServer();
			}
			catch (Exception ex)
			{
				Debug.LogWarning($"Error stopping server on editor quit: {ex.Message}");
			}
		}
	}
}