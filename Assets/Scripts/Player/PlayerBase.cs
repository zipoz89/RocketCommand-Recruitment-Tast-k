using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This class holds references and controlls all cities
/// </summary>
public class PlayerBase : MonoBehaviour
{
    [SerializeField] private List<City> allCities;

    private List<City> aliveCities;

    [SerializeField] private PlayerArtillery artillery;

    public bool IsAlive { get => aliveCities.Count > 0 ? true : false; }
    public int AliveCities { get => aliveCities.Count; }

    public delegate void AllCitiesDied();
    public event AllCitiesDied OnAllCitiesDied;



    public void Initialize()
    {
        foreach (var art in allCities)
        {
            art.OnCityDestroyed += CityDestroyed;
        }

        ReseLives();
    }

    public void CustomUpdate() 
    {
        artillery.CustomUpdate();
    }

    private void ReseLives() 
    {
        aliveCities = new List<City>();
        artillery.ResetCities();

        foreach (var city in allCities)
        {
            city.Rebuild();
            aliveCities.Add(city);
            if (city is ArmedCity) 
            {
                artillery.UpdateCityState((ArmedCity)city, true);
            }
        }     
    }

    private void CityDestroyed(City art)
    {
        aliveCities.Remove(art);


        if (art is ArmedCity)
        {
            artillery.UpdateCityState((ArmedCity)art, false);
        }

        if (!CheckIfStillAlive())
        {
            OnAllCitiesDied();
        }
    }

    public Vector2 GetRandomArtilleryTransform() 
    {
        int randomIndex = Random.Range(0, aliveCities.Count);
        return aliveCities[randomIndex].gameObject.transform.position;
    }

    public bool CheckIfStillAlive()
    {
        return aliveCities.Count > 0;
    }

    public void Deinitialize()
    {
        foreach (var art in allCities)
        {
            art.OnCityDestroyed -= CityDestroyed;
        }
    }


}
