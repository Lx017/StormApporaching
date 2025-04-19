Windows Build tested only! The build is in Builds/ folder

Start Scene File: GameMenuScene
Game Scene File: MapScene 2
 
## Gameplay Introduction

In the Alpha version, players will become familiar with character movement and combat mechanics while battling enemies.  
By defeating enemies, players can enhance their abilities and unlock the stone gate leading to the next level.  

Additionally, players can explore the map, interact with objects, and discover hidden treasure chests.

## Known Issues

- Enemies may continue to move before being completely destroyed after death.
- If the player's speed is enhanced excessively, animation errors may occur.
- Mob cannot walk up slopes

---

## Contributions

### Waiyuk Kwong

- **Motion Matching-related assets:** `Assets/Animation/AnimResource/Motion Matching`
- **Player movement/combat state machine & data & utilities:** All scripts in `Assets/Script/Characters/Player`
- **Player attack logic:** `MeleeWeapon.cs`
- **Enemy AI base:** `Golem Enemy AI.cs`
- **Enemy attack logic:** `EnemyDamageArea.cs`, `AttackColliderBehavior.cs`

### Jincheng Song

- **Game balancing:** `ActorStats.cs`, `EnemyStats.cs`, `MeleeWeapon.cs`, `PlayerStats.cs`, `WeaponBase.cs`
- **Hit impact effects (character pause, camera shake):** `CameraShake.cs`, `HitStop.cs`
- **UI development:** `UIManager.cs`, `GameManager.cs`
- **Treasure chest interactions:** `OpenableChest.cs`
- **Initial menu creation**

### Xiao Lin

- **Sound effects system:** `SoundEffectPlayer.cs`
- **Particle effects**
- **Hit effects**
- **Bullet physics, explosion machanisms**
- **Special effects and sound effects bound to animations and events (e.g., enemy death)**

### Leyao Zhang

- **Trap mechanics:** `ArrowSpawn.cs`, `BearTrapClamp.cs`
- **Trap animations:** `BearTrap_animator.controller`

### Yuxuan Li

- **Enemy design:** **Ice Golem, Fire Golem**
- **Camera freeze mechanics:** `CameraLockEvent.cs`
- **Mouse management:** `MouseManager.cs`, `MouseControlEvent.cs`