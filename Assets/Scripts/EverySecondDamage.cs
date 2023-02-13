using System.Collections;
using UnityEngine;

public class EverySecondDamage : MonoBehaviour
{
    private HealthSystem hs;

    // Start is called before the first frame update
    void Start()
    {
        hs = GetComponent<HealthSystem>();
        StartCoroutine(EverySecond());
    }

    IEnumerator EverySecond()
    {
        while (gameObject != null)
        {
            hs.Damage(10);
            yield return new WaitForSeconds(1);
        }
        yield return null;
    }
}
