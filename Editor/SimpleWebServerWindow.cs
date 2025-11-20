using System;
using UnityEditor;
using UnityEngine;

namespace SimpleWebGLServer
{
	internal class SimpleWebServerWindow : EditorWindow
	{
		public static event Action OnDisabled = () => { };

		private string _clientUrl;
		private string _rootPath;

		private void OnDisable()
		{
			OnDisabled();
		}

		private void OnGUI()
		{
			EditorGUILayout.LabelField("Server running", EditorStyles.boldLabel);
			EditorGUILayout.Space();

			EditorGUILayout.LabelField("Build folder:", _rootPath ?? "-");
			EditorGUILayout.LabelField("URL:", _clientUrl ?? "-");

			EditorGUILayout.Space();

			using (new EditorGUILayout.HorizontalScope())
			{
				if (GUILayout.Button("Open"))
					Application.OpenURL(_clientUrl);

				if (GUILayout.Button("Copy link"))
				{
					EditorGUIUtility.systemCopyBuffer = _clientUrl;
					ShowNotification(new GUIContent("Link copied"), .2f);
				}
			}

			EditorGUILayout.Space();
			EditorGUILayout.HelpBox("Close this window to stop the server.", MessageType.Info);
		}

		public static void Show(string clientUrl, string rootPath)
		{
			var win = GetWindow<SimpleWebServerWindow>(true, "WebGL Server", true);
			win.position = new Rect(100,
				100,
				420,
				160);

			win.minSize = new Vector2(360, 120);
			win.maxSize = new Vector2(600, 160);

			win._clientUrl = clientUrl;
			win._rootPath = rootPath;

			win.Show();
		}
	}
}