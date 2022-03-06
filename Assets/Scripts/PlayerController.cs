using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float speed = 12f;
    [SerializeField] LayerMask groundMask;

    CharacterController controller;
    float velocityY;
    bool grounded;

    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        PlayerMove();
        GroundCheck();
    }

    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if(hit.gameObject.CompareTag("Pickup"))
        {
            hit.gameObject.GetComponent<Pickup>().Picked();
        }
    }

    void GroundCheck()
    {
        RaycastHit hit;
        if(Physics.Raycast(transform.position, Vector3.down, out hit, 1.1f, groundMask))
        {
            grounded = true;

            string groundType = hit.collider.tag;
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
        else
        {
            grounded = false;
        }
    }

    void PlayerMove()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = (z * transform.forward) + (x * transform.right);
        controller.Move(move * speed * Time.deltaTime);

        if(!grounded)
        {
            velocityY += 10 * Time.deltaTime;
            if (velocityY > 30) velocityY = 30;
            controller.Move(Vector3.down * velocityY * Time.deltaTime);
        }
        else
        {
            velocityY = 0;
        }
    }
}
