using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using Unity.Jobs;
using UnityEngine;

public class Nuke : Missile
{
    public delegate void NukeDetonated(Nuke nuke);
    public event NukeDetonated OnNukeDetonated;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent<City>(out City cit) && !isDetonating)
        {
            StartExplosion();
            cit.DestoryMe();
        }
        else if (collision.gameObject.GetComponent<Explosion>() != null)
        {
            StartExplosion();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<Missile>() != null)
        {
            StartExplosion();
        }   
    }

    protected override void MissileDetonated()
    {
        base.MissileDetonated();
        OnNukeDetonated(this);
    }


}
