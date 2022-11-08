
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

## [1.2.2](https://github.com/aadamhuda/CyberCity/commit/b13895ee07957ebf073990e310515d487f648bb6) - 2022-11-08
### Changed
- Changed cursor lock mode to locked from confined ~ [@aadamhuda](https://github.com/aadamhuda) & [@gv301](https://github.com/gv301)
- 
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
