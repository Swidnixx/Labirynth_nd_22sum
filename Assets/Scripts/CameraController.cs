using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] float mouseSensivity = 5;
    Transform playerTransform;
    float xRot = 0;

    List<Portal> portals;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        playerTransform = transform.parent;

        portals = FindObjectsOfType<Portal>().ToList();
    }


    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * Time.deltaTime * mouseSensivity;
        float mouseY = Input.GetAxis("Mouse Y") * Time.deltaTime * mouseSensivity;

        xRot -= mouseY;
        xRot = Mathf.Clamp(xRot, -60, 60);

        transform.localRotation = Quaternion.Euler(xRot, 0, 0);
        playerTransform.Rotate(0, mouseX, 0);
    }

    private void OnPreCull()
    {
        foreach(Portal portal in portals)
        {
            portal.Render();
        }
    }
}
