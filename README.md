# HabibVerse Scripts

A comprehensive C# scripting framework for the HabibVerse multiplayer game built with Unity and Netcode for GameObjects.

## Project Overview

HabibVerse is a multiplayer game featuring character customization, real-time chat, and multiple gameplay modes. This repository contains all game scripts organized into logical modules for maintainability and scalability.

## Project Structure

### Root Level Scripts (Core Entry Points)

- **`HabibVerse.cs`** - Main game manager and system initialization. Handles overall game state, scene management, and core game flow orchestration.
- **`SimpleCameraController.cs`** - Basic camera controller for main menu and non-gameplay scenes. Provides smooth camera movement and rotation.
- **`TimerUI.cs`** - Displays and manages in-game timers for game modes, countdowns, and match duration.

### CharacterCustomization/ - Player Avatar Creation System

- **`PlayerCustomizer.cs`** ⭐ - Main customization hub managing the entire character creation flow including appearance, accessories, and customization data persistence.
- **`SlimeModel.cs`** - Handles the 3D slime character model visualization and material updates based on customization choices.
- **`SlimeParticle.cs`** - Manages particle effects for the slime character.
- **`SlimeFace.cs`** - Manages facial expressions and features for the slime character.
- **`CameraManager.cs`** - Controls the camera during character customization allowing players to rotate and view their character.
- **`PlayerReadyUI.cs`** - Manages the ready state UI allowing players to confirm they're prepared to start the game.
- **`ReadySelect.cs`** - Handles player ready/not-ready selection logic and state synchronization.
- **`CustomizationData.cs`** - Data structure storing player customization preferences (colors, hats, skins).
- **`PlayerCard.cs`** - Displays player information card during lobby phase.
- **`HatSpawn.cs`** - Manages hat and cosmetic item spawning on the player model.
- **`CharacterSelectionState.cs`** - Manages the character selection UI state machine.
- **`ServerUI.cs`** - Server-side UI management during customization phase.
- **`DisconnectionMessage.cs`** - Displays disconnection messages and handles reconnection flow.

### Gameplay/ - Core Game Mechanics & Player Control

- **`PlayerController.cs`** ⭐ - Local player input handling and movement logic. Controls player input processing, movement direction, and basic physics.
- **`AnimatedPlayerController.cs`** ⭐ - Synchronizes animations with player movement states and networked animation playback across all players.
- **`PlayerManager.cs`** ⭐ - Manages individual player instance including health, state, and gameplay properties. Handles player damage, death, and respawn logic.
- **`LocalPlayerGameManager.cs`** - Manages game state and mechanics for the local player instance in gameplay.
- **`PlayerSpawner.cs`** - Handles player instantiation and respawning at designated spawn points.
- **`PlayerHUD.cs`** - In-game heads-up display showing player health, ammo, score, and other vital information.
- **`PlayerUI.cs`** - General player UI management during gameplay.
- **`HabibVerseFreeroamManager.cs`** - Game mode manager for free-roaming exploration without objectives.
- **`HabibVerseTreasureHuntManager.cs`** - Game mode manager for treasure hunt competitive gameplay with collectibles and scoring.
- **`BulletPickup.cs`** - Collectible item that replenishes player ammunition when picked up.
- **`SizePickup.cs`** - Collectible item that temporarily or permanently alters player size/scale.
- **`Treasure.cs`** - Treasure collectible in treasure hunt mode with scoring value.
- **`Bullet.cs`** - Projectile behavior, collision detection, and damage application.
- **`BulletTrailScriptableObject.cs`** - ScriptableObject configuration for bullet trail visual effects.
- **`PauseUI.cs`** - Pause menu interface with resume, settings, and quit options.
- **`SlideTrigger.cs`** - Trigger zone that applies sliding effect to players who enter.
- **`TrampolineTrigger.cs`** - Trigger zone that bounces/launches players upward for platform navigation.

### Lobby/ - Pre-Game Server & Authentication

- **`AuthenticateUI.cs`** ⭐ - Main authentication UI handling login/signup flows and credential verification.
- **`ClientNetworkTransform.cs`** - Network synchronization component for player position/rotation during lobby.
- **`EditPlayerName.cs`** - UI for editing and saving the player's display name.
- **`LoadingUI.cs`** - Loading screen UI with progress indicators and tips.

### Chats/ - In-Game Communication System

- **`LobbyChatBehaviour.cs`** ⭐ - Main chat system manager handling message sending, receiving, and display.
- **`ChatMessage.cs`** - Individual chat message data structure and display component.
- **`words.json`** - Profanity filter word list and content moderation rules.

### Team/ - Backend Services & Authentication

- **`GameManager.cs`** ⭐ - High-level game orchestration and multiplayer session management.
- **`Sign_UP.cs`** - User registration endpoint and account creation logic.
- **`Sign_IN.cs`** - User authentication and login validation logic.
- **`SessionID.cs`** - Session token management and player session tracking.

### UI/ - General User Interface Components

- **`MUIManager.cs`** - Main UI manager coordinating all UI panels and screens.
- **`CharacterSelectionUI.cs`** - Character selection interface allowing player to choose their avatar.

### TestingLobby/ - Development & Debugging

- **`TestingLobbyUI.cs`** - Testing utility UI for development and debugging gameplay scenarios.

## Requirements

- Unity 2022.3 LTS or later
- Netcode for GameObjects (Multiplayer)
- TextMesh Pro (for UI)
- Photon or similar networking solution

## Key Features

- **Character Customization** - Full player character creation with hats and skins
- **Multiplayer Support** - Real-time synchronization across players
- **Multiple Gamemodes** - Freeroam and Treasure Hunt modes
- **Chat System** - In-game messaging with content filtering
- **UI Framework** - Comprehensive menu and HUD systems
- **Animation System** - Smooth character animation and control

## Getting Started

1. Open the project in Unity
2. Configure networking settings in Lobby scripts
3. Ensure all scenes are properly referenced in the build settings
4. Run from the main menu scene

## Development

### Adding New Features

1. Create appropriately named scripts in the relevant folder
2. Follow existing naming conventions and code style
3. Ensure network synchronization for multiplayer features
4. Update this README with new major systems

### Code Organization

- Group related functionality into folder modules
- Use clear, descriptive class and method names
- Implement proper network sync for multiplayer interactions
- Include XML documentation comments for public methods

## License

[Add your license information here]

## Contributors

[Add contributor information here]
