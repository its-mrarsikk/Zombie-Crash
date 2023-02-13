using UnityEngine;
using StarterAssets;

public class FallDamage : MonoBehaviour
{
    public float startYPos;
    public float endYPos;
    public float damageThreshold;
    public bool damaged = false;
    public bool firstCall = true;
    public float extraDamageMultiplier = 0f;

    // Update is called once per frame
    void Update()
    {
        if (!GetComponent<FirstPersonController>().Grounded)
        {
            if (transform.position.y > startYPos)
            {
                firstCall = true;
            }

            if (firstCall)
            {
                startYPos = transform.position.y;
                firstCall = false;
                damaged = true;
            }
        }
        if (GetComponent<FirstPersonController>().Grounded)
        {
            endYPos = transform.position.y;
            if (startYPos - endYPos > damageThreshold)
            {
                if (damaged)
                {
                    FallDamaged(startYPos - endYPos - damageThreshold);
                    damaged = false;
                    firstCall = true;
                }
            }
        }
    }

    public void FallDamaged(float damageAmount)
    {
        /***** Add extra damage for more realism from heights*****/
        if (extraDamageMultiplier > 0f)
        {

            GetComponent<HealthSystem>().Damage(damageAmount * extraDamageMultiplier);
        }
        else
        {
            GetComponent<HealthSystem>().Damage(damageAmount);
        }
    }
}