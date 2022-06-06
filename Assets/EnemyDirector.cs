using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using Unity.Jobs;
using UnityEngine;

public class EnemyDirector : MonoBehaviour
{
    [SerializeField] private GameObject nukePrefab;
    [SerializeField] private Transform SpawnArea;
    [SerializeField] private PlayerFacade player;

    [SerializeField] private float spawnInterval = 1f;
    private float timeFromLastSpawn = 0;

    private int spawnedEnemies { get => nukePooler.activeObjects.Count; }
    private int maxEnemies 
    {
        get 
        {
            return difficultyLevel + 2;
        }
    }

    private GameobjectPooler<Nuke> nukePooler = new GameobjectPooler<Nuke>();

    private int difficultyLevel = 1;

    public delegate void OnDetonated(Nuke nuke);

    private void Start()
    {
        nukePooler.Initialize(nukePrefab);
        timeFromLastSpawn = spawnInterval;
    }
    private void Update()
    {
        SpawnNukesIfAble();
        HandleActiveNukes();
    }

    private void SpawnNukesIfAble()
    {
        if (spawnedEnemies < maxEnemies && player.IsAlive)
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

    [ContextMenu("Get from pool")]
    public void SpawnNuke() 
    {
        Nuke nuke = nukePooler.Get();
        if (nuke != null)
        { 
            nuke.OnNukeDetonated += DetonateNuke;
            nuke.Send(GetRandomSpawnPos(),player.GetRandomTarget(),1);
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

    public void IncrementeDifficulty(int i = 1) 
    {
        difficultyLevel += i;
    }

    private void HandleActiveNukes() 
    {
        foreach (var nuke in nukePooler.activeObjects)
        {
            nuke.Travel();
        }
    }
}

