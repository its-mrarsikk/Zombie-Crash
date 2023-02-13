using System.Collections;
using UnityEngine;

public class DayTimeMaster : MonoBehaviour
{
    [SerializeField] private bool nightTime;
    [SerializeField] private GameObject dirLight;
    private static DayTimeMaster dtm;
    private static Material skyboxMat;
    private static Light dLight;

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

        dLight = dirLight.GetComponent<Light>();

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
                // RenderSettings.ambientIntensity = (float)(0.0102 * (100 - i));
                dLight.intensity -= 0.006f;
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
                // RenderSettings.ambientIntensity = (float)(0.0102 * (100 + i));
                dLight.intensity += 0.006f;
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
}
