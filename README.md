# HabibVerse Scripts

A comprehensive C# scripting framework for the HabibVerse multiplayer game built with Unity and Netcode for GameObjects.

## Project Overview

HabibVerse is a multiplayer game featuring character customization, real-time chat, and multiple gameplay modes. This repository contains all game scripts organized into logical modules for maintainability and scalability.

## Project Structure

### Core Systems

- **CharacterCustomization/** - Player character creation and customization
  - `PlayerCustomizer.cs` - Main customization logic
  - `SlimeModel.cs` - Character model management
  - `CameraManager.cs` - Customization camera controls
  - `PlayerReadyUI.cs` - Ready state UI management

- **Gameplay/** - Core game mechanics and player controls
  - `PlayerController.cs` - Local player movement and input
  - `AnimatedPlayerController.cs` - Animation controller for players
  - `BulletPickup.cs` / `SizePickup.cs` - Collectible items
  - `HabibVerseFreeroamManager.cs` - Freeroam game mode
  - `HabibVerseTreasureHuntManager.cs` - Treasure hunt game mode
  - `PlayerHUD.cs` - In-game UI and HUD

- **Lobby/** - Pre-game lobby and server management
  - `AuthenticateUI.cs` - Authentication interface
  - `EditPlayerName.cs` - Player name configuration
  - `ClientNetworkTransform.cs` - Network player sync
  - `LoadingUI.cs` - Loading screens

- **Chats/** - In-game communication system
  - `LobbyChatBehaviour.cs` - Lobby chat functionality
  - `ChatMessage.cs` - Chat message handling
  - `words.json` - Profanity/content filter

- **Team/** - Team management and logic

- **TestingLobby/** - Testing and debug utilities

- **UI/** - General UI components and management

### Root Level Scripts

- `HabibVerse.cs` - Main game manager and initialization
- `SimpleCameraController.cs` - Basic camera control
- `TimerUI.cs` - Timer display and management

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
