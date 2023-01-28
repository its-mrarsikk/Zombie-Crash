using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallOutOfWorld : MonoBehaviour
{
    [ReadOnly] public int destroyHeight;
    private bool healthOwner;
    private HealthSystem healthSystem;


    void Start()
    {
        healthSystem = GetComponent<HealthSystem>();
        healthOwner = healthSystem != null;
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y > destroyHeight) return;

        if (healthOwner) healthSystem.Damage(Mathf.Infinity);
        else Destroy(gameObject);
    }
}
