# vr-lab-project: Virtual Reality (VR) FPS Game

## Overview
**Sntruggled** is a VR zombie survival game where you fight off endless waves of cats using various gun weapons. Set in a world where you are a stuffed animal in a warehouse, you must find “a way out” through aim and exploration.

## Game Components
### Objects:
- **Gun Models:** Multiple gun types with different firearms, designs, and playstyles.
- **Spawners:** Wave-spawner points for cat enemies.
- **Mystery Boxes:** Uses in-game coins to open and receive a random gun.

### Attributes:
- **Physics Engine:** Somewhat accurate cat animation with human-like movement.
- **Cat Enemies:** Cats that follow the player, trying to feed the player a cupcake.
- **Scoring System:** Based on the overall point score earned by killing cat enemies.
- **Currency System:** Earn coins by killing cat enemies.

### Relationships:
- **Player to Weapon:** Different firearms allow varied playstyles.
- **Player to Environment:** Interaction flashlight and mystery boxes.
- **Player to Game World:** Immersive engagement through VR movement, aiming controls, and 3D spatial audio.

### Environments:
- **Visual Settings:** Immersive map with expandable portions.
- **Audio Landscape:** 3D spatial audio for cat enemy sounds and environmental effects.
- **User Interface:** HUD displaying health, ammo, coins, points, and wave spawn number/time countdown.
- **VR Compatibility:** Support for leading VR headsets for an immersive experience.

## Development Timeline and Tasks
### Week 1: Planning and Design
- **Concept Finalization:** Define core mechanics, objectives, and unique selling points.
- **Technical Specifications:** Determine hardware and software requirements.
- **Team Assignment:** Allocate roles to developers, designers, and testers.

### Weeks 2-4: Development
- **3D Modeling:** Create character and toy gun models.
- **Physics Engine Integration:** Implement toy movement, ragdoll physics, and weapon handling.
- **VR Interface Design:** Develop VR controls and weapon usage.
- **Gameplay Mechanics:** Implement wave-spawning, multiplayer mode, and scoring systems.

### Week 5: Testing and Feedback
- **Internal Testing:** Identify and resolve critical bugs and performance issues.
- **User Feedback Sessions:** Gather input to refine mechanics, controls, and user experience.

### Week 6: Finalization and Deployment
- **Polishing:** Enhance visuals, optimize performance, and implement feedback.
- **Deployment:** Release on selected platforms with compatibility testing.

## Equipment and Software Requirements
### Hardware:
- **VR Headsets:** Meta Quest 3 for development and testing.
- **Development Workstations:** High-performance PCs with NVIDIA RTX GPUs.
- **Control Devices:** Standard game controllers.

### Software and Tools:
- **Game Engine:** Unity with XR Toolkit.
- **3D Modeling:** Unity's Asset Store; Blendr.
- **Audio Design Tools:** Unity's built-in sound editor.
- **Version Control Systems:** GitHub for source code and asset management.

By following this structured plan, this development team aims to deliver a high-quality VR Zombie Survival Game within a 1.5-month development timeframe.

## Setup Instructions 
- Blendr, Unity, and this project should be installed
- Open this project in Unity.
- Once Blendr is installed, the assets should be loaded
- If not, then open Blendr and drag the assets into it
- Close Blendr then go to Unity and the scenes/assets should reload with the proper assets
- As long as 1 Start Scene is the only scene loaded, pressing play on Unity should start the game just fine!
  
## Member Contributions 
- William Licup: Worked on gun and chest functionality
- Nathan Lam: Set up environment including map, enemies, wave spawners
- Angela Santos: Worked on main menu, UI, HUD display, score manager
- Emily Tran: Worked on enemy AI and animation, and healing system
