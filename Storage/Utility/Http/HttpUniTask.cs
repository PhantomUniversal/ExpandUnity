using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;

namespace PhantomEngine
{
    public class HttpUniTask
    {
        private CancellationTokenSource cts;
        
        public async UniTask Get()
        {
            string url = "https://localhost:8080/api/test";
            cts = new CancellationTokenSource();

            try
            {
                using UnityWebRequest request = UnityWebRequest.Get(url);
                await request.SendWebRequest().ToUniTask(cancellationToken: cts.Token);

                if (request.result != UnityWebRequest.Result.Success)
                {
                    Debug.LogError("GET Error: " + request.error);
                }
                else
                {
                    string resultText = request.downloadHandler.text;
                    Debug.Log("GET Response: " + resultText);
                }
            }
            catch (OperationCanceledException)
            {
                Debug.Log("GET 요청이 취소되었습니다.");
            }
        }

        public async UniTask Post()
        {
            string url = "https://example.com/api/submit";
            
            WWWForm form = new WWWForm();
            form.AddField("username", "testUser");
            form.AddField("score", "100");

            cts = new CancellationTokenSource();

            try
            {
                using (UnityWebRequest request = UnityWebRequest.Post(url, form))
                {
                    await request.SendWebRequest().ToUniTask(cancellationToken: cts.Token);

                    if (request.result != UnityWebRequest.Result.Success)
                    {
                        Debug.LogError("POST Error: " + request.error);
                    }
                    else
                    {
                        string resultText = request.downloadHandler.text;
                        Debug.Log("POST Response: " + resultText);
                    }
                }
            }
            catch (OperationCanceledException)
            {
                Debug.Log("POST 요청이 취소되었습니다.");
            }
        }
    }
}