using System;
using UnityEngine;

public abstract class BarController : MonoBehaviour
{
    [Header("Graphics")]
    public GameObject foregroundSprite;
    public Canvas canvas;
    [Tooltip("Leave None if you don't want to output. If not None, should be an object with TMPro text component.")]
    public GameObject counter;

    [Header("Resources")]
    public GameObject resourceOwner;

    public abstract void UpdateBar();

    protected string NormalizeFloat(float input)
    {
        if (input == MathF.Truncate(input)) return MathF.Truncate(input).ToString(); // just a x.0 here, return x with no deci
        return (MathF.Truncate(input * 10) / 10).ToString();
    }
}
