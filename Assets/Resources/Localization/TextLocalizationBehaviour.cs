using TMPro;
using UnityEngine;

[RequireComponent(typeof(TextMeshProUGUI))]
public class TextLocalizationBehaviour : MonoBehaviour
{
    public string key;
    private TextMeshProUGUI field;

    void Start()
    {
        field = GetComponent<TextMeshProUGUI>();
        string value = LocalizationSystem.Localize(key).value;
        field.text = value;
        enabled = false;
    }
}
