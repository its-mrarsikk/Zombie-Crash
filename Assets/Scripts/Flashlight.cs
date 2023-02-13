using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class Flashlight : BarController
{
    public int maximumEnergy = 100;
    [ReadOnly] public bool lightEnabled = false;
    public int energy
    {
        get => energyLeft; private set { energyLeft = value; UpdateBar(); }
    }

    [SerializeField][ReadOnly] private int energyLeft;
    [SerializeField] private GameObject lightObject;
    private Light lt;
    private MultipleAudioClips audios;
    private sbyte clickCount = 0;
    public InputAction toggleFlashlight = new(binding: "<Mouse>/rightButton");

    void Awake() => toggleFlashlight.performed += _ => OnToggleLight();

    // Start is called before the first frame update
    void Start()
    {
        lt = lightObject.GetComponent<Light>();
        lt.enabled = false;
        energy = maximumEnergy;
        audios = GetComponent<MultipleAudioClips>();
    }

    public override void UpdateBar()
    {
        if (counter != null)
            counter.GetComponent<TMP_Text>().text = $"{NormalizeFloat(energy)}/{NormalizeFloat(maximumEnergy)}";

        float fillamt = energy / maximumEnergy;
        foregroundSprite.GetComponent<Image>().fillAmount = fillamt;
    }

    void OnToggleLight()
    {
        Debug.Log("toggle light");
        if (energy <= 0) { OnFullyDischarged(true); return; }

        if (lt.enabled)
        {
            lt.enabled = true;
            StartCoroutine(DischargeCoroutine());
            StopCoroutine(ChargeCoroutine());
        }
        else
        {
            lt.enabled = false;
            StartCoroutine(ChargeCoroutine());
            StopCoroutine(DischargeCoroutine());
        }
    }

    void OnFullyDischarged(bool click)
    {
        Debug.Log("fdischarged");
        if (click)
        {
            if (clickCount < 2)
            {
                audios.PlayClip(0);
                clickCount++;
            }
            else
            {
                audios.PlayClip(1);
                clickCount = 0;
            }
        }
        else audios.PlayClip(0);

        lt.enabled = false;
    }

    IEnumerator DischargeCoroutine()
    {
        if (energy <= 0)
        {
            OnFullyDischarged(false);
            yield break;
        }
        yield return new WaitForSeconds(0.5f);
        energy--;
    }

    IEnumerator ChargeCoroutine()
    {
        if (energy == maximumEnergy)
        {
            yield break;
        }
        yield return new WaitForSeconds(1);
        energy++;
    }
}