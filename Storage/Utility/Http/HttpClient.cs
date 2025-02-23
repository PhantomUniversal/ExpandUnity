using System;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;

namespace PhantomEngine
{
    public class HttpClient
    {
        private readonly IHttpOption serializationOption;

        public HttpClient(IHttpOption option)
        {
            serializationOption = option;
        }
        
        public async Task Get(string url)
        {
            try
            {
                using var www = UnityWebRequest.Get(url);
                www.SetRequestHeader("Content-Type", serializationOption.ContentType);

                var operation = www.SendWebRequest();

                while (!operation.isDone)
                    await Task.Yield();

                if (www.result != UnityWebRequest.Result.Success)
                    Debug.LogError($"Failed: {www.error}");

                var result = www.downloadHandler.text;
                Debug.Log(result);
            }
            catch (Exception ex)
            {
                Debug.LogError($"{nameof(Get)} failed: {ex.Message}");
            }
        }
    }
}