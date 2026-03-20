# ✅ CryptCrawler — Student Progress Checklist
## CS 4700: Game Design Studio | Unity 6 + C#

**Name:** Rebecca
**Date Started:** March 2026
**Date Submitted:** March 20, 2026

---

## How to Use This Checklist
- Check each box **as you complete it** — don't pre-check!
- Use the ⚠️ column to mark things you're stuck on and need help with
- Review the **Reflection Questions** at the end before submitting

---

## 📁 Section 1 — Project Setup

| ✅ Done | ⚠️ Stuck | Task |
|:---:|:---:|---|
| ✅ | | Created Unity 6 project with **2D URP** template |
| ✅ | | Installed **Cinemachine** package |
| ✅ | | Installed **2D Tilemap Extras** (for Rule Tiles) |
| ✅ | | Created folder structure: Art/, Scripts/, Prefabs/, Audio/, Scenes/ |
| ✅ | | Imported or created placeholder sprites |
| ✅ | | Configured sprites: Filter Mode = Point, PPU = 16 |

**Section Notes:** All 6 complete. Aseprite used for pixel art sprites. Folder structure includes Animations/, Rules/, Tiles/ in addition to required folders.

---

## 🗺️ Section 2 — Tilemap World Building

| ✅ Done | ⚠️ Stuck | Task |
|:---:|:---:|---|
| ✅ | | Created `Grid` with at least 3 Tilemap children (Ground, Hazards, Background) |
| ✅ | | Created a **Rule Tile** for the ground |
| ✅ | | Added at least 5 rules to the Rule Tile (edges, corners, center) |
| ✅ | | Painted a level with platforms, gaps, and elevation changes |
| ✅ | | Added **Tilemap Collider 2D** to Ground |
| ✅ | | Added **Composite Collider 2D** and enabled "Used By Composite" |
| ✅ | | Hazard Tilemap has collider with **Is Trigger: true** |
| ✅ | | Background Tilemap has Order in Layer = -1 |

**Section Notes:** 6 Tilemap layers: Background, Platforms, Midground, Props, Doors, Blood (hazard). 3 Rule Tile assets: Platform.asset, Midground.asset, Midground2.asset. Rich tile set includes rock variants, blood-stained edges, skull tiles, brick dividers, logs, and wood.

---

## 🏃 Section 3 — Player Movement & Physics

| ✅ Done | ⚠️ Stuck | Task |
|:---:|:---:|---|
| ✅ | | Player has: SpriteRenderer, Rigidbody2D, CapsuleCollider2D, Animator |
| ✅ | | Rigidbody2D: Collision Detection = Continuous, Freeze Rotation Z = true |
| ✅ | | `PlayerMovement.cs` attached and compiles without errors |
| ✅ | | `GroundCheck` child object created at bottom of player |
| ✅ | | Player moves left/right with A/D or arrow keys |
| ✅ | | Player jumps with Space and only when grounded |
| ✅ | | Sprite flips direction when moving |
| ✅ | | Better jump physics applied (fall feels snappy, tap = short jump) |

**Section Notes:** Implemented as a full state machine (PlayerIdleState, PlayerMoveState, PlayerJumpState, PlayerAttackState) rather than a single script. Uses Unity New Input System. Variable gravity with separate NormalGravity, FallGravity, JumpGravity values. Jump-cut multiplier (0.5) for variable jump height.

---

## 🎬 Section 4 — Player Animation

| ✅ Done | ⚠️ Stuck | Task |
|:---:|:---:|---|
| ✅ | | `Player_Idle` animation clip created |
| ✅ | | `Player_Run` animation clip created |
| ✅ | | `Player_Jump` animation clip created |
| ✅ | | `Player_Fall` animation clip created |
| ✅ | | `Player_Death` animation clip created |
| ✅ | | Animator parameters set: `isRunning`, `isGrounded`, `yVelocity`, `die` |
| ✅ | | All transitions wired correctly (no floating states) |
| ✅ | | "Has Exit Time" unchecked on all movement transitions |
| ✅ | | Animations play correctly in Play Mode |

**Section Notes:** 7 animation clips total: Idle, Run, Jump, Falling, Dead, Attack, Landing. Additional parameters: isIdle, isJumping, isAttacking for melee attack state. All state machine transitions verified in Play Mode.

---

## 📷 Section 5 — Camera

| ✅ Done | ⚠️ Stuck | Task |
|:---:|:---:|---|
| ✅ | | Cinemachine Virtual Camera added to scene |
| ✅ | | Camera follows player smoothly |
| ✅ | | Camera confined to level bounds (CinemachineConfiner2D) |
| ✅ | | Camera shake implemented in `CameraShake.cs` |
| ✅ | | Camera shakes when player takes damage |

**Section Notes:** CameraShake is a singleton on the Virtual Camera. Shake(2f, 0.3f) called from PlayerHealth.TakeDamage(). Uses CinemachineBasicMultiChannelPerlin for shake effect.

---

## 🔲 Section 6 — Layers & Collision Matrix

| ✅ Done | ⚠️ Stuck | Task |
|:---:|:---:|---|
| ✅ | | Created layers: Ground, Player, Enemy, Hazard, PlayerProjectile, EnemyProjectile |
| ✅ | | Ground Tilemap assigned to Ground layer |
| ✅ | | Player assigned to Player layer |
| ✅ | | Layer Collision Matrix configured (PlayerProjectile ✗ Player, Enemy ✗ Enemy) |
| ✅ | | PlayerMovement Ground Check uses correct layer mask |

**Section Notes:** All 6 layers created. PlayerProjectile and EnemyProjectile layers exist in project but are not actively used since shooting was replaced with melee combat. GroundLayer LayerMask used in Player.cs CheckGrounded(). EnemyLayer LayerMask used in Combat.cs OverlapCircle.

---

## 👾 Section 7 — Enemy AI

| ✅ Done | ⚠️ Stuck | Task |
|:---:|:---:|---|
| ✅ | | Enemy prefab created with Rigidbody2D, CapsuleCollider2D, Animator |
| ✅ | | Enemy assigned to Enemy layer |
| ✅ | | `EnemyPatrol.cs` attached, enemy moves left and right |
| ✅ | | Enemy turns at waypoint boundaries |
| ✅ | | Enemy turns at ledges (no ground detected) |
| ✅ | | Enemy sprite flips direction correctly |
| ✅ | | `EnemyHurt.cs` — touching enemy damages player |
| ✅ | | Enemy `Die()` plays animation and removes GameObject |
| ✅ | | At least **2 enemies** placed in the level |

**Section Notes:** 2 Skeleton enemies placed (Skeleton1, Skeleton2), each with their own LeftEdge/RightEdge waypoints and SkelGroundCheck child. TurnAround() has 0.2s cooldown to prevent vibration at edges. Enemy has full animation set: idle, walk, hit, death, attack. Health event system (OnDamaged, OnDeath) drives animation reactions. Enemies are prefabs in Assets/Prefabs/Skeleton.prefab.

---

## 🏹 Section 8 — Shooting System → Melee Combat (Design Substitution)

| ✅ Done | ⚠️ Stuck | Task |
|:---:|:---:|---|
| ✅ | | ~~Arrow prefab~~ → **Melee attack with OverlapCircle detection** |
| ✅ | | ~~Arrow assigned to PlayerProjectile layer~~ → **EnemyLayer mask used for melee targeting** |
| ✅ | | ~~`Arrow.cs`~~ → **`Combat.cs` handles attack direction** |
| ✅ | | ~~Arrow destroys on hitting enemy~~ → **Enemy takes damage via Health.ChangeHealth()** |
| ✅ | | ~~Arrow destroys on hitting ground~~ → **Attack radius confined to enemy layer only** |
| ✅ | | ~~Arrow max lifetime~~ → **Attack cooldown (1.5s) prevents spam** |
| ✅ | | ~~`PlayerShooter.cs`~~ → **`Combat.cs` fires on left-click (Fire1)** |
| ✅ | | Fire rate limit prevents spam |
| ✅ | | Attacks in the direction the player faces |
| ✅ | | ~~`FirePoint`~~ → **`AttackPoint` child object positioned at sword tip** |

**Section Notes:** Shooting replaced with melee sword combat using Physics2D.OverlapCircle on AttackPoint. This was an intentional design decision — melee combat fits the dark dungeon aesthetic and skeleton enemy type better than projectiles. Includes attack animation, sword-slash SFX, knockback force applied to enemies, and floating damage numbers. Attack cooldown = 1.5s. Damage = 100 (one-shots enemies).

---

## ❤️ Section 9 — Health & Damage

| ✅ Done | ⚠️ Stuck | Task |
|:---:|:---:|---|
| ✅ | | `PlayerHealth.cs` tracks current / max health |
| ✅ | | UI hearts update when player takes damage |
| ✅ | | Invincibility frames work after taking damage (flashing effect) |
| ✅ | | Player death triggers game over flow |
| ✅ | | `GameSession.cs` singleton persists across scenes |
| ✅ | | Lives system: death decrements lives, reloads scene |
| ✅ | | Out of lives → loads Game Over scene |
| ✅ | | Hazard tiles damage/kill player on touch |

**Section Notes:** Max health = 3 hearts. Heart UI uses full/empty sprite swap. Invincibility duration = 1.5s with Mathf.Sin flashing. Death sets Rigidbody to Kinematic (prevents fall-through). GameSession tracks 3 lives, reloads scene on death, loads GameOver when lives = 0. Hazard.cs on Blood tilemap deals 999 damage (instant kill).

---

## 🎬 Section 10 — Scenes & Game States

| ✅ Done | ⚠️ Stuck | Task |
|:---:|:---:|---|
| ✅ | | Build Settings has all scenes in correct order |
| ✅ | | Main Menu scene with Start button works |
| ✅ | | Game Over scene exists and loads correctly |
| ✅ | | Level Exit triggers next level load |
| ✅ | | Win scene or win condition implemented |

**Section Notes:** 4 scenes in Build Profiles: MainMenu (0), Level1 (1), GameOver (2), Win (3). MainMenuUI.cs loads Level1 on Start. GameOverUI.cs and WinUI.cs both safely destroy GameSession before reloading. LevelExit.cs on ExitDoor GameObject triggers Win scene when Player tag enters BoxCollider2D trigger.

---

## 🎵 Section 11 — Audio & Polish

| ✅ Done | ⚠️ Stuck | Task |
|:---:|:---:|---|
|  X  | | ~~Background music loops on AudioSource~~ |
| ✅ | | Jump SFX plays when player jumps |
| X | | ~~Shoot SFX plays when player fires~~ |
| ✅ | | Damage SFX plays when player is hit |
| X | | ~~At least 1 particle effect (death, hit, collect, etc.)~~ |

**Section Notes:** AudioManager singleton with separate music and SFX AudioSources. Music: simplesound-scary-cinematic-background3.mp3. Jump: action_jump.mp3. Attack/Shoot: sword-slash.mp3 (PlayHit() called in PlayerAttackState). Death: female-character-death-vocal.mp3. Death particles instantiated at player position on death.

---

## 🎨 Quality Standards

### Technical Quality
| ✅ | Standard |
|:---:|---|
| ✅ | No null reference errors in Console during normal play |
| ✅ | All scripts organized in correct subfolders |
| ✅ | All tweakable values use `[SerializeField]` — no magic numbers |
| ✅ | Enemies are **prefabs** (not unique scene objects) |
| ✅ | ~~Arrow is a **prefab**~~ → DamageNumber is a prefab (melee substitution) |
| ✅ | GameObjects are named clearly in Hierarchy |
| ✅ | Unused GameObjects/scripts removed |

### Design Quality
| ✅ | Standard |
|:---:|---|
| ✅ | Level has a clear start and end |
| ✅ | Level has varied terrain (high platforms, low areas, gaps) |
| x | ~~Difficulty ramps up through the level~~ |
| ✅ | Player can distinguish hazards from safe ground visually |
| ✅ | Game is playable start to finish without bugs |

---

## 🚀 Extension Features (Optional — Choose 1+)

| ✅ | Difficulty | Feature |
|:---:|:---:|---|
| | 🟢 | ~~Double jump~~ |
| | 🟢 | ~~Animated collectible coins/gems~~ |
| | 🟢 | ~~Multiple enemy types (reskins with different speeds)~~ |
| | 🟡 | ~~Checkpoint system~~ |
| | 🟡 | ~~Moving platform~~ |
| | 🟡 | ~~Enemy with ranged attack~~ |
| | 🟡 | ~~Score system with on-screen display~~ |
| | 🔴 | ~~Boss enemy with health bar~~ |
| | 🔴 | ~~Multiple levels (3+) with scene transitions~~ |
| | 🔴 | ~~Save system (high score persists between sessions)~~ |

## Other Extensions:**
|✅| 🟢 | Floating damage numbers |
|✅| 🟢 | Enemy knockback on hit |

**Brief description of what you added:** When the player hits a skeleton, a red damage number floats upward above the enemy's head and fades out over 0.8 seconds using a World Space Canvas. Enemies also get knocked back in the direction away from the player, with patrol movement paused for 0.2 seconds to allow the force to register. GameSession.cs includes AddScore() and GetScore() methods for future score UI integration.

---

## 📤 Submission Checklist

| ✅ | Task |
|:---:|---|
| ✅ | Project builds without errors (File → Build Settings → Build And Run) |
| ✅ | Game is playable from Main Menu to Game Over/Win |
| ✅ | Submitted GitHub repository link on Canvas |
| ✅ | Repository is **public** |
| ✅ | All 3 tutorial documents included in repo (`Tutorial.md`, `Checklist.md`, `QuickReference.md`) |
| ✅ | Completed **Reflection Questions** (below) |

**GitHub Repository:** https://github.com/Rebeccals1/CryptCrawler-2DPlatform

---

*CryptCrawler Checklist | CS 4700: Game Design Studio | Unity 6 + C# | Rebecca | March 2026*
