using Unity.Collections;
using Unity.Entities;
using Unity.Transforms;
using Unity.Mathematics;
using Unity.Jobs;

public sealed class HitCheckSystem : JobComponentSystem
{
    struct PlayerGroup
    {
        [ReadOnly] public ComponentDataArray<Position> positions;
        [ReadOnly] public ComponentDataArray<PlayerData> playerData;
    }

    [Inject] PlayerGroup playerGroup;

    protected override JobHandle OnUpdate(JobHandle inputDeps) => new HitcheckJob()
    {
        playerPosition = playerGroup.positions[0]
    }.Schedule(this, 64, inputDeps);

    [ComputeJobOptimization]
    struct HitcheckJob : IJobProcessComponentData<Position, ItemData>
    {
        public Position playerPosition;

        public void Execute([ReadOnly] ref Position position, [WriteOnly] ref ItemData item)
        {
            if (math.distance(playerPosition.Value, position.Value) < 1)
            {
                item.isDestroy = 1;
            }
        }
    }
}
