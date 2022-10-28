
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

## [0.0.0](https://github.com/aadamhuda/gameDev/tree/f5995418d057d2d20d219b8d4cce686b3b783b9d) - 2022-10-25
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
