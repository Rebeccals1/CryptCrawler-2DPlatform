using UnityEngine;
using System;

public class Health : MonoBehaviour
{
    [SerializeField] GameObject damageNumberPrefab;
    [SerializeField] Canvas worldCanvas;
    [SerializeField] float knockbackForce = 5f;
    [SerializeField] private int maxHealth = 100;

    public event Action OnDamaged;
    //public event Action OnHealed;
    public event Action OnDeath;
    private int health;
    Rigidbody2D rb;
    

    private void Start()
    {
        health = maxHealth;
        rb = GetComponent<Rigidbody2D>();
    }

    public void ChangeHealth(int amount, Vector2 knockbackDirection = default)
    {
        if (amount == 0) return;

        health += amount;

        // Clamp health between 0 and maxHealth
        health = Mathf.Clamp(health, 0, maxHealth);

        if (amount < 0)
        {
            SpawnDamageNumber(-amount);
            ApplyKnockback(knockbackDirection);
            if (health <= 0)
            {
                OnDeath?.Invoke();
            }
            else
            {
                OnDamaged?.Invoke();
            }
        }
    }

    void SpawnDamageNumber(int amount)
    {
        if (damageNumberPrefab == null || worldCanvas == null) return;
        GameObject obj = Instantiate(damageNumberPrefab, worldCanvas.transform);
        
        Vector2 screenPos = Camera.main.WorldToScreenPoint(transform.position + Vector3.up * 1.5f);
        obj.GetComponent<RectTransform>().position = screenPos;
        obj.GetComponent<DamageNumber>().SetValue(amount);
    }

    void ApplyKnockback(Vector2 direction)
    {
        if (rb == null || direction == Vector2.zero) return;
        rb.linearVelocity = Vector2.zero;
        rb.AddForce(direction * knockbackForce, ForceMode2D.Impulse);
        GetComponent<EnemyPatrol>()?.TakeKnockback(0.6f);
    }

}