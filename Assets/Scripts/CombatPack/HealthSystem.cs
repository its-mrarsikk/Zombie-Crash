using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
[AddComponentMenu("Combat/Health/Health System", 2)]
public class HealthSystem : MonoBehaviour
{
    private float _health;
    private bool _isReady = false;

    [Header("Graphical Resources")]
    public GameObject healthBar;
    public GameObject deathEffect;
    [Header("Settings")]
    [Min(1f)]
    public float maximumHealth;
    public float health { get => _health; set { _health = value; OnHealthUpdated(); } }
    public bool logHealthUpdates = false;
    public bool handleCollisions = true;
    [Header("Collider Tags")]
    [Space]
    public List<string> attackerTags = new() { "AttackObject" };
    public List<string> healerTags = new() { "HealObject" };

    // Start is called before the first frame update
    void Start()
    {
        health = maximumHealth;
        _isReady = true;
        Debug.Log($"Health system ready for {gameObject.name} ({health} HP)");
        if (healthBar == null) Debug.Log($"Warning: no health bar specified for {gameObject.name}.");
    }

    void OnHealthUpdated()
    {
        if (healthBar != null) healthBar.GetComponent<HealthBarController>().UpdateBar();

        if (health <= 0)
        {
            OnDeath();
            return;
        }
        if (health > maximumHealth)
        {
            health = maximumHealth;
        }

        if (logHealthUpdates && _isReady) Debug.Log($"Health updated for {gameObject.name}. New health: {health}.");
    }

    void OnDeath()
    {
        Debug.Log($"{gameObject.name} has reached 0 HP.");
        _health = 0;
        if (deathEffect != null)
        {
            GameObject effect = Instantiate(deathEffect, transform.position, transform.rotation, null);
            effect.SetActive(true);
            effect.GetComponent<ParticleSystem>().Play(true);
        }
        Destroy(gameObject);
    }

    public void Damage(float amount) => health -= amount;

    public void Heal(float amount) => health += amount;

    void OnCollisionEnter(Collision collision)
    {
        if (!handleCollisions) return;
        if (attackerTags.Contains(collision.collider.tag))
        {
            Damage(collision.collider.GetComponent<AttackingScript>().damageFromHit);
        }
        if (healerTags.Contains(collision.collider.tag))
        {
            // TODO: Implement healer component
        }
    }
}
