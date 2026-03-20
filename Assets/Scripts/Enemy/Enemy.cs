using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Animator anim;
    public Health health;

    private void OnEnable()
    {
        if (health != null)
        {
            health.OnDamaged += HandleDamage;
            health.OnDeath   += HandleDeath;
        }
    }

    private void OnDisable()
    {
        if (health != null)
        {
            health.OnDamaged -= HandleDamage;
            health.OnDeath   -= HandleDeath;
        }
    }

    void HandleDamage()
    {
        anim?.SetTrigger("isDamaged");
    }

    void HandleDeath()
    {
        GetComponent<EnemyPatrol>()?.Die();
    }
}