using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    // ─── Serialized Fields ──────────────────────────────────────────────
    [Header("Components")]
    [field: SerializeField] public PlayerInput PlayerInput { get; private set; }
    [field: SerializeField] public Rigidbody2D Rb { get; private set; }
    [field: SerializeField] public Animator Anim { get; private set; }
    [field: SerializeField] public Combat Combat { get; private set; }
    
    [Header("Ground Check")]
    [field: SerializeField] public Transform GroundCheck { get; private set; }
    [field: SerializeField] public float GroundCheckRadius { get; private set; } = 0.15f;
    [field: SerializeField] public LayerMask GroundLayer { get; private set; }

    [Header("Gravity")]
    [field: SerializeField] public float NormalGravity { get; private set; } = 4;
    [field: SerializeField] public float FallGravity { get; private set; } = 4;
    [field: SerializeField] public float JumpGravity { get; private set; } = 4;

    [Header("Movement")]
    [field: SerializeField] public float WalkSpeed { get; private set; } = 4f;
    [field: SerializeField] public float RunSpeed { get; private set; } = 8f;
    [field: SerializeField] public Vector2 MoveInput { get; private set; }
    [field: SerializeField] public int FacingDirection { get; private set; } = 1;
    
    [Header("Jump")]
    [field: SerializeField] public float JumpForce { get; private set; } = 13;   
    [field: SerializeField] public float JumpCutMultiplier { get; private set; } = 0.5f;

    [SerializeField] private bool isAlive = true;

    // ─── Global Variables ──────────────────────────────────────────────
    public PlayerState currentState;
    public PlayerIdleState idleState;
    public PlayerJumpState jumpState;
    public PlayerMoveState moveState;
    public PlayerAttackState attackState;
    public bool isGrounded;
    public bool runPressed;
    public bool jumpPressed;
    public bool jumpReleased;
    public bool attackPressed;
    private Vector3 startingScale;

    // ─── Methods ───────────────────────────────────────────────────── 
    private void Awake() 
    {
        idleState = new PlayerIdleState(this);
        jumpState = new PlayerJumpState(this);
        moveState = new PlayerMoveState(this);
        attackState = new PlayerAttackState(this);
    }

    public void Start()
    {
        Rb = GetComponent<Rigidbody2D>();
        Rb.gravityScale = NormalGravity;
        startingScale = transform.localScale;
        ChangeState(idleState);
    }

    public void Update()
    {
        if(!isAlive) return;
        currentState.Update();
        FlipSprite();
        HandleAnimations();
    }

    public void FixedUpdate() {
        if(!isAlive) return;
        currentState.FixedUpdate();
        CheckGrounded();
    }

    public void ChangeState(PlayerState newState){
        if(currentState != null) {
            currentState.Exit();
        }
        currentState = newState;
        currentState.Enter();
    }

    // ─── Ground Checks ─────────────────────────────────────────────────────
    public void CheckGrounded() {
        isGrounded = Physics2D.OverlapCircle(GroundCheck.position, GroundCheckRadius, GroundLayer);
    }

    // visible circle for GroundCheck object
    private void OnDrawGizmosSelected() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(GroundCheck.position, GroundCheckRadius);
    }

    private void OnDrawGizmos() {
        if (Combat != null && Combat.AttackPoint != null) {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(Combat.AttackPoint.position, Combat.AttackRadius);
        }
    }

    // ─── Gravity ──────────────────────────────────────────────────────────
    public void ApplyVariableGravity()
    {
        if(Rb.linearVelocity.y < -0.1f) // Falling
        {
            Rb.gravityScale = FallGravity;
        } 
        else if(Rb.linearVelocity.y > 0.1f) // Rising 
        {
            Rb.gravityScale = JumpGravity;
        }
        else
        {
            Rb.gravityScale = NormalGravity;
        }
    }

    // ─── Jump ────────────────────────────────────────────────────────────
    public void OnJump(InputValue value){
        if(value.isPressed){
            jumpPressed = true;
            jumpReleased = false;
        } 
        else // Button is released
        {
            jumpReleased = true;
        }
    }

    public void HandleJump(){
        // ONLY allow jumping if we are NOT in the attack state
        if (currentState == attackState) return;
        
        
        if(jumpPressed && isGrounded){
            Rb.linearVelocity = new Vector2(Rb.linearVelocity.x, JumpForce);
            jumpPressed = false;
            jumpReleased = false;
        }

        if(jumpReleased){
            if(Rb.linearVelocity.y > 0) // Still going up
            {
               Rb.linearVelocity = new Vector2(Rb.linearVelocity.x, Rb.linearVelocity.y * JumpCutMultiplier); 
            }
            jumpReleased = false;
        }
    }

    // ─── Movement ─────────────────────────────────────────────────────────
    public void OnMove(InputValue value){
        MoveInput = value.Get<Vector2>();
    }

    public void OnRun(InputValue value){
        runPressed = value.isPressed;
    }

    // ─── Attacks ──────────────────────────────────────────────────────────
    public void OnAttack(InputValue value)
    {
        attackPressed = value.isPressed;
    }

    public void AttackAnimationFinished(){
        currentState.AttackAnimationFinished();
        
    }

    // ─── Public API (called by other scripts) ───────────────────────────
    public void OnDeath()
    {
        isAlive = false;
        Anim.SetTrigger("isDead");
        Rb.linearVelocity = Vector2.zero;
        // Change to Kinematic so it doesn't fall through the world
        Rb.bodyType = RigidbodyType2D.Kinematic;
        GetComponent<Collider2D>().enabled = false;
    }

    // ─── Animations & Turns ───────────────────────────────────────────────
    public void HandleAnimations() {
        Anim.SetBool("isGrounded", isGrounded);

        float cleanY = Rb.linearVelocity.y;
        if (Mathf.Abs(cleanY) < 0.01f) cleanY = 0f;
        Anim.SetFloat("yVelocity", cleanY);
    }

    public void FlipSprite() {
        if (Mathf.Abs(MoveInput.x) < 0.1f) return; // Don't flip if not moving

        int newDirection = MoveInput.x > 0 ? 1 : -1;

        if (newDirection != FacingDirection) {
            FacingDirection = newDirection;
            transform.localScale = new Vector3(FacingDirection * Mathf.Abs(startingScale.x), startingScale.y, startingScale.z);
        }
    }



}
