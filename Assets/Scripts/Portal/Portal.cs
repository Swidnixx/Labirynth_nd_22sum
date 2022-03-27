using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    public Portal linkedPortal;
    public MeshRenderer renderPlane;
    Camera playerCam;
    Camera portalCam;
    RenderTexture viewTexture;

    PortalTeleport teleport;

    private void Awake()
    {
        playerCam = Camera.main;
        portalCam = GetComponentInChildren<Camera>();
        portalCam.enabled = false;

        teleport = GetComponentInChildren<PortalTeleport>();

        teleport.player = playerCam.transform.parent;
        teleport.receiver = linkedPortal.GetComponentInChildren<PortalTeleport>().transform;
    }

    void CreateViewTexture()
    {
        if (viewTexture == null || viewTexture.width != Screen.width || viewTexture.height != Screen.height)
        {
            if (viewTexture != null)
                viewTexture.Release();

            viewTexture = new RenderTexture(Screen.width, Screen.height, 0);

            portalCam.targetTexture = viewTexture;
            linkedPortal.renderPlane.material.SetTexture("_MainTex", viewTexture);
        }
    }

    public void Render()
    {
        renderPlane.enabled = false;
        CreateViewTexture();

        var m = transform.localToWorldMatrix * linkedPortal.transform.worldToLocalMatrix * 
            playerCam.transform.localToWorldMatrix;
        portalCam.transform.SetPositionAndRotation(m.GetColumn(3), m.rotation);
        //portalCam.transform.Rotate(Vector3.up * 180);

        portalCam.Render();
        renderPlane.enabled = true;
    }
}
