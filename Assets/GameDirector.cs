using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using Unity.Jobs;
using UnityEngine;

public class GameDirector : MonoBehaviour
{
    [SerializeField] private GameObject nukePrefab;
    [SerializeField] private GameObject explosionPrefab;
    [SerializeField] private Transform SpawnArea;
    [SerializeField] private PlayerFacade player;

    private GameobjectPooler<Nuke> nukePooler = new GameobjectPooler<Nuke>();
    private GameobjectPooler<Explosion> explosionPooler = new GameobjectPooler<Explosion>();

    private int difficultyLevel = 1;

    public delegate void OnDetonated(Nuke nuke);

    private void Start()
    {
        nukePooler.Initialize(nukePrefab);
        explosionPooler.Initialize(explosionPrefab);
    }
    private void Update()
    {
        HandleActiveNukes();
    }

    [ContextMenu("Get from pool")]
    public void SpawnNuke() 
    {
        Nuke nuke = nukePooler.Get();
        if (nuke != null)
        { 
            nuke.OnNukeDetonated += DetonateNuke;
            nuke.Send(GetRandomSpawnPos(),player.GetRandomArtilleryTransform(),1);
        }
    }

    public void SpawnExplosion(Vector2 pos)
    {
        Explosion boom = explosionPooler.Get();
        if (boom != null)
        {
            boom.OnExplosionEnded += ClearExplosion;
            boom.Explode(pos,2, 1);
        }
    }

    private void DetonateNuke(Nuke nuke)
    {
        SpawnExplosion(nuke.transform.position);
        nuke.OnNukeDetonated -= DetonateNuke;
        nukePooler.Return(nuke);
    }

    private void ClearExplosion(Explosion boom) 
    {
        boom.OnExplosionEnded -= ClearExplosion;
        explosionPooler.Return(boom);
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
        foreach (var nuke in nukePooler.active)
        {
            nuke.Fall();
        }
    }
}

