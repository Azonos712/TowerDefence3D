using System.Security.Cryptography;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("Attributes")]
    public float startSpeed = 5;
    [HideInInspector]
    public float speed;
    public float turnSpeed = 6;
    public float health = 100;
    public int reward = 50;
    public GameObject deathEffect;


    private void Start()
    {
        speed = startSpeed;
    }

    public void TakeDamage(float amount)
    {
        health -= amount;

        if (health <= 0)
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
        PlayerStats.Money += reward;

        GameObject e = Instantiate(deathEffect, transform.position, Quaternion.identity);
        Destroy(e, 3f);

        Destroy(gameObject);
    }

    
}
