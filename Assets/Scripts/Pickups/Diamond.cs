using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Diamond : Pickup
{
    public int points = 5;
    public override void Picked()
    {
        GameManager.instance.AddPoints(points);
        Destroy(this.gameObject);
    }
}
