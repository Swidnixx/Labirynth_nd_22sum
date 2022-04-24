using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorMechanim : MonoBehaviour
{
    public float speed = 1;
    public Transform door;
    public Transform closedPos;
    public Transform openPos;
    public bool open = false;

    void Start()
    {
        door.position = closedPos.position;
    }

    void Update()
    {
        if(open && !Mathf.Approximately(Vector3.Distance(door.position, openPos.position), 0))
        {
            door.position = Vector3.MoveTowards(door.position, openPos.position, Time.deltaTime * speed);
        }
        if (!open && !Mathf.Approximately(Vector3.Distance(door.position, closedPos.position), 0))
        {
            door.position = Vector3.MoveTowards(door.position, closedPos.position, Time.deltaTime * speed);
        }
    }
}
