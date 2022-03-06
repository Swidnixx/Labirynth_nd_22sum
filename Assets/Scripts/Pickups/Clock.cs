using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clock : Pickup
{
    public int timeToAdd = 10;
    public override void Picked()
    {
        GameManager.instance.AddTime(timeToAdd);
        Destroy(gameObject);
    }
}
