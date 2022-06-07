using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// this class is responsible for city that can send rockets logic
/// </summary>
public class ArmedCity : City
{
    [SerializeField] private float reloadTime = 2;
    [SerializeField] private GameObject rocketPrefab;

    private GameobjectPooler<Rocket> rocketPooler = new GameobjectPooler<Rocket>();

    public delegate void RocketReloaded(ArmedCity city);
    public event RocketReloaded OnRocketReloaded;

    private bool hasRocketLoaded = true;
    public bool HasRocketLoaded { get => hasRocketLoaded; }

    private float currentReloadTime;

    private void Start()
    {
        rocketPooler.Initialize(rocketPrefab, 3);
        currentReloadTime = reloadTime;
    }

    private void Update()
    {
        foreach (var rocket in rocketPooler.activeObjects)
        {
            rocket.TravelToTarget();
        }
    }

    public void SpawnRocket(Vector2 target)
    {
        Rocket rocket = rocketPooler.Get();
        if (rocket != null)
        {
            rocket.Send(this.transform.position, target, 1);
            rocket.OnRocketDetonated += RocketDetonated;
        }
    }

    private void RocketDetonated(Rocket rocket)
    {
        rocket.OnRocketDetonated -= RocketDetonated;
        rocketPooler.Return(rocket);
    }

    private IEnumerator Reload() 
    {
        currentReloadTime = 0;
        hasRocketLoaded = false;
        while (currentReloadTime < reloadTime)
        {
            currentReloadTime += Time.deltaTime;
            yield return null;
        }
        if (OnRocketReloaded != null)
        { 
            OnRocketReloaded(this);
        }
        hasRocketLoaded = true;
    }

    public void SendRocket(Vector2 coord) 
    {
        SpawnRocket(coord);
        StartCoroutine(Reload());
    }


}
