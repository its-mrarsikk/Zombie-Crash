using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[AddComponentMenu("Combat/Health/Health Bar Counter")]
public class HealthCounter : MonoBehaviour
{
    public GameObject player;
    private HealthSystem hs;

    void Start()
    {
        hs = player.GetComponent<HealthSystem>();
    }
    // Update is called once per frame
    void Update()
    {
        GetComponent<TMP_Text>().text = $"{hs.health}/{hs.maximumHealth}";
    }
}
