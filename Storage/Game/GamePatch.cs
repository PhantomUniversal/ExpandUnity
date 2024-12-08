using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace PhantomEngine
{
    public class GamePatch : MonoBehaviour
    {
        [SerializeField] 
        private bool PatchAuto = true;
        [SerializeField]
        private List<AssetLabelReference> PatchLabels = new();
        
        
        private void Start()
        {
            if (!PatchAuto)
                return;
            
            PatchProgressAsync().Forget();
        }

        private void OnDestroy()
        {
            if (PatchLabels is { Count: > 0 })
            {
                PatchLabels.Clear();
                PatchLabels = null;
            }
        }
        
        
        private async UniTask PatchProgressAsync()
        {            
            var patchInit = await InitializeContentAsync();
            if (!patchInit)
            {
                Log.ReportLog(101);
                return;
            }
            
            var patchCatalog = await CheckCatalogAsync();
            if (patchCatalog.Count > 0)
            {
                Log.ReportLog(102);
                await UpdateCatalogAsync(patchCatalog);
            }

            var patchList = ConvertLabelReference(PatchLabels);
            var patchSize = await TotalSizeAsync(patchList);
            if (patchSize > 0)
            {
                var patchMb = $"{patchSize / 1024 / 1024:F} MB";
                Log.ReportLog(103, patchMb);
                await DownloadProgressAsync(patchList);
            }
            else
            {
                Log.ReportLog(104, "Exit patch");
            }
        }
        
        private async UniTask<bool> InitializeContentAsync()
        {
            var initHandle = Addressables.InitializeAsync();
            await initHandle.ToUniTask();
            if (initHandle.Status != AsyncOperationStatus.Succeeded)
            {
                return false;
            }
            
            if (initHandle.IsValid())
                Addressables.Release(initHandle);

            return true;
        }
        
        private async UniTask<List<string>> CheckCatalogAsync()
        {
            var catalogHandle = Addressables.CheckForCatalogUpdates(false);
            var catalogResult = await catalogHandle.ToUniTask();
            if (catalogHandle.Status != AsyncOperationStatus.Succeeded)
            {
                return default;
            }
            
            if(catalogHandle.IsValid())
                Addressables.Release(catalogHandle);
            
            return catalogResult; 
        }

        private async UniTask UpdateCatalogAsync(List<string> updateList)
        {
            var updateHandle = Addressables.UpdateCatalogs(updateList, false);
            await updateHandle.ToUniTask();
            if (updateHandle.Status != AsyncOperationStatus.Succeeded)
            { 
                return;
            }
            
            if(updateHandle.IsValid())
                Addressables.Release(updateHandle);
        }
        
        private List<string> ConvertLabelReference(List<AssetLabelReference> resourceReferences)
        {
            var resourceList = new List<string>();
            foreach (var resourceLabel in resourceReferences)
            {
                if(!resourceList.Contains(resourceLabel.labelString))
                    resourceList.Add(resourceLabel.labelString);
            }

            return resourceList;
        }

        private async UniTask<long> PartialSizeAsync(string sizeKey)
        {
            var sizeHandle = Addressables.GetDownloadSizeAsync(sizeKey);
            var sizeResult = await sizeHandle.ToUniTask();
            
            if(sizeHandle.IsValid())
                Addressables.Release(sizeHandle);

            return sizeResult;
        }
        
        private async UniTask<long> TotalSizeAsync(List<string> sizeList)
        {
            var sizeTotal = (long)0;
            foreach (var sizeKey in sizeList)
            {
                var size = await PartialSizeAsync(sizeKey);
                sizeTotal += size;
            }

            return sizeTotal;
        }

        private async UniTask DownloadProgressAsync(List<string> downloadList)
        {
            foreach (var downloadKey in downloadList)
            {
                var downloadSize = await PartialSizeAsync(downloadKey);
                if (downloadSize == 0) 
                    continue;
                
                var downloadHandle = Addressables.DownloadDependenciesAsync(downloadKey);
                while (!downloadHandle.IsDone)
                {
                    //var downloadPercent = downloadHandle.PercentComplete;
                    //var downloadValue = (int)(downloadSize * downloadPercent);
                    await UniTask.Yield();
                }
                    
                if(downloadHandle.IsValid())
                    Addressables.Release(downloadHandle);
            }
        }
        
        [ContextMenu("Clear")]
        public async UniTask ClearCacheAsync()
        {
            foreach (var clearLocator in Addressables.ResourceLocators)
            {
                var clearHandle = Addressables.ClearDependencyCacheAsync(clearLocator.Keys, true);
                await clearHandle.ToUniTask();
                
                if(clearHandle.IsValid())
                    Addressables.Release(clearHandle);
            }
            
            Caching.ClearCache();
        }

        public async UniTask<bool> ClearPartialAsync(object clearKey)
        {
            var clearHandle = Addressables.LoadResourceLocationsAsync(clearKey);
            var clearResult = await clearHandle.ToUniTask();
            foreach (var clearResource in clearResult)
            {
                Addressables.ClearDependencyCacheAsync(clearResource);
            }

            if(clearHandle.IsValid())
                Addressables.Release(clearHandle);
            
            return true;
        }
    }
}