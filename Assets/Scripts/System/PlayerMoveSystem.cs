using UnityEngine;
using Unity.Entities;

public sealed class PlayerMoveSystem : ComponentSystem
{
    struct Group
    {
        public ComponentArray<Rigidbody> rigidbodys;
        public ComponentDataArray<PlayerData> playerData;
    }

    [Inject] Group group;

    protected override void OnUpdate()
    {
        var x = Input.GetAxis("Horizontal");
        var y = Input.GetAxis("Vertical");

        group.rigidbodys[0].AddForce(x, 0, y);
    }
}
