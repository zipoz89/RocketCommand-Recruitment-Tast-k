using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBase : MonoBehaviour
{
    [SerializeField] private List<City> allCities;

    [HideInInspector]
    public List<City> aliveCities;

    [SerializeField] private PlayerArtillery altillery;

    public bool IsAlive { get => aliveCities.Count > 0 ? true : false; }

    private void Start()
    {
        foreach (var art in allCities)
        {
            art.OnCityDestroyed += CityDestroyed;
        }

        ReseLives();
    }

    private void ReseLives() 
    {
        foreach (var art in allCities)
        {
            aliveCities.Add(art);
            if (art is ArmedCity) 
            {
                altillery.UpdateCityState((ArmedCity)art, true);
            }
        }     
    }

    private void CityDestroyed(City art)
    {
        aliveCities.Remove(art);

        if (art is ArmedCity)
        {
            altillery.UpdateCityState((ArmedCity)art, false);
        }

        if (!CheckIfStillAlive())
        {
            Debug.Log("YOU DIED");
        }
    }

    public Vector2 GetRandomArtilleryTransform() 
    {
        int randomIndex = Random.Range(0, aliveCities.Count);
        return aliveCities[randomIndex].gameObject.transform.position;
    }

    private bool CheckIfStillAlive()
    {
        return aliveCities.Count > 0;
    }

    private void OnDestroy()
    {
        foreach (var art in allCities)
        {
            art.OnCityDestroyed -= CityDestroyed;
        }
    }


}
