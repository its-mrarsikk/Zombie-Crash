using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

[AddComponentMenu("Combat/Attacking Script", 1)]
[RequireComponent(typeof(Collider))]
public class AttackingScript : MonoBehaviour
{
    public InputAction fireAction = new(binding: "<Mouse>/leftButton"); // fire for player
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
    // [Header("Trail")]
    // public bool hasTrail;
    // public TrailRenderer trail;

    void Awake()
    {
        if (!transform.parent.transform.CompareTag("Player")) return;
        fireAction.performed += _ => StartCoroutine(Fire());
        fireAction.Enable();
    }

    void Start() => collider = GetComponent<Collider>();

    public void DisableHitbox() => collider.enabled = false;
    public void PlaySound() => GetComponent<AudioSource>().Play();

    public IEnumerator Fire()
    {
        collider.enabled = true;
        if (animator != null)
        {
            animator.SetTrigger("Fire");
            // if (hasTrail) trail.enabled = true;
            yield return new WaitWhile(IsAttackingAnimation);
            // if (hasTrail) trail.enabled = false;
        }
        collider.enabled = false;
    }

    void Reset()
    {
        minRandomDamageFromHit = 20.0f;
        maxRandomDamageFromHit = 25.0f;
        fireAction = new(binding: "<Mouse>/leftButton");
    }

    bool IsAttackingAnimation() => animator.GetCurrentAnimatorClipInfo(0)[0].clip.name.EndsWith("Attack");
}
