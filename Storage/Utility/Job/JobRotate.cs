using Unity.Burst;
using UnityEngine;
using UnityEngine.Jobs;

namespace PhantomEngine
{
    [BurstCompile]
    public struct JobRotate : IJobParallelForTransform
    {
        public float Timer;
        public float Speed;
        public float Radius;

        public void Execute(int index, TransformAccess transform)
        {
            Vector3 pos = transform.localPosition;
            transform.position = new Vector3(
                pos.x + Mathf.Sin(Timer * Speed) * Radius,
                pos.y,
                pos.z + Mathf.Cos(Timer * Speed) * Radius
                );
        }
    }
}