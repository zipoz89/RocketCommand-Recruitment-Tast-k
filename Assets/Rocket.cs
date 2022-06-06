using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : Missile
{
    public delegate void RocketDetonated(Rocket rocket);
    public event RocketDetonated OnRocketDetonated;

    public void TravelToTarget() 
    {
        if (this.transform.position.y < target.y)
        {
            Travel();
        }
        else 
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
        OnRocketDetonated(this);
    }
}
