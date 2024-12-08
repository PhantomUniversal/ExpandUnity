using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using UnityEngine;

namespace PhantomEngine
{
    public static class Log
    {
        private static readonly Dictionary<LogType, LogData> LogContainer = new();
        
        
        private static string GetDirectoryPath()
        {
            return Path.Combine(Application.persistentDataPath, "Log");
        }
        
        private static string GetLogPath(LogType logType)
        {
            string logDirectory = GetDirectoryPath();
            return Path.Combine(logDirectory, $"{logType}Log.txt");
        }
        
        
        public static void ReportLog(object logKey, object logValue = null)
        {
#if UNITY_EDITOR
            Debug.Log($"Key: {logKey} / Value: {logValue}");
#endif
            
            UpdateLog(logKey, logValue, LogType.Default);
        }
        
        public static void ErrorLog(object logKey, object logValue = null)
        {
#if UNITY_EDITOR
            Debug.LogError($"Key: {logKey} / Value: {logValue}");
#endif
            
            UpdateLog(logKey, logValue, LogType.Default);
        }
        
        
        public static LogData LoadLog(LogType logType)
        {
            string logPath = GetLogPath(logType);
            if (!File.Exists(logPath)) 
                return new LogData();
            
            string logFile = File.ReadAllText(logPath);
            return JsonConvert.DeserializeObject<LogData>(logFile);
        }

        public static void SaveLog(LogType logType, LogData logData)
        {
            string logDirectory = GetDirectoryPath();
            if (!Directory.Exists(logDirectory))
            {
                Directory.CreateDirectory(logDirectory);
            }
            
            string logFile = JsonConvert.SerializeObject(logData); 
            string logPath = GetLogPath(logType);
            File.WriteAllText(logPath, logFile);
        }
        
        public static void UpdateLog(object logKey, object logValue, LogType logType)
        {
            if (!LogContainer.ContainsKey(logType))
            {
                LogData logData = LoadLog(logType) ;
                LogContainer.Add(logType, logData);
            }

            LogContainer[logType].Table.AddLast(new LogTable(logKey.ToString(), logValue.ToString()));
            if (LogContainer[logType].Table.Count >= 200)
            {
                LogContainer[logType].Table.RemoveFirst();
            }
            
            SaveLog(logType, LogContainer[logType]);
        }
        
        public static void ClearLog()
        {
            foreach (Enum logType in Enum.GetValues(typeof(LogType)))
            {
                string logPath = GetLogPath((LogType)logType);
                if (File.Exists(logPath))
                {
                    File.Delete(logPath);
                }
            }
        }
    }
}