using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Explosion : MonoBehaviour
{
    private float explodeTime;
    [SerializeField] private Gradient colorVariation;
    [SerializeField] private SpriteRenderer sprite;
    public delegate void ExplosionEnded();
    public event ExplosionEnded OnExplosionEnded;

    public void Explode(float explodeRadius,float explodeTime) 
    {
        sprite.color = colorVariation.Evaluate(Random.Range(0,1));
        this.explodeTime = explodeTime;
        this.transform.DOScale(new Vector3(explodeRadius, explodeRadius, explodeRadius), explodeTime).SetEase(Ease.OutQuad).OnComplete(Implode);
    }

    private void Implode() 
    {
        this.transform.DOScale(new Vector3(0, 0, 0), explodeTime).SetEase(Ease.InQuad).OnComplete(Finish);
    }

    private void Finish() 
    {
        OnExplosionEnded();
    }
}
