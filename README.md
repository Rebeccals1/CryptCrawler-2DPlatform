# ⚔️ CryptCrawler — 2D Platformer

> A dark dungeon platformer built in Unity 6 with C# | CS 4700: Game Design Studio

---

## 🎮 Gameplay Overview

CryptCrawler is a 2D side-scrolling platformer set in a dark underground dungeon. The player fights through a crypt filled with patrolling skeleton enemies, deadly blood hazards, and treacherous platforms. Survive long enough to reach the exit door and escape.

---

## ✨ Features

- **Melee combat system** — swing your sword with attack radius detection, cooldown, and knockback
- **Skeleton enemy AI** — patrols between waypoints, detects ledges, reacts to damage with hit/death animations
- **Floating damage numbers** — visual feedback showing damage dealt above enemies
- **Heart UI** — 3-heart health system with invincibility frames and sprite flashing on damage
- **Hazard tiles** — blood floor tiles that instantly kill the player on contact
- **Camera shake** — Cinemachine-powered shake on player damage
- **Audio** — background music, jump SFX, sword hit SFX, death SFX
- **Lives system** — 3 lives before Game Over, scene reloads on death
- **Full scene flow** — Main Level → Game Over → Win

---

## 🕹️ Controls

| Action | Key |
|---|---|
| Move | A / D or Arrow Keys |
| Jump | Space |
| Attack | Left Click |

---

## 🏗️ Architecture

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

## 📁 Project Structure

```
Assets/
├── Animations/       # Player and skeleton animation clips + controllers
├── Art/              # Sprites for player, skeleton, environment, UI
├── Audio/            # Music and SFX (.mp3)
├── Prefabs/          # Player, Skeleton, DamageNumber, Doors
├── Scenes/           # Level1, GameOver, Win
├── Scripts/
│   ├── Combat/       # Combat.cs, Hazard.cs
│   ├── Core/         # GameSession, AudioManager, CameraShake, LevelExit, GameOverUI, WinUI, DamageNumber
│   ├── Enemy/        # Enemy, EnemyPatrol, EnemyHurt, Health
│   └── Player/       # Player, PlayerHealth, PlayerState + all state classes
├── Tiles/            # Tilemap tile assets (platforms, midground, doors, hazards)
└── Rules/            # Rule tile assets for auto-tiling
```

---

## 🧠 Scripts Overview

| Script | Purpose |
|---|---|
| `Player.cs` | Core player controller, state machine, input, physics |
| `PlayerHealth.cs` | Heart UI, invincibility frames, death flow |
| `Combat.cs` | Melee attack with OverlapCircle, cooldown, knockback direction |
| `EnemyPatrol.cs` | Waypoint patrol, ledge detection, turn cooldown, death |
| `Enemy.cs` | Subscribes to Health events, triggers hit/death animations |
| `Health.cs` | General-purpose health component with events, knockback, damage numbers |
| `GameSession.cs` | Singleton — manages lives, scene loading, score |
| `AudioManager.cs` | Singleton — music and SFX playback |
| `CameraShake.cs` | Cinemachine perlin noise shake on damage |
| `Hazard.cs` | Trigger-based instant kill on hazard tilemap contact |
| `LevelExit.cs` | Triggers next level load when player enters exit zone |
| `DamageNumber.cs` | Floating damage text that rises and fades above enemies |

---

## 🔧 Built With

- **Unity 6** (2D URP)
- **C#**
- **Cinemachine** — camera follow and shake
- **2D Tilemap Extras** — Rule Tiles for auto-tiling
- **Unity Input System** — new input action system
- **TextMeshPro** — damage number UI

---

## 🚀 Extension Features Implemented

- ✅ Floating damage numbers above enemies
- ✅ Enemy knockback on hit
- ✅ Score system scaffold (GameSession.AddScore)

---

## 🪞 Reflection

**Prototype goal:** Build a functional 2D dungeon platformer with combat and enemy AI.

**Biggest change from prototype:** Switched from a projectile shooting system to melee combat — felt more appropriate for the dungeon aesthetic and skeleton enemy type. The player state machine also evolved beyond a simple movement script into a fully modular architecture.

**Most challenging thing:** Getting the skeleton animation system working — parameter naming, transition conditions, and event subscriptions all had to align perfectly across multiple scripts.

**If I had one more week:** Add a boss enemy, checkpoint system, and additional enemy types with different behaviors.

---

## 📋 Submission

- Course: CS 4700 — Game Design Studio
- Engine: Unity 6
- Language: C#
- Docs: `Tutorial.md` | `Checklist.md` | `QuickReference.md`
