using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFacade : MonoBehaviour
{
    [SerializeField] private PlayerBase PlayerBase;

    public Transform GetRandomArtilleryTransform() 
    {
        var t =  PlayerBase.GetRandomArtilleryTransform();
        Debug.Log(t.position);
        return t;
    }
}
