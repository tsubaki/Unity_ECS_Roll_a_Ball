using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] PrefabSpawner coinSpawner;
    [SerializeField] GameObject click2start, ui, clear;

    IEnumerator onUpdateCoroutine;

    void Awake()
    {
        onUpdateCoroutine = OnUpdate();
    }

    void OnEnable()
    {
        StartCoroutine(onUpdateCoroutine);
    }

    void OnDisable()
    {
        StopCoroutine(onUpdateCoroutine);
    }

    IEnumerator OnUpdate()
    {
        // game start
        var world = World.Active;
        var entityManager = world.GetOrCreateManager<EntityManager>();

        world.GetOrCreateManager<UpdateScoreSystem>().Enabled = false;
        world.GetOrCreateManager<PlayerMoveSystem>().Enabled = false;
        click2start.SetActive(true);

        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Space));

        // playing
        ui.SetActive(true);
        coinSpawner.gameObject.SetActive(true);
        click2start.SetActive(false);
        var group = entityManager.CreateComponentGroup(typeof(ItemData));
        world.GetOrCreateManager<PlayerMoveSystem>().Enabled =true;
        world.GetOrCreateManager<UpdateScoreSystem>().Enabled = true;

        yield return new WaitUntil(() => group.CalculateLength() == 0);

        //game clear

        world.GetOrCreateManager<UpdateScoreSystem>().Enabled = false;
        world.GetOrCreateManager<PlayerMoveSystem>().Enabled = false;
        clear.SetActive(true);
        ui.SetActive(false);
    }
}
