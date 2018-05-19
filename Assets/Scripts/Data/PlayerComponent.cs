using Unity.Entities;

public sealed class PlayerComponent : ComponentDataWrapper<PlayerData> { }

[System.Serializable]
public struct PlayerData : IComponentData{}