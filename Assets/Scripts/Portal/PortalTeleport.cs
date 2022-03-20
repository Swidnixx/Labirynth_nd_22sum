using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalTeleport : MonoBehaviour
{
    [SerializeField] Transform receiver;
    [SerializeField] Transform player;

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
        playerIsPassing = false;
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
                float rotationDiff = -Quaternion.Angle(transform.rotation, receiver.rotation);
                rotationDiff += 180;
                Vector3 positionOffset = Quaternion.Euler(0, rotationDiff, 0) * portalToPlayer;
                player.Rotate(Vector3.up, rotationDiff);
                player.position = receiver.position + positionOffset;
                playerIsPassing = false;
            }
        }
    }
}
