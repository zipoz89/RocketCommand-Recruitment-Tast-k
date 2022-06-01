using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class City : MonoBehaviour
{
    [SerializeField] private GameObject spriteObject;

    public delegate void CityDestroyed(City art);
    public event CityDestroyed OnCityDestroyed;

    public void DestoryMe() 
    {
        spriteObject.SetActive(false);
        OnCityDestroyed(this);
        Debug.Log("Destoryed",this);
    }
}
