using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
