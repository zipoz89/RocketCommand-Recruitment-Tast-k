using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using Unity.Jobs;
using UnityEngine;

/// <summary>
/// this class is responsible for spawning enemy rockets
/// </summary>
public class EnemyDirector : CustomController
{
    [SerializeField] private GameObject nukePrefab;
    [SerializeField] private Transform SpawnArea;
    [SerializeField] private PlayerManager player;

    [SerializeField] private float spawnInterval = 1f;

    private float timeFromLastSpawn = 0;
    private int nukesToSpawnThisRound = 1;
    private int nukesSpawnded = 0;
    private int maxEnemies = 0;
    private float speedMultiplier = 1;

    private int spawnedEnemies { get => nukePooler.activeObjects.Count; }

    private GameobjectPooler<Nuke> nukePooler = new GameobjectPooler<Nuke>();

    public delegate void AllNukesSend();
    public event AllNukesSend OnAllNukesSend;



    public override void Initialize()
    {
        nukesSpawnded = 0;
        timeFromLastSpawn = 0;
        nukePooler.Initialize(nukePrefab);
        timeFromLastSpawn = spawnInterval;
    }
    public override void CustomUpdate()
    {
        SpawnNukesIfAble();
        HandleActiveNukes();

        if (nukesSpawnded >= nukesToSpawnThisRound && nukePooler.activeObjects.Count == 0)
        {
            OnAllNukesSend();
        }
    }

    public override void Deinitialzie() 
    {
        for (int i = nukePooler.activeObjects.Count - 1; i >= 0; i--)
        {
            nukePooler.Return(nukePooler.activeObjects[i]);
        }

    }

    public void SetRoundParams(int nukes,float speed, int maxNukes) 
    {
        nukesToSpawnThisRound = nukes;
        speedMultiplier = speed;
        maxEnemies = maxNukes;
    }

    private void SpawnNukesIfAble()
    {
        if (spawnedEnemies < maxEnemies && player.IsAlive && nukesSpawnded < nukesToSpawnThisRound)
        {
            if (timeFromLastSpawn <= 0)
            {
                timeFromLastSpawn = spawnInterval;
                SpawnNuke();
            }
            else 
            {
                timeFromLastSpawn -= Time.deltaTime;
            }
        }
    }

    private void SpawnNuke() 
    {
        Nuke nuke = nukePooler.Get();
        if (nuke != null)
        {
            nukesSpawnded++;
            nuke.OnNukeDetonated += DetonateNuke;
            nuke.Send(GetRandomSpawnPos(),player.GetRandomTarget(), speedMultiplier);
        }
    }



    private void DetonateNuke(Nuke nuke)
    {
        nuke.OnNukeDetonated -= DetonateNuke;
        nukePooler.Return(nuke);
    }

    private Vector2 GetRandomSpawnPos() 
    {
        return new Vector2(Random.Range(SpawnArea.position.x - SpawnArea.localScale.x / 2, SpawnArea.position.x + SpawnArea.localScale.x / 2), SpawnArea.position.y);
    }

    private void HandleActiveNukes() 
    {
        foreach (var nuke in nukePooler.activeObjects)
        {
            nuke.Travel();
        }
    }
}

