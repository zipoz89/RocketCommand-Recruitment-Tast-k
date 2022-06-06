using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class City : MonoBehaviour
{
    [SerializeField] private GameObject spriteObject;

    public delegate void CityDestroyed(City art);
    public event CityDestroyed OnCityDestroyed;

    private bool isActive = true;
    public bool IsActive { get => isActive;}

    public void DestoryMe() 
    {
        spriteObject.SetActive(false);
        OnCityDestroyed(this);
        isActive = false;
        Debug.Log("Destoryed",this);
    }

    public void Rebuild()
    {
        spriteObject.SetActive(true);
        isActive = true;
    }
}
