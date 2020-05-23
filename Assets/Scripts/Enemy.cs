using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    [Header("Attributes")]
    public float startSpeed = 5;
    [HideInInspector]
    public float speed;
    public float turnSpeed = 6;
    public float startHealth = 100;
    private float health;
    public int reward = 50;
    [Header("Unity Stuff")]
    public GameObject deathEffect;
    public Image healthBar;

    private bool deadInside = false;

    private void Start()
    {
        speed = startSpeed;
        health = startHealth;
    }

    public void TakeDamage(float amount)
    {
        health -= amount;

        healthBar.fillAmount = ((health * 100f) / startHealth) / 100f;

        if (health <= 0 && !deadInside)
        {
            Die();
        }
    }

    public void Slow(float pct)
    {
        speed = startSpeed * (1f - pct);
    }

    void Die()
    {
        deadInside = true;
        PlayerStats.Money += reward;

        GameObject e = Instantiate(deathEffect, transform.position, Quaternion.identity);
        Destroy(e, 3f);

        Spawner.EnemiesAlive--;
        Destroy(gameObject);
    }
}
