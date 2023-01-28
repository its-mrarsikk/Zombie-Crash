using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[AddComponentMenu("Combat/Health/Health Bar Controller")]
public class HealthBarController : MonoBehaviour
{
    [Header("Graphics")]
    public GameObject foregroundSprite;

    [Header("Health System")]
    public GameObject player;
    public GameObject healthSystemOwner;
    private Camera cam;
    private Canvas canvas;
    private bool isOnPlayer = false;

    void Start()
    {
        cam = Camera.main;
        canvas = foregroundSprite.transform.parent.GetComponentInParent<Canvas>();
        if (transform.parent.name == "PlayerUI") isOnPlayer = true;
    }

    public void UpdateHealthBar()
    {
        HealthSystem healthsys = healthSystemOwner.GetComponent<HealthSystem>();
        float fillamt = healthsys.health / healthsys.maximumHealth;
        foregroundSprite.GetComponent<Image>().fillAmount = fillamt;
    }

    void Update()
    {
        if (isOnPlayer || player == null) return;

        transform.rotation = Quaternion.LookRotation(transform.position - cam.transform.position);
        if (Vector3.Distance(player.transform.position, transform.parent.position) > 10)
        {
            canvas.enabled = false;
        }
        else canvas.enabled = true;
    }
}
