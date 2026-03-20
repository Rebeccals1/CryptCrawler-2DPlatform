# ⚡ CryptCrawler — Quick Reference
## CS 4700: Game Design Studio | Unity 6 + C#

> Keep this open in a second window while you code!

---

## 🏃 Player State Machine

CryptCrawler uses a **state machine** pattern. The player has 4 states, each in its own class:

```csharp
// Change state from anywhere
player.ChangeState(player.idleState);
player.ChangeState(player.moveState);
player.ChangeState(player.jumpState);
player.ChangeState(player.attackState);

// States are created in Player.Awake()
idleState   = new PlayerIdleState(this);
moveState   = new PlayerMoveState(this);
jumpState   = new PlayerJumpState(this);
attackState = new PlayerAttackState(this);
```

### State Base Class (PlayerState.cs)
```csharp
public abstract class PlayerState {
    protected Player player;
    protected Rigidbody2D Rb       => player.Rb;
    protected Animator Anim        => player.Anim;
    protected Vector2 MoveInput    => player.MoveInput;
    protected bool JumpPressed     { get => player.jumpPressed; set => player.jumpPressed = value; }
    protected bool AttackPressed   => player.attackPressed;
    protected Combat combat;

    public virtual void Enter(){}
    public virtual void Exit(){}
    public virtual void Update(){}
    public virtual void FixedUpdate(){}
    public virtual void AttackAnimationFinished(){}
}
```

---

## 🎮 Input (New Input System)

```csharp
// Callbacks on Player.cs — wired via PlayerInput component
public void OnMove(InputValue value)   { MoveInput = value.Get<Vector2>(); }
public void OnJump(InputValue value)   { jumpPressed = value.isPressed; jumpReleased = !value.isPressed; }
public void OnRun(InputValue value)    { runPressed = value.isPressed; }
public void OnAttack(InputValue value) { attackPressed = value.isPressed; }
```

---

## 🏃 Movement & Physics

### Variable Gravity (in PlayerJumpState.FixedUpdate)
```csharp
public void ApplyVariableGravity() {
    if (Rb.linearVelocity.y < -0.1f)       // Falling
        Rb.gravityScale = FallGravity;
    else if (Rb.linearVelocity.y > 0.1f)   // Rising
        Rb.gravityScale = JumpGravity;
    else
        Rb.gravityScale = NormalGravity;
}
```

### Jump Cut (tap = short jump)
```csharp
// In PlayerJumpState.FixedUpdate
if (JumpReleased && Rb.linearVelocity.y > 0) {
    Rb.linearVelocity = new Vector2(Rb.linearVelocity.x, Rb.linearVelocity.y * player.JumpCutMultiplier);
    JumpReleased = false;
}
```

### Ground Check
```csharp
// In Player.CheckGrounded() — called every FixedUpdate
isGrounded = Physics2D.OverlapCircle(GroundCheck.position, GroundCheckRadius, GroundLayer);
```

### Sprite Flip
```csharp
// In Player.FlipSprite()
int newDirection = MoveInput.x > 0 ? 1 : -1;
if (newDirection != FacingDirection) {
    FacingDirection = newDirection;
    transform.localScale = new Vector3(FacingDirection * Mathf.Abs(startingScale.x), startingScale.y, startingScale.z);
}
```

---

## ⚔️ Melee Combat

```csharp
// In Combat.Attack() — called from player states when LMB pressed
public void Attack() {
    if (!CanAttack) return;
    nextAttackTime = Time.time + attackCooldown;

    Collider2D enemy = Physics2D.OverlapCircle(AttackPoint.position, AttackRadius, EnemyLayer);
    if (enemy != null) {
        Vector2 knockbackDir = (enemy.transform.position - player.transform.position).normalized;
        enemy.GetComponent<Health>().ChangeHealth(-Damage, knockbackDir);
    }
}

// Cooldown check
public bool CanAttack => Time.time >= nextAttackTime;
```

### Attack Gizmo (visible in Scene view)
```csharp
private void OnDrawGizmos() {
    if (Combat != null && Combat.AttackPoint != null) {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(Combat.AttackPoint.position, Combat.AttackRadius);
    }
}
```

---

## 🎬 Animation Parameters

| Parameter | Type | Set By |
|---|---|---|
| `isIdle` | Bool | PlayerIdleState Enter/Exit |
| `isRunning` | Bool | PlayerMoveState Enter/Exit |
| `isJumping` | Bool | PlayerJumpState Enter/Exit |
| `isAttacking` | Bool | PlayerAttackState Enter/Exit |
| `isGrounded` | Bool | Player.HandleAnimations() |
| `yVelocity` | Float | Player.HandleAnimations() |
| `die` | Trigger | Player.OnDeath() |

### Skeleton Animator Parameters
| Parameter | Type | Set By |
|---|---|---|
| `isSkelWalking` | Bool | EnemyPatrol.Patrol() |
| `isDamaged` | Trigger | Enemy.HandleDamage() |
| `die` | Trigger | EnemyPatrol.Die() |

---

## ❤️ Health System

### PlayerHealth — Take Damage
```csharp
public void TakeDamage(int amount) {
    if (isInvincible) return;
    currentHealth -= amount;
    currentHealth = Mathf.Max(0, currentHealth);
    UpdateHeartUI();
    CameraShake.Instance?.Shake(2f, 0.3f);
    if (currentHealth <= 0) { Die(); return; }
    isInvincible = true;
    invincibilityTimer = invincibilityDuration;
}
```

### Update Heart UI
```csharp
void UpdateHeartUI() {
    for (int i = 0; i < heartImages.Length; i++)
        heartImages[i].sprite = (i < currentHealth) ? fullHeart : emptyHeart;
}
```

### Enemy Health (event-driven)
```csharp
// Health.cs fires events — Enemy.cs subscribes
health.OnDamaged += HandleDamage;   // plays hit animation
health.OnDeath   += HandleDeath;    // calls EnemyPatrol.Die()

// Always unsubscribe in OnDisable
health.OnDamaged -= HandleDamage;
health.OnDeath   -= HandleDeath;
```

---

## 👾 Enemy AI

### Patrol with Knockback Pause
```csharp
void FixedUpdate() {
    if (!isAlive) return;
    if (knockbackTimer > 0) {
        knockbackTimer -= Time.fixedDeltaTime;
        return;   // skip patrol while knocked back
    }
    Patrol();
}

void Patrol() {
    float direction = movingRight ? 1f : -1f;
    rb.linearVelocity = new Vector2(direction * moveSpeed, rb.linearVelocity.y);
    spriteRenderer.flipX = !movingRight;
    animator.SetBool("isSkelWalking", Mathf.Abs(rb.linearVelocity.x) > 0.1f);
}
```

### Ledge + Waypoint Detection (with turn cooldown)
```csharp
void TurnAround() {
    if (Time.time - lastTurnTime < turnCooldown) return;
    lastTurnTime = Time.time;
    movingRight = !movingRight;
}
```

### Death
```csharp
public void Die() {
    isAlive = false;
    rb.linearVelocity = Vector2.zero;
    rb.gravityScale = 0f;
    if (col != null) col.enabled = false;
    animator.SetTrigger("die");
    Destroy(gameObject, 0.8f);
}
```

---

## 🏗️ GameSession Singleton

```csharp
// Process death — reload or game over
public void ProcessPlayerDeath() {
    lives--;
    if (lives > 0) SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    else { Destroy(gameObject); SceneManager.LoadScene("GameOver"); }
}

// Win
public void LoadNextLevel() {
    SceneManager.LoadScene("Win");
}

// DontDestroyOnLoad pattern
void Awake() {
    if (Instance != null) { Destroy(gameObject); return; }
    Instance = this;
    DontDestroyOnLoad(gameObject);
}
```

---

## 📷 Camera Shake

```csharp
// Trigger from any script
CameraShake.Instance?.Shake(intensity: 2f, duration: 0.3f);

// Recommended values:
// Player takes damage:  Shake(2f, 0.3f)
// Player death:         Shake(3f, 0.5f)
// Light hit:            Shake(1f, 0.15f)
```

---

## 💥 Floating Damage Numbers

```csharp
// Spawned automatically by Health.SpawnDamageNumber()
// Requires: DamageNumber prefab + WorldCanvas assigned on Health component

void SpawnDamageNumber(int amount) {
    if (damageNumberPrefab == null || worldCanvas == null) return;
    GameObject obj = Instantiate(damageNumberPrefab, worldCanvas.transform);
    obj.transform.position = transform.position + Vector3.up * 1.5f;
    obj.GetComponent<DamageNumber>().SetValue(amount);
}
// DamageNumber floats up at 1.5 units/sec, fades over 0.8s, then destroys
```

---

## 🔊 Audio Calls

```csharp
// Via AudioManager singleton
AudioManager.Instance?.PlayJump();   // action_jump.mp3
AudioManager.Instance?.PlayHit();    // sword-slash.mp3
AudioManager.Instance?.PlayDeath();  // female-character-death-vocal.mp3

// Music volume
AudioManager.Instance?.SetMusicVolume(0.5f);
```

---

## 🧱 Tilemap Tips

| Action | How |
|---|---|
| Paint tiles | Tile Palette → Brush (B) → paint in Scene view |
| Erase tiles | Tile Palette → Erase (D) |
| Fill area | Tile Palette → Fill (G) |
| Select tilemap | MUST select the correct Tilemap in Hierarchy first |
| Fix Z-order | Tilemap Renderer → Order in Layer |
| Efficient collision | Tilemap Collider 2D + Composite Collider 2D + Used By Composite |
| Hazard tiles | Tilemap Collider 2D with **Is Trigger = true** + Hazard.cs script |

---

## 🔲 Layer Setup

```
Layers used in CryptCrawler:
  Ground           — Platforms tilemap, used in GroundCheck LayerMask
  Player           — Player GameObject
  Enemy            — Skeleton enemies, used in Combat.cs EnemyLayer
  Hazard           — Blood tilemap
  PlayerProjectile — Reserved (melee game, not actively used)
  EnemyProjectile  — Reserved (melee game, not actively used)
  UI               — Canvas elements

Layer Mask in code:
  LayerMask.GetMask("Ground")
  1 << LayerMask.NameToLayer("Enemy")  // bitmask
```

---

## 🎬 Scene Flow

```
MainMenu (index 0)
  → [Start button] → Level1

Level1 (index 1)
  → [Player dies, lives > 0]  → reload Level1
  → [Player dies, lives = 0]  → GameOver
  → [Reach ExitDoor trigger]  → Win

GameOver (index 2)
  → [Play Again button] → Level1

Win (index 3)
  → [Play Again button] → Level1
```

---

## ⏱️ Timing Utilities

```csharp
// Delay a method call
Invoke(nameof(MethodName), delaySeconds);

// Wait then execute (Coroutine)
IEnumerator WaitThenDo() {
    yield return new WaitForSeconds(1f);
    DoSomething();
}
StartCoroutine(WaitThenDo());

// Destroy with delay
Destroy(gameObject, 0.8f);

// Cooldown pattern (used in Combat.cs)
float nextAttackTime;
public bool CanAttack => Time.time >= nextAttackTime;
if (CanAttack) { nextAttackTime = Time.time + attackCooldown; Attack(); }
```

---

## 🐛 Common Bugs & Fixes

| Bug | Likely Cause | Fix |
|---|---|---|
| Player falls through floor | Composite Collider not set up | Add Composite Collider 2D, enable Used By Composite |
| Player jumps in air | Ground Check layer mask wrong | Check LayerMask in Inspector matches Ground layer |
| Skeleton vibrates at edge | TurnAround fires every frame | Add turn cooldown: `if (Time.time - lastTurnTime < 0.2f) return` |
| Hit animation not playing | Any State → Skele_hit has no condition | Add isDamaged trigger condition to transition |
| Death triggers on every hit | OnDamaged subscribed to HandleDeath | Use OnDeath event for HandleDeath, not OnDamaged |
| Health starts at 0 | Public field serialized as 0 in Inspector | Make `health` private, use `[SerializeField] private int maxHealth` |
| Skeleton dies on spawn | Death animation is default Animator state | Right-click Skele_idle → Set as Layer Default State |
| Player can move after death | isAlive not checked in Update | Add `if (!isAlive) return;` in Update and FixedUpdate |
| isAlive starts false | Serialized bool defaulted to false | Add `= true` and right-click component → Reset |
| NullReferenceException | Unassigned serialized field | Check Inspector — drag missing reference in |
| Animation stuck | Has Exit Time checked | Uncheck Has Exit Time on movement transitions |
| Camera jitter | Multiple cameras active | Disable Main Camera or set Cinemachine priority |
| Sprite flicker (invincibility) | Correct — it's a feature! | Adjust flash speed: `Mathf.Sin(timer * 20f)` |
| DontDestroyOnLoad duplicate | Two GameSessions exist | Singleton Awake() destroys extra instances |
| Damage numbers not showing | DamageNumber prefab missing TMP | Ensure TextMeshProUGUI + DamageNumber.cs are on same object |
| Scene doesn't load on death | GameSession not in scene | Add empty GameObject + GameSession.cs to Level1 |

---

## 🎯 Design Patterns Used

```
Singleton:    GameSession, AudioManager, CameraShake
              One instance, accessible anywhere via Instance

State Machine: Player states (Idle, Move, Jump, Attack)
              Each state is a separate class — clean, modular, extensible

Event System:  Health.OnDamaged, Health.OnDeath
              Scripts subscribe/unsubscribe — no tight coupling

Component:    PlayerHealth, Combat, EnemyPatrol, EnemyHurt, Hazard
              Each script has one responsibility

Prefab:       Skeleton, Player, DamageNumber, Doors
              Reusable object templates — changes apply everywhere
```

---

## ⌨️ Unity Keyboard Shortcuts

| Shortcut | Action |
|---|---|
| `W / E / R` | Move / Rotate / Scale tool |
| `Ctrl + P` | Play / Stop |
| `Ctrl + D` | Duplicate GameObject |
| `F` | Focus selected object in Scene view |
| `Alt + drag` | Pan Scene view |
| `Scroll wheel` | Zoom Scene view |
| `Ctrl + Z` | Undo |
| `Ctrl + Shift + Z` | Redo |
| `Ctrl + S` | Save scene |
| `Ctrl + Shift + B` | Build Profiles |

---

*CryptCrawler Quick Reference | CS 4700: Game Design Studio | Unity 6 + C# | Rebecca | March 2026*
