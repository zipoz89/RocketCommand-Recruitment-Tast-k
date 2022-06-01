using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Explosion : MonoBehaviour, IPoolableObject
{
    private bool isActive = false;
    public bool IsActive { get => isActive; }

    private float explodeTime;
    [SerializeField] private Gradient colorVariation;
    [SerializeField] private SpriteRenderer sprite;
    public delegate void ExplosionEnded(Explosion boom);
    public event ExplosionEnded OnExplosionEnded;



    public void Explode(Vector2 pos,float explodeRadius,float explodeTime) 
    {
        sprite.color = colorVariation.Evaluate(Random.Range(0,1));
        this.explodeTime = explodeTime;
        this.transform.position = pos;
        this.transform.DOScale(new Vector3(explodeRadius, explodeRadius, explodeRadius), explodeTime).SetEase(Ease.OutQuad).OnComplete(Implode);
    }

    private void Implode() 
    {
        this.transform.DOScale(new Vector3(0, 0, 0), explodeTime).SetEase(Ease.InQuad).OnComplete(Finish);
    }

    private void Finish() 
    {
        OnExplosionEnded(this);
    }

    private void SetActive(bool mode)
    {
        isActive = mode;
        this.gameObject.SetActive(mode);
    }

    #region IPoolableObject
    public void OnSpawned()
    {
        SetActive(true);
    }

    public void OnGenerated()
    {
        SetActive(false);
    }

    public void OnReturned()
    {
        SetActive(false);
    }
    #endregion IPoolableObject
}
