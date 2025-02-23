using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace PhantomEngine
{
    public class TitlePatch : MonoBehaviour
    {
        [SerializeField] private List<AssetLabelReference> PatchLabels = new();
        [SerializeField] private UISlider PatchSlider;
        
        
        public async UniTask UpdateContentAsync()
        {            
            var patchInit = await InitializeContentAsync();
            if (!patchInit)
            {
                return;
            }
            
            var patchCatalog = await CheckCatalogAsync();
            if (patchCatalog.Count > 0)
            {
                await UpdateCatalogAsync(patchCatalog);
            }

            var patchList = ConvertLabelReference(PatchLabels);
            var patchSize = await TotalSizeAsync(patchList);
            if (patchSize <= 0) 
                return;

            // Patch mb size = $"{patchSize / 1024 / 1024:F} MB"; 
            await DownloadProgressAsync(patchList);
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
            PatchSlider.gameObject.SetActive(true);
            
            foreach (var downloadKey in downloadList)
            {
                long downloadSize = await PartialSizeAsync(downloadKey);
                if (downloadSize == 0) 
                    continue;
                
                var downloadHandle = Addressables.DownloadDependenciesAsync(downloadKey);
                while (!downloadHandle.IsDone)
                {
                    float downloadPercent = downloadHandle.PercentComplete;
                    PatchSlider.SetValue(downloadPercent);
                    //int downloadValue = (int)(downloadSize * downloadPercent);
                    await UniTask.Yield();
                }
                    
                if(downloadHandle.IsValid())
                    Addressables.Release(downloadHandle);
            }
            
            PatchSlider.gameObject.SetActive(false);
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