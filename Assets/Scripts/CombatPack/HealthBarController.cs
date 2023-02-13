using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Canvas))]
[AddComponentMenu("Combat/Health/Health Bar Controller")]
public class HealthBarController : BarController
{
    //[Header("Resources")]
    public GameObject player;
    private Camera cam;
    private bool isOnPlayer = false;
    private HealthSystem healthsys;

    void Start()
    {
        cam = Camera.main;
        canvas = foregroundSprite.transform.parent.GetComponentInParent<Canvas>();
        if (transform.parent.name == "PlayerUI") isOnPlayer = true;
        healthsys = resourceOwner.GetComponent<HealthSystem>();
    }

    public override void UpdateBar()
    {
        if (counter != null)
            counter.GetComponent<TMP_Text>().text = $"{NormalizeFloat(healthsys.health)}/{NormalizeFloat(healthsys.maximumHealth)}";

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
