using System.Collections.Generic;
using UnityEngine;

namespace PhantomEngine
{
    public class MeshCombiner : MonoBehaviour
    {
        public bool combineChildren = true;
        public bool disableCombinedChildren = true;

        
        void Start()
        {
            CombineMeshes();
        }
        
        
        private void CombineMeshes()
        {
            MeshFilter[] meshFilters = combineChildren ? GetComponentsInChildren<MeshFilter>() : GetComponents<MeshFilter>();
            
            List<CombineInstance> combineInstances = new List<CombineInstance>();
            List<Material> materials = new List<Material>(); // +

            foreach (MeshFilter mf in meshFilters)
            {
                if (mf.transform == transform)
                    continue;

                MeshRenderer mr = mf.GetComponent<MeshRenderer>();
                if(mr == null || mr.sharedMaterial == null)
                    continue;
                
                CombineInstance ci = new CombineInstance
                {
                    mesh = mf.sharedMesh,
                    transform = mf.transform.localToWorldMatrix
                };
                combineInstances.Add(ci);

                if (!materials.Contains(mr.sharedMaterial))
                {
                    materials.Add(mr.sharedMaterial);   
                }
            }
            
            Mesh combinedMesh = new Mesh();
            combinedMesh.CombineMeshes(combineInstances.ToArray(), true, true);
            
            MeshFilter parentMeshFilter = GetComponent<MeshFilter>();
            if (parentMeshFilter == null)
            {
                parentMeshFilter = gameObject.AddComponent<MeshFilter>();
            }
            parentMeshFilter.mesh = combinedMesh;
            
            MeshRenderer parentRenderer = GetComponent<MeshRenderer>();
            if (parentRenderer == null)
                parentRenderer = gameObject.AddComponent<MeshRenderer>();

            parentRenderer.materials = materials.ToArray();
            
            if (disableCombinedChildren)
            {
                foreach (MeshFilter mf in meshFilters)
                {
                    if (mf.transform == transform)
                        continue;
                    mf.gameObject.SetActive(false);
                }
            }
        }
    }
}