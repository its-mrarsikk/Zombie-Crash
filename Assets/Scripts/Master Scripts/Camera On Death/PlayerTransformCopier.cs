using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTransformCopier : MonoBehaviour
{
    public Transform playerTransform;
    public static GameObject copy { get; private set; }

    void Start()
    {
        copy = gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if (playerTransform == null) return;

        transform.SetPositionAndRotation(playerTransform.position, playerTransform.rotation);
    }
}
