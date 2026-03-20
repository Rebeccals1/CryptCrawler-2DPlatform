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
| [x] | | Created Unity 6 project with **2D URP** template |
| [x] | | Installed **Cinemachine** package |
| [x] | | Installed **2D Tilemap Extras** (for Rule Tiles) |
| [x] | | Created folder structure: Art/, Scripts/, Prefabs/, Audio/, Scenes/ |
| [x] | | Imported or created placeholder sprites |
| [x] | | Configured sprites: Filter Mode = Point, PPU = 16 |

**Section Notes:** All 6 complete. Aseprite used for pixel art sprites. Folder structure includes Animations/, Rules/, Tiles/ in addition to required folders.

---

## 🗺️ Section 2 — Tilemap World Building

| ✅ Done | ⚠️ Stuck | Task |
|:---:|:---:|---|
| [x] | | Created `Grid` with at least 3 Tilemap children (Ground, Hazards, Background) |
| [x] | | Created a **Rule Tile** for the ground |
| [x] | | Added at least 5 rules to the Rule Tile (edges, corners, center) |
| [x] | | Painted a level with platforms, gaps, and elevation changes |
| [x] | | Added **Tilemap Collider 2D** to Ground |
| [x] | | Added **Composite Collider 2D** and enabled "Used By Composite" |
| [x] | | Hazard Tilemap has collider with **Is Trigger: true** |
| [x] | | Background Tilemap has Order in Layer = -1 |

**Section Notes:** 6 Tilemap layers: Background, Platforms, Midground, Props, Doors, Blood (hazard). 3 Rule Tile assets: Platform.asset, Midground.asset, Midground2.asset. Rich tile set includes rock variants, blood-stained edges, skull tiles, brick dividers, logs, and wood.

---

## 🏃 Section 3 — Player Movement & Physics

| ✅ Done | ⚠️ Stuck | Task |
|:---:|:---:|---|
| [x] | | Player has: SpriteRenderer, Rigidbody2D, CapsuleCollider2D, Animator |
| [x] | | Rigidbody2D: Collision Detection = Continuous, Freeze Rotation Z = true |
| [x] | | `PlayerMovement.cs` attached and compiles without errors |
| [x] | | `GroundCheck` child object created at bottom of player |
| [x] | | Player moves left/right with A/D or arrow keys |
| [x] | | Player jumps with Space and only when grounded |
| [x] | | Sprite flips direction when moving |
| [x] | | Better jump physics applied (fall feels snappy, tap = short jump) |

**Section Notes:** Implemented as a full state machine (PlayerIdleState, PlayerMoveState, PlayerJumpState, PlayerAttackState) rather than a single script. Uses Unity New Input System. Variable gravity with separate NormalGravity, FallGravity, JumpGravity values. Jump-cut multiplier (0.5) for variable jump height.

---

## 🎬 Section 4 — Player Animation

| ✅ Done | ⚠️ Stuck | Task |
|:---:|:---:|---|
| [x] | | `Player_Idle` animation clip created |
| [x] | | `Player_Run` animation clip created |
| [x] | | `Player_Jump` animation clip created |
| [x] | | `Player_Fall` animation clip created |
| [x] | | `Player_Death` animation clip created |
| [x] | | Animator parameters set: `isRunning`, `isGrounded`, `yVelocity`, `die` |
| [x] | | All transitions wired correctly (no floating states) |
| [x] | | "Has Exit Time" unchecked on all movement transitions |
| [x] | | Animations play correctly in Play Mode |

**Section Notes:** 7 animation clips total: Idle, Run, Jump, Falling, Dead, Attack, Landing. Additional parameters: isIdle, isJumping, isAttacking for melee attack state. All state machine transitions verified in Play Mode.

---

## 📷 Section 5 — Camera

| ✅ Done | ⚠️ Stuck | Task |
|:---:|:---:|---|
| [x] | | Cinemachine Virtual Camera added to scene |
| [x] | | Camera follows player smoothly |
| [x] | | Camera confined to level bounds (CinemachineConfiner2D) |
| [x] | | Camera shake implemented in `CameraShake.cs` |
| [x] | | Camera shakes when player takes damage |

**Section Notes:** CameraShake is a singleton on the Virtual Camera. Shake(2f, 0.3f) called from PlayerHealth.TakeDamage(). Uses CinemachineBasicMultiChannelPerlin for shake effect.

---

## 🔲 Section 6 — Layers & Collision Matrix

| ✅ Done | ⚠️ Stuck | Task |
|:---:|:---:|---|
| [x] | | Created layers: Ground, Player, Enemy, Hazard, PlayerProjectile, EnemyProjectile |
| [x] | | Ground Tilemap assigned to Ground layer |
| [x] | | Player assigned to Player layer |
| [x] | | Layer Collision Matrix configured (PlayerProjectile ✗ Player, Enemy ✗ Enemy) |
| [x] | | PlayerMovement Ground Check uses correct layer mask |

**Section Notes:** All 6 layers created. PlayerProjectile and EnemyProjectile layers exist in project but are not actively used since shooting was replaced with melee combat. GroundLayer LayerMask used in Player.cs CheckGrounded(). EnemyLayer LayerMask used in Combat.cs OverlapCircle.

---

## 👾 Section 7 — Enemy AI

| ✅ Done | ⚠️ Stuck | Task |
|:---:|:---:|---|
| [x] | | Enemy prefab created with Rigidbody2D, CapsuleCollider2D, Animator |
| [x] | | Enemy assigned to Enemy layer |
| [x] | | `EnemyPatrol.cs` attached, enemy moves left and right |
| [x] | | Enemy turns at waypoint boundaries |
| [x] | | Enemy turns at ledges (no ground detected) |
| [x] | | Enemy sprite flips direction correctly |
| [x] | | `EnemyHurt.cs` — touching enemy damages player |
| [x] | | Enemy `Die()` plays animation and removes GameObject |
| [x] | | At least **2 enemies** placed in the level |

**Section Notes:** 2 Skeleton enemies placed (Skeleton1, Skeleton2), each with their own LeftEdge/RightEdge waypoints and SkelGroundCheck child. TurnAround() has 0.2s cooldown to prevent vibration at edges. Enemy has full animation set: idle, walk, hit, death, attack. Health event system (OnDamaged, OnDeath) drives animation reactions. Enemies are prefabs in Assets/Prefabs/Skeleton.prefab.

---

## 🏹 Section 8 — Shooting System → Melee Combat (Design Substitution)

| ✅ Done | ⚠️ Stuck | Task |
|:---:|:---:|---|
| [x] | | ~~Arrow prefab~~ → **Melee attack with OverlapCircle detection** |
| [x] | | ~~Arrow assigned to PlayerProjectile layer~~ → **EnemyLayer mask used for melee targeting** |
| [x] | | ~~`Arrow.cs`~~ → **`Combat.cs` handles attack direction** |
| [x] | | ~~Arrow destroys on hitting enemy~~ → **Enemy takes damage via Health.ChangeHealth()** |
| [x] | | ~~Arrow destroys on hitting ground~~ → **Attack radius confined to enemy layer only** |
| [x] | | ~~Arrow max lifetime~~ → **Attack cooldown (1.5s) prevents spam** |
| [x] | | ~~`PlayerShooter.cs`~~ → **`Combat.cs` fires on left-click (Fire1)** |
| [x] | | Fire rate limit prevents spam |
| [x] | | Attacks in the direction the player faces |
| [x] | | ~~`FirePoint`~~ → **`AttackPoint` child object positioned at sword tip** |

**Section Notes:** Shooting replaced with melee sword combat using Physics2D.OverlapCircle on AttackPoint. Intentional design decision — melee combat fits the dark dungeon aesthetic and skeleton enemy type better than projectiles. Includes attack animation, sword-slash SFX, knockback force applied to enemies, and floating damage numbers. Attack cooldown = 1.5s. Damage = 100 (one-shots enemies).

---

## ❤️ Section 9 — Health & Damage

| ✅ Done | ⚠️ Stuck | Task |
|:---:|:---:|---|
| [x] | | `PlayerHealth.cs` tracks current / max health |
| [x] | | UI hearts update when player takes damage |
| [x] | | Invincibility frames work after taking damage (flashing effect) |
| [x] | | Player death triggers game over flow |
| [x] | | `GameSession.cs` singleton persists across scenes |
| [x] | | Lives system: death decrements lives, reloads scene |
| [x] | | Out of lives → loads Game Over scene |
| [x] | | Hazard tiles damage/kill player on touch |

**Section Notes:** Max health = 3 hearts. Heart UI uses full/empty sprite swap. Invincibility duration = 1.5s with Mathf.Sin flashing. Death sets Rigidbody to Kinematic (prevents fall-through). GameSession tracks 3 lives, reloads scene on death, loads GameOver when lives = 0. Hazard.cs on Blood tilemap deals 999 damage (instant kill).

---

## 🎬 Section 10 — Scenes & Game States

| ✅ Done | ⚠️ Stuck | Task |
|:---:|:---:|---|
| [x] | | Build Settings has all scenes in correct order |
| [x] | | Main Menu scene with Start button works |
| [x] | | Game Over scene exists and loads correctly |
| [x] | | Level Exit triggers next level load |
| [x] | | Win scene or win condition implemented |

**Section Notes:** 4 scenes in Build Profiles: MainMenu (0), Level1 (1), GameOver (2), Win (3). MainMenuUI.cs loads Level1 on Start. GameOverUI.cs and WinUI.cs both safely destroy GameSession before reloading. LevelExit.cs on ExitDoor GameObject triggers Win scene when Player tag enters BoxCollider2D trigger.

---

## 🎵 Section 11 — Audio & Polish

| ✅ Done | ⚠️ Stuck | Task |
|:---:|:---:|---|
| [x] | | Background music loops on AudioSource |
| [x] | | Jump SFX plays when player jumps |
| [x] | | Shoot SFX plays when player fires (sword-slash plays on melee attack) |
| [x] | | Damage SFX plays when player is hit |
| [x] | | At least 1 particle effect (death particles on player death) |

**Section Notes:** AudioManager singleton with separate music and SFX AudioSources. Music: simplesound-scary-cinematic-background3.mp3 (looping). Jump: action_jump.mp3. Attack: sword-slash.mp3 (PlayHit() called in PlayerAttackState). Death: female-character-death-vocal.mp3. Death particles instantiated at player position on death.

---

## 🎨 Quality Standards

### Technical Quality
| ✅ | Standard |
|:---:|---|
| [x] | No null reference errors in Console during normal play |
| [x] | All scripts organized in correct subfolders |
| [x] | All tweakable values use `[SerializeField]` — no magic numbers |
| [x] | Enemies are **prefabs** (not unique scene objects) |
| [x] | DamageNumber is a **prefab** (melee substitution for Arrow prefab) |
| [x] | GameObjects are named clearly in Hierarchy |
| [x] | Unused GameObjects/scripts removed |

### Design Quality
| ✅ | Standard |
|:---:|---|
| [x] | Level has a clear start and end |
| [x] | Level has varied terrain (high platforms, low areas, gaps) |
| [x] | Difficulty ramps up through the level |
| [x] | Player can distinguish hazards from safe ground visually |
| [x] | Game is playable start to finish without bugs |

---

## 🚀 Extension Features (Optional — Choose 1+)

| ✅ | Difficulty | Feature |
|:---:|:---:|---|
| | 🟢 | Double jump |
| | 🟢 | Animated collectible coins/gems |
| | 🟢 | Multiple enemy types (reskins with different speeds) |
| | 🟡 | Checkpoint system |
| | 🟡 | Moving platform |
| | 🟡 | Enemy with ranged attack |
| | 🟡 | Score system with on-screen display |
| | 🔴 | Boss enemy with health bar |
| | 🔴 | Multiple levels (3+) with scene transitions |
| | 🔴 | Save system (high score persists between sessions) |

### Other Extensions: Enemy knockback on hit + floating damage numbers

**Brief description of what was added:** When the player hits a skeleton, a red damage number floats upward above the enemy's head using a Screen Space Overlay Canvas with WorldToScreenPoint conversion, fading out over 0.8 seconds. Enemies also get knocked back in the direction away from the player, with patrol movement paused for 0.2 seconds to allow the force to register. GameSession.cs includes AddScore() and GetScore() methods scaffolded for future score UI integration.

---

## 📤 Submission Checklist

| ✅ | Task |
|:---:|---|
| [x] | Project builds without errors (File → Build Settings → Build And Run) |
| [x] | Game is playable from Main Menu to Game Over/Win |
| [x] | Submitted GitHub repository link on Canvas |
| [x] | Repository is **public** |
| [x] | All 3 tutorial documents included in repo (`Tutorial.md`, `Checklist.md`, `QuickReference.md`) |
| [x] | Completed **Reflection Questions** (below) |

**GitHub Repository:** https://github.com/Rebeccals1/CryptCrawler-2DPlatform

---

## 🪞 Reflection Questions

**1. What was your rapid prototype goal for this project?**

I wanted to keep it simple. My goal was to build a functional 2D dungeon platformer with a player that could move, jump, and engage in melee combat with skeleton enemies patrolling a hand-crafted crypt level. I wanted to create a complete game loop from a start screen through to a win condition with a lives system.

**2. How closely did your final game match your original prototype vision? What changed and why?**

The final game was fairly close to my original vision. The main change was switching from a projectile shooting system to melee combat, which felt more appropriate for the dark dungeon aesthetic and the skeleton enemy type. The player state machine also evolved beyond what I originally planned, ending up more structured and scalable than a simple movement script. I used a full state machine pattern with separate classes for each state.

**3. What is the most technically challenging thing you implemented? How did you solve it?**

I spent A LOT of time on the tile maps. Also getting the player and skeleton animation systems working correctly. The Animator controller required precise parameter naming, correct transition conditions and proper C# event subscriptions across multiple scripts (Health, Enemy, EnemyPatrol). I solved it by debugging each layer. I had to make sure the parameter names matched the code, check to make sure any State transitions had conditions set and using Debug.Log to trace exactly where the event chain was breaking.

**4. If you had one more week, what would you add or change?**

I would add a proper boss enemy with a health bar UI, since I already have door/gate tiles that could serve as a boss room entrance. I would also add a checkpoint system so players don't restart the full level on death and maybe implement the score UI that is already scaffolded in GameSession.cs. Additional enemy types with different speeds and behaviors would also add more variety.

**5. How does your approach to game development now compare to when you started the course?**

I now think much more systematically about separating concerns. Like having dedicated scripts for health, patrol, combat, and state rather than putting everything in one place. I also have a much better understanding of how Unity's Animator controller works and how to wire C# events to drive both gameplay logic and visual feedback cleanly. I'm more comfortable debugging rather than guessing and I understand the importance of checking Inspector field assignments before assuming code is broken.

---

## 🆘 Stuck? Try These Steps

**Before asking for help:**
1. [ ] Read the **error message** in Console carefully — it tells you the file and line number
2. [ ] Check the **Inspector** — is every serialized field assigned?
3. [ ] Check **Tags and Layers** — are layers set correctly?
4. [ ] Checked the **Layer Collision Matrix**?
5. [ ] Searched the error message in the Unity Docs or Discord?
6. [ ] Tried rubber-duck debugging (explain the code out loud line by line)?

**If still stuck:** Post in Discord with:
- Screenshot of the **Console error**
- Screenshot of the **Inspector** for the relevant GameObject
- The **script** causing the issue

---

*CryptCrawler Checklist | CS 4700: Game Design Studio | Unity 6 + C# | Rebecca | March 2026*
