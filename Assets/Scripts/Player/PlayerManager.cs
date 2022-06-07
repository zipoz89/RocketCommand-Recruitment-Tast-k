using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This class is facade for player calsses
/// </summary>
public class PlayerManager : CustomController
{
    [SerializeField] private PlayerBase PlayerBase;

    public bool IsAlive { get => PlayerBase.IsAlive; }
    public int AliveCities { get => PlayerBase.AliveCities; }

    public event PlayerBase.AllCitiesDied OnAllCitiesDied
    {
        add => PlayerBase.OnAllCitiesDied += value;
        remove => PlayerBase.OnAllCitiesDied -= value;
    }

    public Vector2 GetRandomTarget() 
    {
        var t =  PlayerBase.GetRandomArtilleryTransform();
        return t;
    }

    public override void  Initialize() 
    {
        PlayerBase.Initialize();
    }

    public override void CustomUpdate()
    {
        PlayerBase.CustomUpdate();
    }

    public override void Deinitialzie()
    {
        PlayerBase.Deinitialize();
    }



}
