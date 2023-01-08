# CyberCity (v2.2.7)
CyberCity is a turn based strategy game set in a cyberpunk setting. You progress through two levels, the cargo level and city level, defeating enemies in strategic turn-based combat and picking up clues on your way that progress the story of our four characters: Nadiya, Dreyar, Astra and Joe. Collect all the clues in the two levels to complete the game, and explore the narrative of CyberCity!

This project was done for the Multi-Platform Games Development Module (ECS657U) at QMUL. Our game took inspiration from Persona 5 and many other turn-based strategy games. For a few of us in this group, it was our first time developing a game. This README also contains the builds from all our previous versions of the game, so feel free to try them out to see how our game progressed! :)

![menu_screen](https://user-images.githubusercontent.com/44711169/211208766-bc0ab7f0-a298-4ac9-b9ca-609a40479f5f.png)

We would like to thank you for playing CyberCity, and hope you enjoy it!

## Table of Contents
1. [How To Play](#how-to-play)
2. [Assets](#assets)
3. [Custom Assets](#custom-assets)
4. [Demo Videos](#demo-videos)
5. [Creators](#creators)
6. [Previous Releases](#previous-releases)

You can access the Builds repository via this [link](https://github.com/aadamhuda/CyberCityBuilds).

![city_load](https://user-images.githubusercontent.com/44711169/211208778-8719445e-7232-4c18-877b-8df40f843769.png)

## How To Play
You can access the game on your browser via this [link](https://aadamhuda.github.io/CyberCityBuilds/v2.2.7/index.html).

To play:
- Use the WASD to move around the environment and your mouse to rotate
- Use left mouse click to cycle through narrative pop ups
- When approaching the enemy, press the 'F' key to engage when in attacking radius, there will be a red indicator that will appear if you are in attacking radius
- Enemies can also  ambush you, where in battle they will deal 15% more damage, so try to get them before they get you.
  - To attack use the ability buttons to the right (all representative of different type of attacks), some attacks may consume MP
  - To heal press the "HEAL" button, this consumes MP
  - The "ITEM" button displays the items that can be used in battle, only one can be used a turn
  - The "ENEMY INFO" button displays the affinities of the current player and also the current targeted enemy, once you learn a weakness or strength of an enemy, then they'll appear on this menu
  - Use 'A' and 'D' to target different enemies in battle
  - Also you can press the "ESCAPE" button to escape and leave the battle, and return to a the last checkpoint you saved at.
- To save your current process press the 'O' key to open the save menu at a checkpoint, these will have a green ball above them, and have a blue triangular light near it too
- Press 'P' to access the pause menu
- Press 'E' to pick up items out of battle, these items can be used in battle to heal, revive or regenerate MP
  - These items have an orange indicator above when you are in range to pick it up
- If you pick up a clue it dissapears to show you picked it up
  - When in range, the clue will have a yellow indicator above to show that you are in range to pick it up
  - Press the 'E' key to pick it up
  - Once you pick up the four clues in the cargo level, and go to the green pad, you'll be taken to the next level (City level)
  - If you pick up all eight clues, and go to the green pad within the city level, you win!
  
![loading_screen_2](https://user-images.githubusercontent.com/44711169/211208808-e97cfe46-ad0d-464b-b098-a48e3bcde424.png)

## Assets
[Cinemachine](https://unity.com/unity/features/editor/art-and-design/cinemachine)
  - Used for the camera
 
[Nav Mesh Components](https://github.com/Unity-Technologies/NavMeshComponents)
  - Defines where the enemies can or can't go

[Mixamo](https://www.mixamo.com/)
  - Used for player and enemy models
  - Used for player and enemy animations

[Skybox Series](https://assetstore.unity.com/packages/2d/textures-materials/sky/skybox-series-free-103633)
  - Change the sky to a different weather setting

#### Cargo Level:
##### Walls:
- [Shipping Container 1](https://sketchfab.com/3d-models/ps1-style-shipping-containers-asset-pack-1b6abdb6e8f5402f8a258df6b07ae7a9)
- [Shipping Container 2](https://sketchfab.com/3d-models/cargo-container-82609a2050274620b5dd1e2098e17c73)
- [Cargo Ship](https://sketchfab.com/3d-models/cargo-ship-v2-pbr-109bfddb57d94cedb424ddcb42757e88)
- [Construction Site Items](https://sketchfab.com/3d-models/construction-site-low-poly-prop-set-cc261d95224242ab8f287e3a2cdf07cc)
##### Checkpoints:
- [Vending Machines](https://sketchfab.com/3d-models/soda-machines-d0b81fdb4e514859bfcc95165144e8c7#download)
- [LED Lights](https://sketchfab.com/3d-models/triangle-lights-b2ba5430c55148f283eab2178117ffc7)
##### Gate:
- [Gate](https://sketchfab.com/3d-models/animated-industrial-door-dbcd6eb9c9424dbfbb23caf7ab2f1536)
##### Flooring:
- [Floor Material](https://sketchfab.com/3d-models/sci-fi-floor-material-be0d40fbf5f44c388c94879801d3ab7b)
##### Lighting:
- [Lights](https://sketchfab.com/3d-models/scifi-light-12-cbd8826078314bb9b118971824c689ac)

#### City Level:
##### Walls:
- [Building 1](https://sketchfab.com/3d-models/armpit-city-960a3a67c1ee437c8e758287185b2396)
- [Building 2](https://sketchfab.com/3d-models/sc-fi-building-bb76aebb7dd947c8a4edf02ed12376ce)
- [Building 3](https://sketchfab.com/3d-models/sci-fi-tower-c271103cd2f84ed3992baeda6416bbb0#download)
- [Building 4](https://sketchfab.com/3d-models/sci-fi-building-7-b26b4ace15bd48259fcf235d31eec7c8)
- [Building 5](https://sketchfab.com/3d-models/sci-fi-building-4-a07af3b8c6c045d89a37a3b3cde7be1b)
- [Building 6](https://sketchfab.com/3d-models/sci-fi-building-10-08f1f006b4bb4e04831555cfc1a3ea49)
- [Building 7](https://sketchfab.com/3d-models/cyber-noddle-stand-2-f019fa3323f74d2fa170ff236d6839d9)
- [Building 8](https://sketchfab.com/3d-models/sci-fi-building-6-e1ec43d7bf1146789553000f49e7e060)
- [Building 9](https://sketchfab.com/3d-models/futuristic-building-555daf778aa245158e4b24214ccc1dbb)
- [Building 10](https://sketchfab.com/3d-models/sci-fi-city-public-domain-cc0-b353532235bb4c45afeac578187c9be1)
- [Building 11](https://sketchfab.com/3d-models/cyberpunk-building-e79154e588bd4a6c84fa07e9a53f3ff9)
- [Building 12](https://sketchfab.com/3d-models/guns-and-ammo-eed0704c03b64b15813981d32dd421f9)
- [Building 13](https://sketchfab.com/3d-models/japanese-house-20-5ae0121e33b54b7789c6b9113da444bf)
- [Building 14](https://sketchfab.com/3d-models/subway-bar-900fcf289b1b4c3cbc7f595684ca033a)
- [Building 15](https://sketchfab.com/3d-models/vehicle-foundry-building-f92fa65a630844d9954d56f0330af3c4)
- [Building 16](https://sketchfab.com/3d-models/researchcenter-building001-ecfa6e9201d94a04b04b4fdd67e418e9)
- [Building 17](https://sketchfab.com/3d-models/scifi-building-1220360fcebc4aa0874f1ca6bcb4cb30)
- [Building 18](https://sketchfab.com/3d-models/pub-scene-9f10ca63a4454d5a90fcd1ddc6298d7e)
- [Item](https://sketchfab.com/3d-models/sci-fi-portable-generator-a6e84c36f4af4149be1a8518e7331495)
- [Bus Stop](https://sketchfab.com/3d-models/bus-stop-90fbd043c83a423598d425bb3a641dfc)
- [Garbage 1](https://sketchfab.com/3d-models/dumpster-cyberpunk-game-assets-b0d0304c927b450d904163fa43978519)
- [Garbage 2](https://sketchfab.com/3d-models/astas-treasures-7dc5903c241c48d9989e10396e82f90e)
- [Telephone Box](https://sketchfab.com/3d-models/phone-box-low-6051df01fb0d4770ae6b42e35e6730bd)
- [Aircon Set](https://sketchfab.com/3d-models/lowpoly-airco-set-bangkok-cityscene-2571ba721a9246cb930bba77428d99aa)
- [Vending Machine](https://sketchfab.com/3d-models/bepis-vending-machine-6f736e1267b5404aae51b518a417e814)
##### Checkpoints:
- [Vending Machine](https://sketchfab.com/3d-models/kitsch-vending-machine-8ce19438dd1d49a2ad7f709024f18dcd)
##### Gate:
- [Gate](https://sketchfab.com/3d-models/sci-fi-gate-detailing-01-xyz-school-hw-d7acd3160f694519ba6adda2eb915247)
##### Flooring:
- [Floor Material](https://sketchfab.com/3d-models/floor-material-fb32b41a61f447a8bb4e6218d7920a02)
##### Lighting:
- [Neon Signs](https://sketchfab.com/3d-models/neon-signs-307e887d740649f88fbc77b061f3a742)

#### Weapons:
- [Sword 1](https://sketchfab.com/3d-models/future-sword-gad174-c9f08775b01545aaba781b76e8e818e0)
- [Sword 2](https://sketchfab.com/3d-models/thermal-katana-remake-97566e9dafc14a9d88053baf703d332f)
- [Sword 3](https://sketchfab.com/3d-models/cyber-blade-7a9afdc5ecb241a298ba3b278bb88680)
- [Sword 4](https://sketchfab.com/3d-models/laser-sword-aka-cyber-katana-20-ea010b5ad8d34fce93b5ca4575635e1c)
- [Bow](https://sketchfab.com/3d-models/futuristic-bow-clustersky-8b4ef43898384c0187fed7969db04a5b)

#### Objects:
- [Clue](https://sketchfab.com/3d-models/muta-gen1b12-fractal-photogrammetry-b48965996f064ff6ae841f047ce9218f)
- [Item](https://sketchfab.com/3d-models/small-bottle-761e522abb934b0a98063a9851728180#download)

#### Fonts:
- [Ashur Punk Font](https://www.fontspace.com/asher-punk-font-f55489)
- [Cyberway Riders Font](https://www.fontspace.com/cyberway-riders-font-f43849)
- [Tex Gyre Adventor Font](https://www.1001fonts.com/tex-gyre-adventor-font.html)
- [Rajdhani](https://fonts.google.com/specimen/Rajdhani?preview.text=Rajdhani&preview.text_type=custom#standard-styles)

#### Music:
- [Hybrid Cinematic Rock](https://pixabay.com/music/rock-hybrid-cinematic-rock-84238/)
- [Lifelike](https://pixabay.com/music/future-bass-lifelike-126735/)
- [Dark Ambient](https://pixabay.com/music/ambient-dark-ambient-126122/)

#### Animation Sounds:
- [RPG Essentials Sound Effects](https://assetstore.unity.com/packages/audio/sound-fx/rpg-essentials-sound-effects-free-227708)
- [Shooting Sound](https://assetstore.unity.com/packages/audio/sound-fx/shooting-sound-177096)
- [Fantasy SFX for Particle Distort Texture Effect Library](https://assetstore.unity.com/packages/audio/sound-fx/foley/fantasy-sfx-for-particle-distort-texture-effect-library-42146#content)
- [Footsteps](https://assetstore.unity.com/packages/audio/sound-fx/foley/footsteps-essentials-189879)

#### Partical & Effects:
- [Special Effects](https://assetstore.unity.com/packages/vfx/particles/spells/52-special-effects-pack-10419)
- [Status Effects](https://assetstore.unity.com/packages/vfx/particles/spells/status-effects-free-238904)

## [Custom Assets](https://github.com/aadamhuda/CyberCity/tree/main/gameDev-GroupH/Assets/Custom%20assets) 
All custom assets designed by [@sarahbrah7](https://github.com/sarahbrah7)
  - Dialogue Boxes 
  - Characters Drawing
  - Loading Screens
    - For both levels
  - Pause Screen
  - Button
  - Menu Screen
  - Lose Screen 
  - Win Screen
  - Item Drawings 
 
![ether_item1](https://user-images.githubusercontent.com/44711169/211208884-236d03db-cb4e-4c52-9a3f-db5a55e6d36e.png)
![ether_item2](https://user-images.githubusercontent.com/44711169/211208886-2b57ab7d-d7a7-4c14-bbff-b75728ecff15.png)
![potion_item1](https://user-images.githubusercontent.com/44711169/211208887-e2009013-76b5-44f2-9512-177eafd45ec3.png)
![potion_item2](https://user-images.githubusercontent.com/44711169/211208888-99f5fde0-5327-44ca-acc8-2f31c7af4e20.png)
![revive_item1](https://user-images.githubusercontent.com/44711169/211208889-68197ae0-f91a-4a70-a62f-b08475d742e7.png)
![revive_item2](https://user-images.githubusercontent.com/44711169/211208891-530cc428-76a7-4468-bc01-ff954e16296b.png)

## Demo Videos:
  - [Final Game Demo](https://youtu.be/F5lDAzxZ9wQ) by [@Ibraheem717](https://github.com/Ibraheem717)
  - [First Prototype Demo](https://youtu.be/kIIAM0zeU-E) by [@Ibraheem717](https://github.com/Ibraheem717)

## Creators:
I'd like to thank all the Creators of this game with their amazing contributions! The creators all contributed highly to the completion of the final game. For some creators, this was their first game development project, and for some, their first time exploring the mechanics of video games. Creator contribution can be found in the [changelog](https://github.com/aadamhuda/CyberCity/blob/main/CHANGELOG.md) 
- [@aadamhuda](https://github.com/aadamhuda)
- [@Ibraheem717](https://github.com/Ibraheem717)
- [@gv301](https://github.com/gv301)
- [@HinaMatsuzaki](https://github.com/HinaMatsuzaki)
- [@sarahbrah7](https://github.com/sarahbrah7)

## Previous releases:
- [Version 2.2.6](https://aadamhuda.github.io/CyberCityBuilds/v2.2.6/index.html)
- [Version 2.2.5](https://aadamhuda.github.io/CyberCityBuilds/v2.2.5/index.html)
- [Version 2.2.4](https://aadamhuda.github.io/CyberCityBuilds/v2.2.4/index.html)
- [Version 2.2.3](https://aadamhuda.github.io/CyberCityBuilds/v2.2.3/index.html)
- [Version 2.2.2](https://aadamhuda.github.io/CyberCityBuilds/v2.2.2/index.html)
- [Version 2.2.1](https://aadamhuda.github.io/CyberCityBuilds/v2.2.1/index.html)
- [Version 2.2.0](https://aadamhuda.github.io/CyberCityBuilds/v2.2.0/index.html)
- [Version 2.1.0](https://aadamhuda.github.io/CyberCityBuilds/v2.1.0/index.html)
- [Version 2.0.0](https://aadamhuda.github.io/CyberCityBuilds/v2.0.0/index.html)
- [Version 1.11.1](https://aadamhuda.github.io/CyberCityBuilds/v1.11.1/index.html)
- [Version 1.11.0](https://aadamhuda.github.io/CyberCityBuilds/v1.11.0/index.html)
- [Version 1.10.0](https://aadamhuda.github.io/CyberCityBuilds/v1.10.0/index.html)
- [Version 1.9.0](https://aadamhuda.github.io/CyberCityBuilds/v1.9.0/index.html)
- [Version 1.8.0](https://aadamhuda.github.io/CyberCityBuilds/v1.8.0/index.html)
- [Version 1.7.0](https://aadamhuda.github.io/CyberCityBuilds/v1.7.0/index.html)
- [Version 1.6.1](https://aadamhuda.github.io/CyberCityBuilds/v1.6.1/index.html)
- [Version 1.6.0](https://aadamhuda.github.io/CyberCityBuilds/v1.6.0/index.html)
- [Version 1.5.0](https://aadamhuda.github.io/CyberCityBuilds/v1.5.0/index.html)
- [Version 1.4.0](https://aadamhuda.github.io/CyberCityBuilds/v1.4.0/index.html)
- [Version 1.3.0](https://aadamhuda.github.io/CyberCityBuilds/v1.3.0/index.html)
- [Version 1.2.2](https://aadamhuda.github.io/CyberCityBuilds/v1.2.2/index.html)
- [Version 1.2.1](https://aadamhuda.github.io/CyberCityBuilds/v1.2.1/index.html)
- [Version 1.2.0](https://aadamhuda.github.io/CyberCityBuilds/v1.2.0/index.html)
- [Version 1.1.0](https://aadamhuda.github.io/CyberCityBuilds/v1.1.0/index.html)
- [Version 1.0.0](https://aadamhuda.github.io/CyberCityBuilds/v1.0.0/index.html)
- [Version 0.0.0](https://aadamhuda.github.io/CyberCityBuilds/v0.0.0/index.html)
