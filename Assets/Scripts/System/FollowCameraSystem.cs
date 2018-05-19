using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

public sealed class FollowCameraSystem : ComponentSystem
{
    struct PlayerGroup
    {
        [ReadOnly] ComponentDataArray<PlayerData> playerData;
        [ReadOnly] public ComponentDataArray<Position> position;
    }

    struct CameraGroup
    {
        ComponentArray<Camera> camera;
        public ComponentArray<Transform> transform;
    }

    [Inject] CameraGroup cameraGroup;
    [Inject] PlayerGroup playerGroup;

    protected override void OnUpdate()
    {
        cameraGroup.transform[0].localPosition = playerGroup.position[0].Value + new float3(0, 18.5f, -8.5f);
    }
}