using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// this class is responsible for player sending rockets
/// </summary>
public class PlayerArtillery : MonoBehaviour
{
    [SerializeField] private List<ArmedCity> aliveArmedCities = new List<ArmedCity>();
    [SerializeField] private Queue<ArmedCity> reloadedCities = new Queue<ArmedCity>();
    private CommandProcessor rocketProcessor = new CommandProcessor();
    [SerializeField] private float minYSendCoord;



    public void CustomUpdate()
    {
        SendRockets();

    }

    public void ResetCities() 
    {
        rocketProcessor.RestQueue();
        foreach (var city in aliveArmedCities)
        {
            city.OnRocketReloaded -= AddToReloadedCities;
        }
        aliveArmedCities = new List<ArmedCity>();
        reloadedCities = new Queue<ArmedCity>();
    }

    private void SendRockets() 
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 mousePos = new Vector2(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y);
            if (mousePos.y < minYSendCoord)
            {
                return;
            }
            SendRocketCommand rocketCommand = new SendRocketCommand(mousePos,this);
            rocketProcessor.ScheduleCommand(rocketCommand);
            if (reloadedCities.Count > 0) 
            {
                rocketProcessor.ExecuteCommand();
            }
        }
    }

    public void SendRocketAtCoords(Vector2 coord)
    {
        if (reloadedCities.Count > 0)
        {    
            var city = reloadedCities.Dequeue();
            city.SendRocket(coord);
        }
    }

    public void UpdateCityState(ArmedCity city, bool state) 
    {
        if (state == true)
        {
            if (!aliveArmedCities.Contains(city))
            {
                aliveArmedCities.Add(city);
                AddToReloadedCities(city);
                city.OnRocketReloaded += AddToReloadedCities;
            }
        }
        else 
        {
            aliveArmedCities.Remove(city);
            city.OnRocketReloaded -= AddToReloadedCities;
        }
    }

    private void AddToReloadedCities(ArmedCity city) 
    {
        if (!reloadedCities.Contains(city)) 
        {
            reloadedCities.Enqueue(city);
            if (rocketProcessor.HasCommandScheduled) 
            {
                rocketProcessor.ExecuteCommand();
            }
        }
    }
}
