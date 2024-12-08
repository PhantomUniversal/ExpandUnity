using System;
using System.Collections.Generic;

namespace PhantomEngine
{
    [Serializable]
    public class LogData
    {
        public LinkedList<LogTable> Table = new();
    }
    
    [Serializable]
    public class LogTable
    {
        public DateTime Date;
        public string Key;
        public string Value;
        
        public LogTable(string logKey, string logValue)
        {
            Date = DateTime.Now;
            Key = logKey;
            Value = logValue;
        }
    }
}