using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace PhantomEngine
{
    public class AddressablePatch : MonoBehaviour
    {
        void Start()
        {
            PatchAsync().Forget();
        }

        private async UniTask PatchAsync()
        {
            var initHandle = Addressables.InitializeAsync();
            await initHandle.ToUniTask();
            if (initHandle.Status != AsyncOperationStatus.Succeeded)
            {
                Log.Message("Patch fail", "red");
                return;
            }

            var catalogHandle = Addressables.CheckForCatalogUpdates(false);
            var catalogResult = await catalogHandle.ToUniTask();
            if(catalogHandle.IsValid())
                Addressables.Release(catalogHandle);

            var catalogList = new List<object>();
            if (catalogResult is { Count: > 0 })
            {
                catalogList = await UpdateAsync(catalogResult);
            }
            else
            {
                
                foreach (var catalogLocator in Addressables.ResourceLocators)
                {
                    foreach (var catalogKey in catalogLocator.Keys)
                    {
                        if(!catalogList.Contains(catalogKey))
                            catalogList.Add(catalogKey);
                    }
                }
            }
            
            var catalogSize = await SizeAsync(catalogList);
            Debug.Log($"{catalogSize:F}M");
        }
        
        private async UniTask<List<object>> UpdateAsync(List<string> updateList)
        {
            var updateHandle = Addressables.UpdateCatalogs(updateList, false);
            var updateResult = await updateHandle.ToUniTask();
            var updateLocators = updateResult.SelectMany(list => list.Keys).ToList();
            
            if(updateHandle.IsValid())
                Addressables.Release(updateHandle);
            
            return updateLocators;
        }
        
        private async UniTask<float> SizeAsync(List<object> sizeList)
        {
            if (sizeList == null || sizeList.Count == 0)
                return 0;
            
            var size = (long)0;
            foreach (var sizeKey in sizeList)
            {
                var sizeHandle = Addressables.GetDownloadSizeAsync(sizeKey);
                var sizeResult = await sizeHandle.ToUniTask();
                size += sizeResult;
                
                if(sizeHandle.IsValid())
                    Addressables.Release(sizeHandle);
            }

            var sizeTotal = size / 1024f / 1024f;
            return sizeTotal;
        }
        
        private async UniTask ClearAsync()
        {
            foreach (var clearLocator in Addressables.ResourceLocators)
            {
                var clearHandle = Addressables.ClearDependencyCacheAsync(clearLocator.Keys, false);
                await clearHandle;
                
                if(clearHandle.IsValid())
                    Addressables.Release(clearHandle);
            }
            
            Caching.ClearCache();
        }
    }
}