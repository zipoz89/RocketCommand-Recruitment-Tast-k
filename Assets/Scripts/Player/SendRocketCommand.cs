using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Command that sends rocket at coordinates
/// </summary>
public class SendRocketCommand : Command
{
    private Vector2 coord;
    private PlayerArtillery art;

    public SendRocketCommand(Vector2 coord, PlayerArtillery art) 
    {
        this.coord = coord;
        this.art = art;
    }

    public override void Execute()
    {
        art.SendRocketAtCoords(coord);
    }
}
