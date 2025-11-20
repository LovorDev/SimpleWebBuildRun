using System.IO;
using Unity.Automation.Players.WebGL;
using UnityEditor;
using UnityEngine;

namespace SimpleWebGLServer
{
    public static class WebGLServerMenu
    {
        [MenuItem("Tools/Simple Web Build Run/Stop")]
        public static void StopWebGLBuild()
        {
            SimpleWebServerManager.StopServer();
        }

        [MenuItem("Tools/Simple Web Build Run/Run")]
        public static void RunWebGLBuild()
        {
            RunWebGLWithHost("localhost", "localhost");
        }

        [MenuItem("Tools/Simple Web Build Run/Run (LAN network)")]
        public static void RunWebGLBuildLan()
        {
            var clientHostname = SimpleWebServer.GetLocalIPAddress();
            RunWebGLWithHost(clientHostname, "+");
        }

        private static void RunWebGLWithHost(string clientHostname, string acceptedHostname)
        {
            var buildFolderPath = EditorUtility.OpenFolderPanel("Select WebGL Build Folder", "", "");
            if (string.IsNullOrEmpty(buildFolderPath) || !Directory.Exists(buildFolderPath))
            {
                Debug.LogWarning("WebGL build folder not selected or does not exist");
                return;
            }

            SimpleWebServerManager.StartServer(buildFolderPath, clientHostname, acceptedHostname);
        }
    }
}