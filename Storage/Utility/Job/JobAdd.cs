using Unity.Burst;
using Unity.Collections;
using Unity.Jobs;

namespace PhantomEngine
{
    [BurstCompile]
    public struct JobAdd : IJob
    {
        public float A;
        public float B;
        [WriteOnly] public NativeArray<float> Result;
        
        public void Execute()
        {
            Result[0] = A + B;
        }
    }
}