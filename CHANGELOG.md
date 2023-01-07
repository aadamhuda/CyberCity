
# Change Log  
All notable changes to this project will be documented in this file.
      
The format is based on [Keep a Changelog](http://keepachangelog.com/)
and this project adheres to [Semantic Versioning](http://semver.org/).

## [Unreleased]
### Added

### Changed

### Deprecated

### Removed

### Fixed

### Security

## [2.1.0](https://github.com/aadamhuda/CyberCity/releases/tag/v2.1.0) - 2022-12-26
### Added
- Added enemies and enemy paths to new enviroment ~ [@Ibraheem717](https://github.com/Ibraheem717)
- Added navmesh to new enviroment ~ [@Ibraheem717](https://github.com/Ibraheem717)
- Added attack effect animations and sound ~ [@gv301](https://github.com/gv301)
  - Currently completed fire
- Doors now animate ~ [@Ibraheem717](https://github.com/Ibraheem717)
  - When the player has collected 2 clues the first door will open
  - The secound will open on 4 clues
  - The doors will remain open if the player has collected clues
- Added invisible walls to prevent the player from falling off the map ~ [@Ibraheem717](https://github.com/Ibraheem717)
- Added floor map to the city level ~ [@sarahbrah7](https://github.com/sarahbrah7)
- Enemies now remember player attributes ~ [@Ibraheem717](https://github.com/Ibraheem717)
  - Whenever enemies attack players they log their attack
  - The attack is specific to each companion and affinity
  - The enemies now attack players with known weaknesses
  - Otherwise they'll attack randomly
### Changed
- Updated sword models ~ [@gv301](https://github.com/gv301)
- Scene changes ~ [@Ibraheem717](https://github.com/Ibraheem717)
  - Scenes now are changed with names rather than index
  - To change scene use the getter method in SaveData and get the right scene 
  - All scene will be stored in SaveData 

## [2.0.0](https://github.com/aadamhuda/CyberCity/commit/5debe407f5656ddf563416298ebb115cb68bbd18) - 2022-12-24
### Added
- Added item spawner ~ [@HinaMatsuzaki](https://github.com/HinaMatsuzaki)
  - If you duplicate item spawner object, you can add a floor panel and the number of pickups and it will randomly create the pickups on the surface
- Added audio and sensitivity ~ [@HinaMatsuzaki](https://github.com/HinaMatsuzaki)
- Added sounds to animations ~ [@gv301](https://github.com/gv301)
  - Player heal and attack animation sounds
  - Enemy attack animation sound
- Mouse sensitivity slider now changes camera sensitivity ~ [@gv301](https://github.com/gv301)
- Added reaction animations to being hit ~ [@gv301](https://github.com/gv301)
- Created a battle animator which handles animations and audio ~ [@gv301](https://github.com/gv301)
- Added floor map of the cargo level ~ [@sarahbrah7](https://github.com/sarahbrah7)
- Added multiple enemy models, and more enemy types  for enemy variety ~ [@aadamhuda](https://github.com/aadamhuda)
- Added random enemy system which randomly picks the types of the three enemies that the player will face in battle ~ [@aadamhuda](https://github.com/aadamhuda)
- When picking up items, it will appear in inventory ~ [@Ibraheem717](https://github.com/Ibraheem717)
  - To access inventory, Pause then click inventory
  - Right now there's no inventory label so click the empty one
- Inventory is loaded onto battle ~ [@Ibraheem717](https://github.com/Ibraheem717)
- Player can save on checkpoints ~ [@Ibraheem717](https://github.com/Ibraheem717)
  - Loads on most recent save 
  - Save takes in account of:
    - Enemies killed
    - Items taken
    - Player save location
    - Clues 
- Added MP system, attacks have an MP cost and cannot be used if the user has insufficient Magic Points ~ [@aadamhuda](https://github.com/aadamhuda)
- Linked MP system to the Player HUD for real time status of the Players Magic points ~ [@aadamhuda](https://github.com/aadamhuda)
- Added MP save after a battle ~ [@aadamhuda](https://github.com/aadamhuda)
- Added walls to the cargo level ~ [@sarahbrah7](https://github.com/sarahbrah7)
  - Using assets
- Added checkpoints to the cargo level ~ [@sarahbrah7](https://github.com/sarahbrah7)
- Added a 4th clue (in code and cargo level environment) ~ [@sarahbrah7](https://github.com/sarahbrah7)
- Added gates to the cargo level map ~ [@sarahbrah7](https://github.com/sarahbrah7)
- Cargo level map sky lighting is set as "stormy" using skybox asset ~ [@sarahbrah7](https://github.com/sarahbrah7)
- Added lights to walls throughout the cargo level map ~ [@sarahbrah7](https://github.com/sarahbrah7)
- Added sword equip and unequip with sword model ~ [@gv301](https://github.com/gv301)
- Added colliders to environment models ~ [@aadamhuda](https://github.com/aadamhuda)
### Deprecated 
- Reverted player movement to old input system ~ [@gv301](https://github.com/gv301)
### Fixed
- Fixed an issue where killing all enemies with the bow, will still cause battle to continue ~ [@Ibraheem717](https://github.com/Ibraheem717)

## [1.11.1](https://github.com/aadamhuda/CyberCity/commit/100816f745117ea34f7408243ff777666494f39a) - 2022-12-08
### Added
- Added animation for player to revert back to idle when revived ~ [@aadamhuda](https://github.com/aadamhuda)
- Added walls in checkpoints, so player cannot run out ~ [@Ibraheem717](https://github.com/Ibraheem717)
### Fixed
- Fixed bug with rotation on the start of player turn ~ [@aadamhuda](https://github.com/aadamhuda)
- Fixed an issue where using an ice attack on a frozen enemy will unfreeze them ~ [@aadamhuda](https://github.com/aadamhuda)

## [1.11.0](https://github.com/aadamhuda/CyberCity/commit/00f6f4381aca777e4f95f3133a05aed961a35f7b) - 2022-12-07
### Added
- Enemies can now use a set of moves ~ [@Ibraheem717](https://github.com/Ibraheem717)
  - Players and enemies can now be vulnerable or resistant to type of moves
### Changed
- Remodelled Damage ~ [@Ibraheem717](https://github.com/Ibraheem717)
  - Units have resistances and vulnerbilities
  - When attacking an enemy, the enemy checks the type of attack
  - If the enemy is resitant it'll resist 25% of damage and vice versa
  - Damage is stored in a dict

## [1.10.0](https://github.com/aadamhuda/CyberCity/commit/1eae4ed02bd005b454e818c0b0274b80deec1b82) - 2022-12-07
### Added
- Added shoot animation and updated animation speeds ~ [@gv301](https://github.com/gv301)
- Added heal animation ~ [@gv301](https://github.com/gv301)
- Added downed animation for players ~ [@gv301](https://github.com/gv301)
- Added death animation for enemies ~ [@gv301](https://github.com/gv301)
- Added feature: when health item is being used, downed players are unable to be selected ~ [@aadamhuda](https://github.com/aadamhuda)
  - When revive items are selected to be used, then players who aren’t downed are unable to be selected
  - When players are selecting an item, they are able to cancel the selected item by pressing the cancel button, this goes back to item select   
### Changed
- Reworked item menu to fit new HUD ~ [@aadamhuda](https://github.com/aadamhuda)
- Changed item usage: only one item can be used per round ~ [@aadamhuda](https://github.com/aadamhuda)

## [1.9.0](https://github.com/aadamhuda/CyberCity/commit/28767556b80e5c68575d944d9c1d533fdfe81834) - 2022-12-05
### Added
- Added new HUD elements ~ [@aadamhuda](https://github.com/aadamhuda)
  - New designed buttons
  - Abilities menu which now shows the individual abilities rather than using a change attack button
  - Added sprites which will hold player health, Magic points and an icon 
- Added a new tracking system which uses keys A and D to change enemy. Also shows visually the current chosen enemy ~ [@aadamhuda](https://github.com/aadamhuda)
- Added music for out of battle scene and in battle scene ~ [@aadamhuda](https://github.com/aadamhuda)
- Added main menu fonts ~ [@aadamhuda](https://github.com/aadamhuda)
- Added health bars to battle GUI ~ [@aadamhuda](https://github.com/aadamhuda)
### Changed
- Changed player and enemy damage values for balancing ~ [@aadamhuda](https://github.com/aadamhuda)
### Removed
- Removed old HUD elements ~ [@aadamhuda](https://github.com/aadamhuda)
  - Player and enemy names
  - Player and enemy HP
  - Battle Buttons

## [1.8.0](https://github.com/aadamhuda/CyberCity/commit/15b893e0a885bff8c1c2755d84752db42bc73737) - 2022-12-02
### Added
- Added battle inventory and using items ~ [@HinaMatsuzaki](https://github.com/HinaMatsuzaki)
- Added checkpoint areas ~ [@Ibraheem717](https://github.com/Ibraheem717)
  - Press 'm' to save location in checkpoint 
- Added burn and freeze attack animation ~ [@gv301](https://github.com/gv301)

## [1.7.0](https://github.com/aadamhuda/CyberCity/commit/76132d25a5f4c7dfeb1a5330558fb5ae3adcdd43) - 2022-11-30
### Added
- Added battle camera ~ [@aadamhuda](https://github.com/aadamhuda)
  - Camera will change to each players perspective on their turn
- Added downed state for player ~ [@aadamhuda](https://github.com/aadamhuda)
  - Players objects will not be removed in battle but rather theyll be put into a downed state, this prepares for the ability for players to be revived in battle using items
- Added inventory menu with button items and scrollbar to pause menu prefab ~ [@sarahbrah7](https://github.com/sarahbrah7)
- Inventory has fucntionality but sizing bug on the buttons ~ [@sarahbrah7](https://github.com/sarahbrah7)
### Changed
- Changed how enemies spawn ~ [@Ibraheem717](https://github.com/Ibraheem717)
  - They now instantiate
  - Depending on the difficulty
### Removed
- Removed functions to remove player from array - no longer needed, player now has 'downed variable' ~ [@aadamhuda](https://github.com/aadamhuda)
### Fixed
- Sizing bug fixed within inventory ~ [@sarahbrah7](https://github.com/sarahbrah7)

## [1.6.0](https://github.com/aadamhuda/CyberCity/commit/a4dc0149e005a62920db4adb20cbfda5bee6c044) - 2022-11-20
### Added
- Made a pause screen scene with buttons (save, load, settings, exit) ~ [@sarahbrah7](https://github.com/sarahbrah7)
- Player presses p in prototype and battle scenes and now access pause menu gameobject ~ [@sarahbrah7](https://github.com/sarahbrah7)
- Exit button in pause screen redirects user to main menu ~ [@sarahbrah7](https://github.com/sarahbrah7)
- Resume button added ~ [@sarahbrah7](https://github.com/sarahbrah7)
### Changed
- Pause screen no longer a scene but now a gameobject within the protoype and battle scenes ~ [@sarahbrah7](https://github.com/sarahbrah7)
- Resume button now resumes the game instead of load button resuming the game ~ [@sarahbrah7](https://github.com/sarahbrah7)
### Fixed
- Camera faces player direction ~ [@gv301](https://github.com/gv301)
- changed resume timescale from 0.1f to 1f (causing slowness on resume) ~ [@aadamhuda](https://github.com/aadamhuda)

## [1.5.0](https://github.com/aadamhuda/CyberCity/commit/44b9deb4ec70b1c9ac7a4d4f0a93caca750441c4) - 2022-11-21
### Added
- Added companion models with animations using player animation controller ~ [@aadamhuda](https://github.com/aadamhuda)
  - Companion 1, 2, 3
- Added attack type label ~ [@aadamhuda](https://github.com/aadamhuda)
- Player engages battle with health they ended with in last battle ~ [@Ibraheem717](https://github.com/Ibraheem717)
### Changed
- Enemies now have set paths ~ [@Ibraheem717](https://github.com/Ibraheem717)
- Cleaned up HUD elements ~ [@aadamhuda](https://github.com/aadamhuda)
### Removed
- Removed camera in enemy weapon model ~ [@aadamhuda](https://github.com/aadamhuda)
### Fixed
- PlayersHealth saves after each battle ~ [@Ibraheem717](https://github.com/Ibraheem717)
- Fixed restart battle (Game would crash as enemies targeted non-existing player) ~ [@Ibraheem717](https://github.com/Ibraheem717)

## [1.4.0](https://github.com/aadamhuda/CyberCity/commit/c0561ea44561819e207c32c4305ff312e985dc1e) - 2022-11-16
### Added
- Added jump animation ~ [@gv301](https://github.com/gv301)
- Player faces currently targeted enemy ~ [@gv301](https://github.com/gv301)
- Player moves towards enemy when attacking ~ [@gv301](https://github.com/gv301)
- Added out of battle enemy model ~ [@aadamhuda](https://github.com/aadamhuda)
- Added animations for enemy model ~ [@aadamhuda](https://github.com/aadamhuda)
  - Hit, walk, run
- Added in battle enemy models, no animations ~ [@HinaMatsuzaki](https://github.com/HinaMatsuzaki)
- Player runs backwards towards original position after attacking ~ [@gv301](https://github.com/gv301)
- Player now performs a melee attack animation for a normal attack ~ [@gv301](https://github.com/gv301)
- Added all types of attacks ~ [@HinaMatsuzaki](https://github.com/HinaMatsuzaki)
  - Player1: normal, shoot, curse (if cursed, do less damage)
  - Player2: normal, fire, burn
  - Player3: normal, grass, poison
  - Player4: normal, water, freeze
- added an attack indicator which shows the current attack type selected on screen ~ [@HinaMatsuzaki](https://github.com/HinaMatsuzaki)
### Changed
- Reworked battle animation controller and improved animation smoothness ~ [@gv301](https://github.com/gv301)
### Fixed
- Fixed frozen checks, enemy now applies damage to player when not frozen ~ [@aadamhuda](https://github.com/aadamhuda)

## [1.3.0](https://github.com/aadamhuda/CyberCity/commit/ced73e9c4bd1afc8010ad22e7da76c51dcb8cf20) - 2022-11-16
### Added
- Added multiple players in battle scene ~ [@Ibraheem717](https://github.com/Ibraheem717)
  - Player turn now rotates between each player
  - Before switching to enemies, where they will have all their turns
- Added three types of attacks ~ [@HinaMatsuzaki](https://github.com/HinaMatsuzaki)
  - Normal: the one we had in prototype
  - Burn
  - Freeze
- Added changeAttackButton: change attack type ~ [@HinaMatsuzaki](https://github.com/HinaMatsuzaki)
- Added new player model with animations for player movement ~ [@gv301](https://github.com/gv301)
### Changed
- Enemy now attack random player ~ [@Ibraheem717](https://github.com/Ibraheem717)
- Each player has a HUD, highlighted when using them ~ [@Ibraheem717](https://github.com/Ibraheem717)
- Player model changed ~ [@gv301](https://github.com/gv301)
- Player movements are now animated with idle, walking, and running animations ~ [@gv301](https://github.com/gv301)
- Added one more type of attack called shoot (multi-attack): smaller damage than other attacks, but all the enemies get damaged ~ [@HinaMatsuzaki](https://github.com/HinaMatsuzaki)
### Deprecated
- Enemy now attack random player ~ [@Ibraheem717](https://github.com/Ibraheem717)
### Fixed
- Fixed issue with players having 0 health on battle start ~ [@aadamhuda](https://github.com/aadamhuda)

## [1.2.2](https://github.com/aadamhuda/CyberCity/commit/b13895ee07957ebf073990e310515d487f648bb6) - 2022-11-08
### Changed
- Changed cursor lock mode to locked from confined ~ [@aadamhuda](https://github.com/aadamhuda) & [@gv301](https://github.com/gv301)

## [1.2.1](https://github.com/aadamhuda/CyberCity/commit/26d03ab771461bc81f7bfedc06f65c44d92a5dc2) - 2022-11-06
### Added
- Added confined cursor lock state ~ [@aadamhuda](https://github.com/aadamhuda)
### Changed
- Changle clue colour to yellow ~ [@aadamhuda](https://github.com/aadamhuda)
- Change in battle player model to match out of battle player model ~ [@aadamhuda](https://github.com/aadamhuda)
### Fixed
- Camera fixed, no longer goes through walls ~ [@gv301](https://github.com/gv301)
- Fixed restart after completing the game ~ [@gv301](https://github.com/gv301)

## [1.2.0](https://github.com/aadamhuda/CyberCity/commit/124b1789e10248e3d021cc1a096feb6cdf6366bc) - 2022-11-06
### Added
- Main menu scene made ~ [@sarahbrah7](https://github.com/sarahbrah7)
  - With a play game button which goes to the game scene (not battle scene)
  - With an exit game button which exits the game
- Added message when near clue, "press k to interact" ~ [@aadamhuda](https://github.com/aadamhuda)
- Added escape to battle ~ [@Ibraheem717](https://github.com/Ibraheem717)
  - Player can escape battle but will start at the beginning
- Added wait times for escape ~ [@aadamhuda](https://github.com/aadamhuda)
- Created restart button ~ [@aadamhuda](https://github.com/aadamhuda)
  - Restart button instantiates a prefab on player death only, to allow player to restart the battle ~ [@aadamhuda](https://github.com/aadamhuda)
### Changed
- Player presses 'x' and now goes to main menu instead of quitting ~ [@sarahbrah7](https://github.com/sarahbrah7)
- Changed escape key to escape button ~ [@aadamhuda](https://github.com/aadamhuda)
- Reordered the buttons in order of use ~ [@aadamhuda](https://github.com/aadamhuda)
- Reduced the multiplier when enemy attacks you out of battle to 15% extra rather than 33% ~ [@aadamhuda](https://github.com/aadamhuda)
### Fixed
- When accessing game from main menu, variables are all initialised to default/spawn values ~ [@aadamhuda](https://github.com/aadamhuda)
- Clues no longer respawn after battle ~ [@Ibraheem717](https://github.com/Ibraheem717)
- Fixed bug where players could escape after losing battle ~ [@aadamhuda](https://github.com/aadamhuda)
- Fixed clue counter issue, where clue counter would reset after every battle, stopping player from winning ~ [@aadamhuda](https://github.com/aadamhuda)
- Fixed bug where enemy will still attack if player has died ~ [@aadamhuda](https://github.com/aadamhuda)

## [1.1.0](https://github.com/aadamhuda/CyberCity/commit/9eeebcaaf2631df35f68091041883f32df00925e) - 2022-11-05
### Added
- Press 'k' alongside going to the clue to pick it up ~ [@sarahbrah7](https://github.com/sarahbrah7)
### Changed
- Three clues now needed to finish game ~ [@sarahbrah7](https://github.com/sarahbrah7)
- Changed heal value to random integer between 60 and 100 ~ [@aadamhuda](https://github.com/aadamhuda)
- Changed player damage to 35 ~ [@aadamhuda](https://github.com/aadamhuda)
- Changed enemy damage to 20 ~ [@aadamhuda](https://github.com/aadamhuda)
### Fixed
- Fixed health subtracting float values when enemy attacks you out of battle ~ [@aadamhuda](https://github.com/aadamhuda)

## [1.0.0](https://github.com/aadamhuda/CyberCity/commit/0430efa53e206c1d156a4e18d0ac4544077c4967) - 2022-11-03
### Added
- Created a win and start room ~ [@sarahbrah7](https://github.com/sarahbrah7)
- When a clue is picked the game freezes and the user is prompted to quit ~ [@sarahbrah7](https://github.com/sarahbrah7)
  - The game quits if user presses 'x'
- Made health bars over the player and enemies ~ [@HinaMatsuzaki](https://github.com/HinaMatsuzaki)
- Added free look camera using Cinemachine ~ [@gv301](https://github.com/gv301)
- Added prototype Scene - the main scene for main environment in prototype ~ [@aadamhuda](https://github.com/aadamhuda)
- Added Enumerator BattleStates which now control game flow using the following states ~ [@aadamhuda](https://github.com/aadamhuda)
  - START, PLAYERTURN, ENEMYTURN, WIN, LOSE
- Added heal functionality - allows player to heal by a certain amount of heal in battle ~ [@aadamhuda](https://github.com/aadamhuda)
- Added specific spawn points for player and enemy sprites ~ [@aadamhuda](https://github.com/aadamhuda)
- Added player and enemy prefabs ~ [@aadamhuda](https://github.com/aadamhuda)
- Added functionality for battle to spawn player and enemy based on prefabs ~ [@aadamhuda](https://github.com/aadamhuda)
- Added short intervals between turns and combat events ~ [@aadamhuda](https://github.com/aadamhuda)
- Added a feature which disables player abilities during an enemy turn ~ [@aadamhuda](https://github.com/aadamhuda)
- Added HUD ~ [@aadamhuda](https://github.com/aadamhuda)
- Added parent class BattleUnit ~ [@aadamhuda](https://github.com/aadamhuda)
- Added multiple enemies to the maze ~ [@Ibraheem717](https://github.com/Ibraheem717)
- Each enemy has their own Alive and Death state ~ [@Ibraheem717](https://github.com/Ibraheem717)
- Enemies roam the maze ~ [@Ibraheem717](https://github.com/Ibraheem717)
- Added run feature, player can now run while holding shift ~ [@gv301](https://github.com/gv301)
- Once enemies can see. If they detect players they chase then engage combat to them ~ [@Ibraheem717](https://github.com/Ibraheem717)
- Battles started by enemies causes extra damage to be taken ~ [@Ibraheem717](https://github.com/Ibraheem717)
### Changed
- Changed room into a maze ~ [@sarahbrah7](https://github.com/sarahbrah7)
- Updated player model to match collider (removed arms) ~ [@gv301](https://github.com/gv301)
- Changed TurnController to BattleSystem ~ [@aadamhuda](https://github.com/aadamhuda)
- Buttons in battle now have an onClick function in BattleSystem script ~ [@aadamhuda](https://github.com/aadamhuda)
- HUD elements now have a separate canvas to buttons ~ [@aadamhuda](https://github.com/aadamhuda)
- Changed PlayerAttack class to Player and EnemyAttack class to Enemy ~ [@aadamhuda](https://github.com/aadamhuda)
- Player and Enemy classes are children to BattleUnit ~ [@aadamhuda](https://github.com/aadamhuda)
### Deprecated
- Temporarily disabled exit button ~ [@aadamhuda](https://github.com/aadamhuda)
- Temporarily disabled restart button ~ [@aadamhuda](https://github.com/aadamhuda)
### Removed
- Removed Game loop ~ [@aadamhuda](https://github.com/aadamhuda)
- Removed health bars ~ [@aadamhuda](https://github.com/aadamhuda)
- Removed Guard functionality (due to feedback) ~ [@aadamhuda](https://github.com/aadamhuda)
- Removed Guard button script ~ [@aadamhuda](https://github.com/aadamhuda)
- Removed Attack Button script ~ [@aadamhuda](https://github.com/aadamhuda)
- Removed Change Target script ~ [@aadamhuda](https://github.com/aadamhuda)
### Fixed
- Using Unity's built in CharacterController instead of Rigidbody (no physics necessary) Made player movement smoother. also takes into account camera direction ~ [@gv301](https://github.com/gv301)
  - Player can now jump
- Player only resets position when they win ~ [@Ibraheem717](https://github.com/Ibraheem717)

## [0.0.0](https://github.com/aadamhuda/CyberCity/tree/f5995418d057d2d20d219b8d4cce686b3b783b9d) - 2022-10-25
### Added
- README file added to the repository
- CHANGELOG file added to the repository and records all updates to the game
- Finished basic player movement and camera ~ [@gv301](https://github.com/gv301)
- Created a change scene into and out of battle ~ [@Ibraheem717](https://github.com/Ibraheem717)
  - Return to 'out of battle' by pressing green button
- Game records last position before going to battle ~ [@Ibraheem717](https://github.com/Ibraheem717)
- Added ChangeTargetButton, AttackButton, GuardButton ~ [@HinaMatsuzaki](https://github.com/HinaMatsuzaki)
- Added enemy attacks ~ [@HinaMatsuzaki](https://github.com/HinaMatsuzaki)
- Added a pop-up to indicate attack ~ [@Ibraheem717](https://github.com/Ibraheem717)
- Added clues ~ [@gv301](https://github.com/gv301)
- Added walls to environment ~ [@sarahbrah7](https://github.com/sarahbrah7)
- Added health limit, so player health doesnt drop below 0 ~ [@aadamhuda](https://github.com/aadamhuda)
   - Player object disables after death 
- Added restart button ~ [@aadamhuda](https://github.com/aadamhuda)
   - Restart button is auto disabled in battle, and will enable on player death
- Added win screen if player picks up clue ~ [@sarahbrah7](https://github.com/sarahbrah7)
- Enemy sprites disable once they reach 0 health ~ [@aadamhuda](https://github.com/aadamhuda)
- Added health limit for enemies, they cannot drop below 0 health ~ [@aadamhuda](https://github.com/aadamhuda)
- Added scene change once all enemies are defeated in battle ~ [@aadamhuda](https://github.com/aadamhuda)
- Added death to enemy outside of battle ~ [@Ibraheem717](https://github.com/Ibraheem717)
### Changed
- Updated the player movement now so it works better, when you want to go down before you would reverse, now you move forward in whichever direction ~ [@gv301](https://github.com/gv301)
- Reduced enemy damage from 20 to 1 ~ [@aadamhuda](https://github.com/aadamhuda)
- Added walls onto main scene, removed from battle ~ [@Ibraheem717](https://github.com/Ibraheem717)
- Changed enemy damage to 15 from 1 ~ [@aadamhuda](https://github.com/aadamhuda)
- Changed player health from from 100 to 150 ~ [@aadamhuda](https://github.com/aadamhuda)
- Changed player damage from 20 to 50 ~ [@aadamhuda](https://github.com/aadamhuda)
### Deprecated
- Reverted back to intial player movement implementation with transform as opposed to rigidbody ~ [@gv301](https://github.com/gv301)
  - Due to the implementation of a free look camera that is controllable by holding right click on the mouse and dragging.
- Reverted to WASD movement system ~ [@gv301](https://github.com/gv301)
  - Free look disabled as a result 
### Fixed
- Connect the input actions to it so it works with keyboard and controller and other control schemes in the input manager ~ [@gv301](https://github.com/gv301)
