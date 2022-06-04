using System;
using System.IO;
using UnityEngine;

namespace Utility
{
    public class Logger : MonoBehaviour
    {
        private StreamWriter _writer;
        private static Logger _instance;

        private void Awake()
        {
            if (_instance == null)
            {
                _instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
                return;
            }
#if UNITY_EDITOR
            _writer = File.AppendText(Path.Combine(Application.dataPath,"log.txt"));   
#else
            _writer = File.AppendText(Path.Combine(Application.persistentDataPath,"log.txt"));
#endif
            Application.logMessageReceived += HandleLog;
        }
 
        private void HandleLog(string condition, string stackTrace, LogType type)
        {
            _writer.Write($"{condition} // {DateTime.Now:HH:mm:ss}\n");
        }

        private void OnApplicationQuit()
        {
            _writer.Close();
        }
    }
}