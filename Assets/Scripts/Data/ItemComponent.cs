using Unity.Entities;

public sealed class ItemComponent : ComponentDataWrapper<ItemData>{}

[System.Serializable]
public struct ItemData : IComponentData
{
    public int isDestroy;
}