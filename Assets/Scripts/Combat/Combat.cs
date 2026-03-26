using UnityEngine;

public class Combat : MonoBehaviour
{
    [Header("Attack")]
    [field: SerializeField] public int Damage { get; private set; } = 1;
    [field: SerializeField] public float AttackRadius { get; private set; } = 0.5f;
    [field: SerializeField] public Transform AttackPoint { get; private set; }
    [field: SerializeField] public LayerMask EnemyLayer { get; private set; }

    public Player player;
    public float attackCooldown = 1.5f;
    public float nextAttackTime;
    public bool CanAttack => Time.time >= nextAttackTime;

    public void AttackAnimationFinished()
    {
        player.AttackAnimationFinished();
    }

    public void Attack(){
        if(!CanAttack){
            return;
        }
        nextAttackTime = Time.time + attackCooldown;
        Collider2D Enemy = Physics2D.OverlapCircle(AttackPoint.position,AttackRadius, EnemyLayer);
        if(Enemy != null){
            Vector2 knockbackDirection = (Enemy.transform.position - player.transform.position).normalized;
            Enemy.gameObject.GetComponent<Health>().ChangeHealth(-Damage, -knockbackDirection);
        }
    }
}
