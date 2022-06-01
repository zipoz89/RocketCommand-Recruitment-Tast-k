using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBase : MonoBehaviour
{
    [SerializeField] private List<City> allCities;

    [HideInInspector]
    public List<City> aliveCities;

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
        }     
    }

    private void CityDestroyed(City art)
    {
        aliveCities.Remove(art);
        if (!CheckIfStillAlive())
        {
            Debug.Log("YOU DIED");
        }
    }

    public Transform GetRandomArtilleryTransform() 
    {
        int randomIndex = Random.Range(0, aliveCities.Count);
        return aliveCities[randomIndex].gameObject.transform;
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
