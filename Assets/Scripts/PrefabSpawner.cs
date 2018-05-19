using UnityEngine;
using Unity.Entities;
using Unity.Transforms;
using Unity.Mathematics;

#if UNITY_EDITOR
using System.Linq;
using UnityEditor;
#endif

public sealed class PrefabSpawner : MonoBehaviour
{
    public GameObject prefab;

    [HideInInspector]
    public float3[] positions;

    private void Start()
    {
#if UNITY_EDITOR
        UnactiveAllChildren();
#endif
        var entityManager = World.Active.GetOrCreateManager<EntityManager>();
        for(int i=0; i<positions.Length; i++)
        {
            var entity = entityManager.Instantiate(prefab);
            entityManager.SetComponentData(entity, new Position() { Value = positions[i] });
        }
    }

#if UNITY_EDITOR
    [ContextMenu("Gather")]
    void Gather()
    {
        var transforms = GetComponentsInChildren<Transform>(true).ToList();
        transforms.Remove(transform);
        var count = transforms.Count();
        positions = new float3[count];
        for(int i =0; i< count; i++)
        {
            positions[i] = transforms[i].position;
            transforms[i].gameObject.tag = "EditorOnly";
        }
    }

    [ContextMenu("Unactive")]
    void UnactiveAllChildren()
    {
        var sampler = UnityEngine.Profiling.CustomSampler.Create("inactive gameobjects (editor overhead)");
        sampler.Begin(gameObject);

        var transforms = GetComponentsInChildren<Transform>().ToList();
        transforms.Remove(transform);
        foreach( var obj in transforms)
        {
            obj.gameObject.SetActive(false);
        }

        sampler.End();
    }

    [CustomEditor(typeof(PrefabSpawner))]
    [CanEditMultipleObjects]
    class PrefabSpawnerEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            foreach( var obj in targets)
            {
                var spawner = (PrefabSpawner)obj;
                GUILayout.Label(string.Format("stored:{0}", spawner.positions.Length));
                if(GUILayout.Button("Gather"))
                {
                    spawner.Gather();
                }
            }
        }
    }

#endif
}
