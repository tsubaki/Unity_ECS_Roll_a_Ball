using Unity.Collections;
using Unity.Entities;
using Unity.Jobs;
using Unity.Transforms;
using UnityEngine;

[UpdateAfter(typeof(HitCheckSystem))]
public sealed class ItemDestroySystem : ComponentSystem
{
    struct Group
    {
        internal int Length;
        [ReadOnly] internal ComponentDataArray<ItemData> items;
        [ReadOnly] internal ComponentDataArray<Position> position;
        [WriteOnly] internal EntityArray entities;
    }

    struct DestroyParticleGroup
    {
        internal ComponentArray<ParticleSystem> particle;
    }

    [Inject] Group group;
    [Inject] DestroyParticleGroup effect;
    [Inject] EndFrameBarrier m_endFrameBarrier;

    protected override void OnUpdate()
    {
        var p = effect.particle[0];
        for (int i=0; i<group.Length; i++)
        {
            if( group.items[i].isDestroy == 1)
            {
                p.transform.position = group.position[i].Value;
                p.Emit(30);

                PostUpdateCommands.DestroyEntity(group.entities[i]);
            }
        }
    }
}
