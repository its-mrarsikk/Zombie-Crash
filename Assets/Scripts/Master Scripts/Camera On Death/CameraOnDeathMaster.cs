using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CameraOnDeathMaster : MonoBehaviour
{
    private Camera cam;
    private Transform plrT;
    public GameObject player;

    void Start()
    {
        cam = Camera.main;
        plrT = player.transform;
    }

    void Update()
    {
        if (plrT == null)
        {
            var codb = cam.AddComponent<CameraOnDeathBehaviour>();
            codb.target = PlayerTransformCopier.copy.transform;
            codb.enabled = true;

            enabled = false;
        }
    }
}
