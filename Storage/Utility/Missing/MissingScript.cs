#if UNITY_EDITOR

using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace PhantomEditor
{
    public static class MissingScript
    {
        [MenuItem("Phantom/Utility/MissingScript/All")]
        private static void RemoveAllMissingScripts()
        {
            string[] prefabGuids = AssetDatabase.FindAssets("t:prefab");
            if (prefabGuids.Length == 0)
            {
                Debug.Log("The prefab does not exist.");
                return;
            }
            
            IEnumerable<string> prefabPaths = prefabGuids.Select(AssetDatabase.GUIDToAssetPath);
            IEnumerable<GameObject> prefabObjects = prefabPaths.Select(AssetDatabase.LoadAssetAtPath<GameObject>);
            RemoveMissingScript(prefabObjects.ToArray());
            Debug.Log("Remove all missing scripts from prefab");
        }
        
        [MenuItem("Phantom/Utility/MissingScript/Selection")]
        private static void RemoveSelectionMissingScript()
        {
            if (Selection.activeObject == null)
            {
                Debug.Log("The selected object does not exist.");
                return;
            }

            GameObject[] targetObjects = GetAllChildren(Selection.gameObjects);
            RemoveMissingScript(targetObjects);
            Debug.Log("Remove missing scripts from selection objects");
        }
        
        [MenuItem("Phantom/Utility/MissingScript/Scene")]
        private static void RemoveSceneMissingScript()
        {
            Scene targetScene = SceneManager.GetActiveScene();
            if (!targetScene.IsValid())
                return;
                
            GameObject[] targetObjects = targetScene.GetRootGameObjects();
            RemoveMissingScript(targetObjects);
            Debug.Log("Remove missing scripts from scene objects");
        }

        private static void RemoveMissingScript(params GameObject[] targetObjects)
        {
            for (int i = 0; i < targetObjects.Length; i++)
            {
                EditorUtility.DisplayProgressBar("Progressing remove missing script", $"{i}/{targetObjects.Length}", i / (float)targetObjects.Length);
                
                int count = GameObjectUtility.GetMonoBehavioursWithMissingScriptCount(targetObjects[i]);
                if(count == 0)
                    continue;

                GameObjectUtility.RemoveMonoBehavioursWithMissingScript(targetObjects[i]);
                EditorUtility.SetDirty(targetObjects[i]);

                if (EditorUtility.IsPersistent(targetObjects[i]) && PrefabUtility.IsAnyPrefabInstanceRoot(targetObjects[i]))
                {
                    PrefabUtility.SavePrefabAsset(targetObjects[i]);
                }
            }
            
            EditorUtility.ClearProgressBar();
        }
        
        private static GameObject[] GetAllChildren(GameObject[] targetObjects)
        {
            var targetTransforms = new List<Transform>();
            foreach (var target in targetObjects)
            {
                targetTransforms.AddRange(target.GetComponentsInChildren<Transform>(true));
            }
            
            return targetTransforms.Distinct().Select(x => x.gameObject).ToArray();
        }
    }
}

#endif