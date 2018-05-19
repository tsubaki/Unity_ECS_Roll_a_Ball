using Unity.Entities;
using Unity.Transforms;
using Unity.Collections;
using Unity.Mathematics;
using UnityEngine;
using Unity.Jobs;

public sealed class ItemRotationSystem : JobComponentSystem
{
    protected override JobHandle OnUpdate(JobHandle inputDeps) =>  new RotationJob()
    {
        time = Time.timeSinceLevelLoad
    }.Schedule(this, 64, inputDeps);

    [ComputeJobOptimization]
    struct RotationJob : IJobProcessComponentData<Rotation, ItemData>
    {
        public float time;
        public void Execute([WriteOnly] ref Rotation rotation, [ReadOnly] ref ItemData item)
        {
            var q = math.axisAngle(new float3(0, 1, 0), time);
            rotation.Value = q;
        }
    }
}