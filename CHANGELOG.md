
# Change Log  
All notable changes to this project will be documented in this file.
      
The format is based on [Keep a Changelog](http://keepachangelog.com/)
and this project adheres to [Semantic Versioning](http://semver.org/).

## [Unreleased]
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
### Changed
- Updated the player movement now so it works better, when you want to go down before you would reverse, now you move forward in whichever direction ~ [@gv301](https://github.com/gv301)
### Deprecated

### Removed

### Fixed
- Connect the input actions to it so it works with keyboard and controller and other control schemes in the input manager ~ [@gv301](https://github.com/gv301)
- Reverted back to intial player movement implementation with transform as opposed to rigidbody ~ [@gv301](https://github.com/gv301)
  - Due to the implementation of a free look camera that is controllable by holding right click on the mouse and dragging.
### Security

## [0.0.0] - 2022-11-10
