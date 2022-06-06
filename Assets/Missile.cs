using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missile : MonoBehaviour, IPoolableObject
{
    [SerializeField] protected float speed = 0.1f;
    [SerializeField] protected Rigidbody2D rb;
    [field: SerializeField]
    public Explosion Explosion { get; private set; }

    private bool isActive = false;
    public bool IsActive { get => isActive; }
    protected Vector2 target;

    [SerializeField] private float eplodeRadious = 1;
    [SerializeField] private float explodeTime = 0.5f;
    [SerializeField] private GameObject missileObject;

    protected bool isDetonating = false;

    private void SetActive(bool mode)
    {
        isActive = mode;
        this.gameObject.SetActive(mode);
    }

    public void Travel()
    {
        if (target == null || isDetonating)
        {
            return;
        }


        Vector2 newPos = Vector3.right * speed;

        this.transform.Translate(newPos* Time.deltaTime);
    }

    public void Send(Vector2 startPos, Vector2 target, float speedMultiplier)
    {
        this.target = target;
        this.transform.position = startPos;
        this.transform.right = (Vector3)target - transform.position;
        speed *= speedMultiplier;
    }

    #region IPoolableObject
    public void OnSpawned()
    {
        Explosion.gameObject.SetActive(false);
        missileObject.gameObject.SetActive(true);
        Explosion.OnExplosionEnded += MissileDetonated;
        SetActive(true);
    }

    public void OnGenerated()
    {
        SetActive(false);
    }

    public void OnReturned()
    {
        Explosion.OnExplosionEnded -= MissileDetonated;
        SetActive(false);
    }

    protected virtual void StartExplosion() 
    {
        if (isDetonating == true)
        {
            return;
        }
        isDetonating = true;
        missileObject.gameObject.SetActive(false);
        Explosion.gameObject.SetActive(true);
        rb.isKinematic = true;
        Explosion.Explode(eplodeRadious, explodeTime);
    }

    protected virtual void MissileDetonated()
    {
        rb.isKinematic = true;
        isDetonating = false;
    }

    #endregion
}
