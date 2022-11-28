using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Enemy))]
public class EnemyHealth : MonoBehaviour
{
    [SerializeField] int maxHealth = 5;
    [SerializeField] int difficultyRamp = 1;
    [SerializeField] float armor = 0f;


    Enemy enemy;
    int currentHealth;

    private void OnEnable()
    {
        currentHealth = maxHealth;
    }

    private void Start()
    {
        enemy = GetComponent<Enemy>();
    }

    // Damages a unit by an amount specified in damage
    public void DamageUnit(int damage)
    {
        this.currentHealth -= Mathf.RoundToInt((1 - armor) * damage);
        CheckHealth();
    }

    // Checks whether the enemy is dead
    private void CheckHealth()
    {
        if (currentHealth <= 0)
        {
            HandleEnemyDeath();
        }
    }

    private void HandleEnemyDeath()
    {
        this.gameObject.SetActive(false);
        maxHealth += difficultyRamp;
        enemy.RewardGold();
    }

    // Particle event fires DamageUnit() Method
    private void OnParticleCollision(GameObject other)
    {
        int damageDone = other.GetComponentInParent<NewTargetLocator>().GetDamageDone();
        DamageUnit(damageDone);
    }
}
