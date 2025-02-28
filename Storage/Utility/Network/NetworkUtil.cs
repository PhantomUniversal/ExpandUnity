using System.Net.NetworkInformation;
using System.Net.Sockets;
using UnityEngine;

namespace PhantomEngine
{
    public class NetworkUtil : MonoBehaviour
    {
        private NetworkInfo networkInfo;

        
        public bool IsGui { get; private set; }


        public bool GetInfo(out NetworkInfo info)
        {
            if (networkInfo != null)
            {
                info = networkInfo;
                return true;
            }

            info = null;
            return false;
        }
        
        public void SetInfo(NetworkInfo info)
        {
            networkInfo = info;
        }
        
        
        void Start()
        {
            if (GetIPv4(out var info))
            {
                SetInfo(info);
            }
        }
    
        private void OnGUI()
        {
            if (networkInfo != null)
            {
                
            }
        }
    
    
        private bool GetIPv4(out NetworkInfo info)
        {
            NetworkInterface[] adapters = NetworkInterface.GetAllNetworkInterfaces();
            Debug.Log(adapters.Length);
            foreach (NetworkInterface adapter in adapters)
            {
                if (!CheckOperational(adapter.OperationalStatus))
                {
                    info = null;
                    return false;
                }
                
                IPInterfaceProperties properties = adapter.GetIPProperties();
                if (properties is null)
                {
                    info = null;
                    return false;
                }
                
                var uniCast = properties.UnicastAddresses;
                if (uniCast.Count > 0)
                {
                    foreach (var ip in uniCast)
                    {
                        if (ip.Address.AddressFamily == AddressFamily.InterNetwork)
                        {
                            info = new NetworkInfo();
                            return true;
                        }
                    }        
                }
            }

            info = null;
            return false;
        }
        
        public bool CheckOperational(OperationalStatus status)
        {
            if (status == OperationalStatus.Up) 
                return true;
            
    #if UNITY_EDITOR
            Debug.Log($"Network operational fail: {status}");
    #endif            
            return false;
        }
    }
   
}