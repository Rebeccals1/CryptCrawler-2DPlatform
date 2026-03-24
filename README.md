# ⚔️ CryptCrawler — 2D Platformer

> A dark dungeon platformer built in Unity 6 with C# | CS 4700: Game Design Studio

---

## Gameplay Overview

CryptCrawler is a 2D side-scrolling platformer set in a dark underground dungeon. The player fights through a crypt filled with patrolling skeleton enemies, deadly blood hazards, and treacherous platforms. Survive long enough to reach the exit door and escape.

---

## Features

- **Melee combat system** — swing your sword with attack radius detection, cooldown, and knockback
- **Skeleton enemy AI** — patrols between waypoints, detects ledges, reacts to damage with hit/death animations
- **Floating damage numbers** — screen-space damage numbers float above enemies on hit
- **Heart UI** — 3-heart health system with invincibility frames and sprite flashing on damage
- **Hazard tiles** — blood floor tiles that instantly kill the player on contact
- **Camera shake** — Cinemachine-powered shake on player damage
- **Audio** — background music, jump SFX, sword hit SFX, death SFX
- **Lives system** — 3 lives before Game Over, scene reloads on death
- **Full scene flow** — Main Menu → Level1 → Game Over / Win

---

## Controls

| Action | Key |
|---|---|
| Move | A / D or Arrow Keys |
| Jump | Space |
| Attack | Left Click |

---

## Architecture

The player uses a **state machine** pattern with dedicated state classes:

```
Player.cs (MonoBehaviour)
├── PlayerIdleState
├── PlayerMoveState
├── PlayerJumpState
└── PlayerAttackState
```

Each state handles its own input, physics, and animation transitions — keeping the code modular and easy to extend.

---

## Project Structure

```
Assets/
├── Animations/       # Player and skeleton animation clips + controllers
├── Art/              # Sprites for player, skeleton, environment, UI
├── Audio/            # Music and SFX (.mp3)
├── Prefabs/          # Player, Skeleton, DamageNumber, Doors
├── Scenes/           # MainMenu, Level1, GameOver, Win
├── Scripts/
│   ├── Combat/       # Combat.cs, Hazard.cs
│   ├── Core/         # GameSession, AudioManager, CameraShake, LevelExit,
│   │                 # GameOverUI, WinUI, MainMenuUI, DamageNumber
│   ├── Enemy/        # Enemy, EnemyPatrol, EnemyHurt, Health
│   └── Player/       # Player, PlayerHealth, PlayerState + all state classes
├── Tiles/            # Tilemap tile assets (platforms, midground, doors, hazards)
└── Rules/            # Rule tile assets for auto-tiling
```

---

## Scripts Overview

| Script | Purpose |
|---|---|
| `Player.cs` | Core player controller, state machine, input, physics |
| `PlayerHealth.cs` | Heart UI, invincibility frames, Kinematic on death |
| `Combat.cs` | Melee attack with OverlapCircle, cooldown, knockback direction |
| `EnemyPatrol.cs` | Waypoint patrol, ledge detection, turn cooldown, knockback timer, death |
| `Enemy.cs` | Subscribes to Health events, triggers hit/death animations |
| `Health.cs` | Reusable health — events, knockback force, damage number spawning |
| `GameSession.cs` | Singleton — manages lives, scene loading, score scaffold |
| `AudioManager.cs` | Singleton — music and SFX playback |
| `CameraShake.cs` | Cinemachine perlin noise shake on damage |
| `Hazard.cs` | Trigger-based instant kill on hazard tilemap contact |
| `LevelExit.cs` | Triggers Win scene when player enters exit zone |
| `DamageNumber.cs` | Floating damage text — rises, fades, destroys over 0.8s |
| `MainMenuUI.cs` | Start button loads Level1 |
| `GameOverUI.cs` | Play Again — safely destroys GameSession, reloads Level1 |
| `WinUI.cs` | Play Again — safely destroys GameSession, reloads Level1 |

---

## Built With

- **Unity 6** (2D URP)
- **C#**
- **Cinemachine** — camera follow, confiner, and shake
- **2D Tilemap Extras** — Rule Tiles for auto-tiling
- **Unity Input System** — new input action system
- **TextMeshPro** — damage number UI

---

## Extension Features Implemented

- ✅ Enemy knockback on hit — force applied in direction away from player, patrol paused 0.2s
- ✅ Floating damage numbers — screen-space canvas with WorldToScreenPoint conversion
- ✅ Score system scaffold — GameSession.AddScore() / GetScore() ready for UI integration

---

## Scene Flow

```
MainMenu → [Start] → Level1
Level1   → [Die × 3]   → GameOver → [Play Again] → Level1
Level1   → [Exit Door]  → Win      → [Play Again] → Level1
```

### 🎨 Pixel Art Attribution
Special thanks to the following talented pixel artists whose assets helped bring *CryptCrawler* to life:
- [Anokolisa](https://anokolisa.itch.io/) — for stylish character and effect designs
We appreciate their incredible work and contributions to the indie game development community.
<br></br>

## License
This project is licensed under the [MIT License](LICENSE).
---

*CryptCrawler | CS 4700: Game Design Studio | Cal Poly Pomona | March 2026*
