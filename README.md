# eFootball 2027 Game

A complete football game developed in Unity with AI opponents, realistic ball physics, and full match management.

## Features

✅ **Player Controls**
- WASD for movement
- Space to jump
- Left Mouse to pass
- Right Mouse to shoot
- E to tackle/slide
- Q for super shot

✅ **Game Mechanics**
- Realistic ball physics
- AI opponent players
- Passing and shooting system
- Tackle and sliding mechanics
- Match scoring and stats

✅ **UI & Camera**
- Dynamic follow camera
- Score display
- Match timer
- Player stats
- Pause menu

✅ **Match System**
- 90-minute matches (configurable)
- 11vs11 gameplay
- Real-time scoring
- Win/Draw/Loss conditions

## Installation

1. Clone this repository
2. Open in Unity (2021 LTS or newer)
3. Load the Main scene from Assets/Scenes/
4. Press Play

## Project Structure

```
Assets/
├── Scripts/
│   ├── Player/
│   ├── Ball/
│   ├── AI/
│   ├── Match/
│   ├── UI/
│   └���─ Camera/
├── Scenes/
├── Prefabs/
├── Materials/
└── Audio/
```

## Controls

| Action | Input |
|--------|-------|
| Move | WASD |
| Jump | Space |
| Pass | Left Mouse |
| Shoot | Right Mouse |
| Tackle | E |
| Super Shot | Q |
| Pause | ESC |

## Game Features

### Player Controller
- Smooth movement with WASD
- Sprint with Shift
- Jump mechanics with ground detection
- Ball interaction (pass, shoot, super shot, tackle)
- Customizable speed and force values

### Ball Physics
- Realistic friction and velocity limits
- Bounce mechanics on collision
- Force application system
- Position reset for goal scenarios

### AI System
- Autonomous player movement
- Ball tracking and pursuit
- Intelligent shooting and passing
- Patrol behavior when ball is distant

### Match Management
- 90-minute match timer
- Real-time scoring
- Goal detection system
- Match end conditions (Win/Loss/Draw)
- UI updates for score and timer

### Camera System
- Dynamic follow camera
- Smooth tracking of player
- Zoom in/out with mouse scroll
- Automatic look-at player position

## Setup Instructions

1. Create a new Unity project (2021 LTS or newer)
2. Copy the Assets folder into your project
3. Create a Main scene with:
   - A ground plane (tag: "Ground")
   - Two goal areas (tag: "Goal")
   - Player spawn point
   - AI player spawn points
4. Add the scripts to corresponding GameObjects
5. Configure the serialized fields in Inspector
6. Play!

## Development

Made with Unity and C#

## License

MIT License
