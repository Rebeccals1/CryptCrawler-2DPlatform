using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    [Header("Patrol")]
    [SerializeField] float moveSpeed = 3f;
    [SerializeField] Transform leftEdge;
    [SerializeField] Transform rightEdge;

    [Header("Ground/Wall Detection")]
    [SerializeField] Transform groundDetect;
    [SerializeField] float detectRadius = 0.2f;
    [SerializeField] LayerMask groundLayer;

    Rigidbody2D rb;
    SpriteRenderer spriteRenderer;
    Animator animator;
    CapsuleCollider2D col;

    bool movingRight = true;
    float knockbackTimer;
    bool isAlive = true;
    float lastTurnTime;
    float turnCooldown = 0.2f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        col = GetComponent<CapsuleCollider2D>();
    }

    void Update()
    {
        if (!isAlive) return;
        CheckEdges();
    }

    void FixedUpdate()
    {
        if (!isAlive) return;
        if(knockbackTimer > 0){
            knockbackTimer -= Time.fixedDeltaTime;
            return;
        }
        Patrol();
    }

    void Patrol()
    {
        float direction = movingRight ? 1f : -1f;
        rb.linearVelocity = new Vector2(direction * moveSpeed, rb.linearVelocity.y);
        spriteRenderer.flipX = !movingRight;
        animator.SetBool("isSkelWalking", Mathf.Abs(rb.linearVelocity.x) > 0.1f);
    }

    void CheckEdges()
    {
        if (movingRight && transform.position.x >= rightEdge.position.x)
            TurnAround();
        else if (!movingRight && transform.position.x <= leftEdge.position.x)
            TurnAround();

        bool groundAhead = Physics2D.OverlapCircle(groundDetect.position, detectRadius, groundLayer);
        if (!groundAhead)
            TurnAround();
    }

    void TurnAround()
    {
        if (Time.time - lastTurnTime < turnCooldown) return;
        lastTurnTime = Time.time;
        movingRight = !movingRight;
    }

    public void TakeKnockback(float duration){
        knockbackTimer = duration;
    }

    public void Die()
    {
        isAlive = false;
        rb.linearVelocity = Vector2.zero;
        rb.gravityScale = 0f;
        if (col != null) col.enabled = false;
        animator.SetTrigger("isDead");
        Destroy(gameObject, 0.8f);
    }
}