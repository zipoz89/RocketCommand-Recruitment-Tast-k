using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using Unity.Jobs;
using UnityEngine;

public class Nuke : MonoBehaviour, IPoolableObject
{
    [SerializeField] float speed = 0.1f;
    [SerializeField] Rigidbody2D rb;
    private bool isActive = false;
    public bool IsActive { get => isActive;}

    private Transform target;

    public delegate void NukeDetonated(Nuke art);
    public event NukeDetonated OnNukeDetonated;

    private void SetActive(bool mode) 
    {
        isActive = mode;
        this.gameObject.SetActive(mode);
    }

    public void Send(Vector2 startPos,Transform target,float speedMultiplier) 
    {
        Debug.Log(target.position);
        this.target = target;
        this.transform.position = startPos;
        this.transform.right = target.position - transform.position;
        speed *= speedMultiplier;
    }

    public void Fall() 
    {
        if (target == null)
        {
            Debug.LogWarning("This nuke has no target", this);
            return;
        }

        Vector2 newPos = this.transform.right * speed;

        rb.MovePosition((Vector2)this.transform.position + newPos);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent<City>(out City cit))
        {
            cit.DestoryMe();
            OnNukeDetonated(this);
        }
        else if (collision.gameObject.TryGetComponent<Explosion>(out Explosion boom))
        {
            OnNukeDetonated(this);
        }
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

    #endregion
}
