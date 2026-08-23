# Doofus Adventure

A 3D Unity game developed for the Hitwicket Game Developer Challenge 2026.

## Overview

Doofus Adventure is a simple platform-survival game where the player guides Doofus across short-lived pulpits. Each pulpit has a limited lifetime, so the player must reach the next pulpit before the current one disappears.

The game is built in Unity 6 and uses a JSON configuration file (Doofus Diary) for gameplay values.

## Features

- WASD and Arrow Key movement
- JSON-driven player speed and pulpit timing
- Random pulpit lifetime based on the provided configuration
- Maximum of two pulpits active at a time
- Direction-aware pulpit spawning to keep the next platform reachable and reduce luck-based layouts
- Score increases when Doofus reaches a new pulpit
- Countdown timer shown for the current pulpit
- Game Over when Doofus falls or the pulpit under him disappears
- Restart and Menu flow
- Persistent high score
- Start screen and How To Play screen
- Color variation between spawned pulpits
- Simple 3D presentation with a follow camera and polished UI

## Controls

| Input | Action |
|---|---|
| W / Up Arrow | Move forward |
| S / Down Arrow | Move backward |
| A / Left Arrow | Move left |
| D / Right Arrow | Move right |

## Configuration

Gameplay values are read from `Assets/Config/doofus_diary`.

Current configuration:

```json
{
  "player_data": {
    "speed": 3
  },
  "pulpit_data": {
    "min_pulpit_destroy_time": 4,
    "max_pulpit_destroy_time": 5,
    "pulpit_spawn_time": 2.5
  }
}
```

## Main Scripts

- `DoofusController.cs` — player movement, pulpit detection, scoring trigger, and fall detection
- `PulpitController.cs` — pulpit lifetime, countdown timer, and destruction handling
- `PulpitSpawner.cs` — JSON-driven pulpit spawning and direction-aware placement
- `ScoreManager.cs` — score tracking
- `GameManager.cs` — game-over, restart, menu flow, and high score handling
- `StartMenuController.cs` — start menu and instructions screen
- `CameraFollow.cs` — camera follow behaviour

## Scenes

- `StartScene` — title screen, Start button, and How To Play screen
- `GameScene` — gameplay, score, timer, pulpit spawning, and Game Over UI

## Assignment Levels

### Level 1 — Character Movement and Platform Placement

Implemented JSON-driven player movement and pulpit spawning/lifetime behaviour.

### Level 2 — Score System

Score increases after Doofus successfully moves onto a different pulpit.

### Level 3 — Start and Game Over Screens

Implemented the Start screen, How To Play screen, Game Over screen, Restart flow, and Menu flow.

## Project Requirements

- Unity 6+
- 3D project
- Git / GitHub

## Repository

This project was created for the Hitwicket Game Developer Challenge 2026.
