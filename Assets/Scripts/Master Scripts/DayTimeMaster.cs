using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class DayTimeMaster : MonoBehaviour
{
    [SerializeField] private bool nightTime;
    private static DayTimeMaster dtm;
    private static Material skyboxMat;
    public static bool isNightTimeNow
    {
        get => dtm.nightTime; private set
        {
            dtm.nightTime = value;
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        dtm = this;
        skyboxMat = RenderSettings.skybox;

        skyboxMat.SetFloat("_Blend", 0);
        skyboxMat.SetFloat("_Rotation", 0);

        StartCoroutine(DayNightLoop());
    }

    public static IEnumerator ToggleDayTime()
    {
        // Blendin' over time
        if (!isNightTimeNow) // day
        {
            Debug.Log("Nox!");
            for (int i = 0; i < 101; i++)
            {
                skyboxMat.SetFloat("_Rotation", skyboxMat.GetFloat("_Rotation") + 3.6f);
                skyboxMat.SetFloat("_Blend", skyboxMat.GetFloat("_Blend") + 0.011f);
                RenderSettings.ambientIntensity = (float)(0.0102 * (100 - i));
                yield return new WaitForSeconds(0.002f);
            }
        }
        else // night
        {
            Debug.Log("Lumos!");
            for (int i = 0; i < 101; i++)
            {
                skyboxMat.SetFloat("_Rotation", skyboxMat.GetFloat("_Rotation") - 3.6f);
                skyboxMat.SetFloat("_Blend", skyboxMat.GetFloat("_Blend") - 0.011f);
                RenderSettings.ambientIntensity = (float)(0.0102 * (100 + i));
                yield return new WaitForSeconds(0.002f);
            }
        }

        isNightTimeNow = !isNightTimeNow;
    }

    static IEnumerator DayNightLoop()
    {
        while (true)
        {
            yield return new WaitForSeconds(5);
            yield return dtm.StartCoroutine(ToggleDayTime());
        }
    }

    static IEnumerator Wait(float s) { yield return new WaitForSeconds(s); }
}
