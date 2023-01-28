using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[AddComponentMenu("Combat/Attacking Script", 1)]
[RequireComponent(typeof(Collider))]
public class AttackingScript : MonoBehaviour
{
    public InputAction fireAction = new(binding: "*/{primaryAction}"); // fire for player
    public Animator animator;
    public bool playsSound;
    new private Collider collider;

    public float damageFromHit
    {
        get =>
            Random.Range(minRandomDamageFromHit, maxRandomDamageFromHit);
    }
    [SerializeField] private float minRandomDamageFromHit = 20.0f;
    [SerializeField] private float maxRandomDamageFromHit = 25.0f;

    void Awake()
    {
        if (!transform.parent.transform.CompareTag("Player")) return;
        fireAction.performed += _ => Fire();
        fireAction.Enable();


    }

    void Start() => collider = GetComponent<Collider>();

    public void DisableHitbox() => collider.enabled = false;
    public void PlaySound() => GetComponent<AudioSource>().Play();

    public void Fire()
    {
        collider.enabled = true;
        if (animator != null)
        {
            GetComponent<Animator>().SetTrigger("Fire");
            StartCoroutine(WaitForAnimation());
        }
        collider.enabled = false;
    }

    IEnumerator WaitForAnimation()
    {
        yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length + animator.GetCurrentAnimatorStateInfo(0).normalizedTime + 1);
    }

    void Reset()
    {
        minRandomDamageFromHit = 20.0f;
        maxRandomDamageFromHit = 25.0f;
        fireAction = new(binding: "*/{primaryAction}");
    }
}
