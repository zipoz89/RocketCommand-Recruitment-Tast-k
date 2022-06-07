using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

/// <summary>
/// this class is responsible for explosion of rocket
/// </summary>
public class Explosion : MonoBehaviour
{
    private float explodeTime;
    [SerializeField] private Gradient colorVariation;
    [SerializeField] private SpriteRenderer sprite;
    public delegate void ExplosionEnded();
    public event ExplosionEnded OnExplosionEnded;

    public void Explode(float explodeRadius,float explodeTime) 
    {
        sprite.color = colorVariation.Evaluate(Random.Range(0f,1f));
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
