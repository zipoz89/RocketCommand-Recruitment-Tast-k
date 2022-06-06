using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFacade : MonoBehaviour
{
    [SerializeField] private PlayerBase PlayerBase;

    public bool IsAlive { get => PlayerBase.IsAlive; }

    public Vector2 GetRandomTarget() 
    {
        var t =  PlayerBase.GetRandomArtilleryTransform();
        return t;
    }


}
