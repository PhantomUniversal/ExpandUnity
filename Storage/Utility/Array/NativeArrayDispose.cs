using Unity.Collections;
using UnityEngine;

namespace PhantomEngine
{
    public class NativeArrayDispose : MonoBehaviour
    {
        private NativeArray<int> nativeArray;


        private void Start()
        {
            nativeArray = new NativeArray<int>(10, Allocator.Persistent);
        }

        private void OnDestroy()
        {
            if (nativeArray.IsCreated)
            {
                nativeArray.Dispose();
            }
        }
    }
}