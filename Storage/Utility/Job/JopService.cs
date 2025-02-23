using Unity.Collections;
using Unity.Jobs;
using UnityEngine;
using UnityEngine.Jobs;

namespace PhantomEngine
{
    public class JopService : MonoBehaviour
    {
        private Transform[] transformsArray;
        private TransformAccessArray transformAccessArray;

        
        private void Start()
        {
            transformAccessArray = new TransformAccessArray(transformsArray);    
        }

        private void Update()
        {
            Rotate();
        }

        private void OnDestroy()
        {
            transformAccessArray.Dispose();    
        }
        
        
        private void Rotate()
        {
            JobRotate jobDate = new JobRotate()
            {
                Timer = Time.time,
                Speed = 2f,
                Radius = 0.05f
            };

            JobHandle jobHandle = jobDate.Schedule(transformAccessArray);
        }
        
        private void Add()
        {
            NativeArray<float> jobResult = new NativeArray<float>(1, Allocator.TempJob);
            JobAdd jobData = new JobAdd
            {
                A = 10f,
                B = 10f,
                Result = jobResult
            };

            JobHandle jobHandle = jobData.Schedule();
            jobHandle.Complete();
            
            Debug.Log(jobResult[0]);
            jobResult.Dispose();
        }
    }
}