using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float speed = 12f;
    [SerializeField] LayerMask groundMask;

    Vector3 velocity;
    CharacterController controller;

    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        PlayerMove();
        GroundCheck();
    }

    void GroundCheck()
    {
        RaycastHit hit;
        if(Physics.Raycast(transform.position, Vector3.down, out hit, 1.5f, groundMask))
        {
            string groundType = hit.collider.tag;
            //print(groundType);

            switch(groundType)
            {
                case "GroundSlow":
                    speed = 7;
                    break;

                case "GroundFast":
                    speed = 20;
                    break;

                default:
                    speed = 12;
                    break;
            }
        }
    }

    void PlayerMove()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = (z * transform.forward) + (x * transform.right);
        controller.Move(move * speed * Time.deltaTime);
    }
}
