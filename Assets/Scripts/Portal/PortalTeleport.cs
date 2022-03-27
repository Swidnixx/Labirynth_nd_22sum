using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalTeleport : MonoBehaviour
{
    public Transform receiver;
    public Transform player;

    bool playerIsPassing;

    void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            playerIsPassing = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerIsPassing = false;
        }
    }

    private void FixedUpdate()
    {
        Teleport();
        DrawVectors();
    }

    void DrawVectors()
    {
        Debug.DrawLine(transform.position, player.position);
    }

    void Teleport()
    {
        if (playerIsPassing)
        {
            Vector3 portalToPlayer = player.position - transform.position;
            Vector3 portalDirection = transform.up;
            float dotProduct = Vector3.Dot(portalDirection, portalToPlayer);

            if(dotProduct < 0)
            {
                //float rotationDiff = -Quaternion.Angle(transform.rotation, receiver.rotation);
                //rotationDiff += 180;
                //Vector3 positionOffset = Quaternion.Euler(0, rotationDiff, 0) * portalToPlayer;
                //player.Rotate(Vector3.up, rotationDiff);
                player.position = new Vector3(receiver.position.x, player.position.y, receiver.position.z);
                player.rotation = Quaternion.LookRotation(receiver.up);
                playerIsPassing = false;
            }
        }
    }
}
