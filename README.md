# CyberCity (v1.6.0)
CyberCity is a turn based strategy game set in a cyberpunk setting. You progress through a maze, defeating enemies and picking up clues on your way.
Collect three clues and you win!

## How To Play
You can access the game on your browser via this [link](https://aadamhuda.github.io/CyberCityBuilds/v1.6.0/index.html).

To play:
- Use the WASD to move around the environment and your mouse to rotate
- When approaching the enemy, press the 'F' key to engage when in attacking radius
- Enemies can also  ambush you, where in battle they will deal 15% more damage, so try to get them before they get you.
  - To attack press the red button which says "ATTACK"
  - To change attack type, press the button which says "CHANGE ATTACK"
  - To heak press the "HEAL" button
  - Once an enemy is defeated you switch to the next enemy
  - Alternatively, you can press the "CHANGE TARGET" button to switch between the enemies
  - Also you can press the 'ESC' key to escape and leave the battle, and respawn in the spawn room
- If you pick up a clue it dissapears to show you picked it up
  - Press the 'k' key to pick it up
  - If you pick up three you win!
  
### Known Issues:
The player may respawn at spawn sometimes after battle [Issue #105](https://github.com/aadamhuda/CyberCity/issues/105)
  - A bug has been found where when the player object exits the battle after a win, the player sometimes does not spawn back in the location it engaged the enemy, but rather spawns at the spawn point.
  - The trigger is currently unknown and needs to be investigated into.

## Assets
[Cinemachine](https://unity.com/unity/features/editor/art-and-design/cinemachine)
  - Used for the camera
 
[Nav Mesh Components](https://github.com/Unity-Technologies/NavMeshComponents)
  - Defines where the enemies can or can't go

[Mixamo](https://www.mixamo.com/)
  - Used for character models
    - Player
    - Out of battle enemy
    - In battle enemy
    - Companion 1, 2, 3
    
  - Used for character animations
    - Idle
    - Walk
    - Slow run
    - Fast run
    - Player sword strike
    - Enemy sword strike
    - Walk backwards
    - Run backwards
    
[Out of Battle Enemy Weapon](https://sketchfab.com/3d-models/laser-sword-aka-cyber-katana-20-ea010b5ad8d34fce93b5ca4575635e1c)

### Previous releases:
- [Version 1.5.0](https://aadamhuda.github.io/CyberCityBuilds/v1.5.0/index.html)
- [Version 1.4.0](https://aadamhuda.github.io/CyberCityBuilds/v1.4.0/index.html)
- [Version 1.3.0](https://aadamhuda.github.io/CyberCityBuilds/v1.3.0/index.html)
- [Version 1.2.2](https://aadamhuda.github.io/CyberCityBuilds/v1.2.2/index.html)
- [Version 1.2.1](https://aadamhuda.github.io/CyberCityBuilds/v1.2.1/index.html)
- [Version 1.2.0](https://aadamhuda.github.io/CyberCityBuilds/v1.2.0/index.html)
- [Version 1.1.0](https://aadamhuda.github.io/CyberCityBuilds/v1.1.0/index.html)
- [Version 1.0.0](https://aadamhuda.github.io/CyberCityBuilds/v1.0.0/index.html)
- [Version 0.0.0](https://aadamhuda.github.io/CyberCityBuilds/v0.0.0/index.html)
