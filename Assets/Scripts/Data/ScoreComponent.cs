using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;


public sealed class ScoreComponent : ComponentDataWrapper<ScoreData> { }

[System.Serializable]
public struct ScoreData : IComponentData { }