## Project Overview
This target shooting game was developed as a task for my club to learn Unity game development. As this was my first game development project, I focused on implementing core mechanics while learning Unity's workflow.

## Key Features
- 🎯 Multiple target boards with scoring system
- ⏱️ Timer-based rounds (45 seconds per level)
- 📈 Progressive difficulty (targets move faster each level)
- 🏆 Score multiplier based on current level
- 🔄 Automatic round restart when all targets are hit

## Known Issues
1. **UI Visibility in Build**:
   - The level counter and timer text (visible in Unity Editor) don't appear in the built game
   - *Debugging in progress* - suspecting Canvas render mode or text mesh pro issues
   - Works correctly in Editor play mode
2. **Pause Menu**:
   - Implemented but not fully functional in build
   

## How to Experience the Project
### For Players:
1. Navigate to `Final Game/Build/` folder
2. Run the executable file
3. Controls:
   - Mouse: Aim and shoot
   - ESC: Attempt to pause (currently non-functional in build)

### For Developers:
1. Open project in **Unity Hub** (2022.3.11f1 recommended)
2. Explore:
   - `Assets/Scripts/` for game logic
   - `Assets/Scenes/` for level setup
   - `Assets/Prefabs/` for target objects

## Development Notes
### Learning Journey:
- This being my first Unity project, I:
  - Learned Unity's component system
  - Understood physics interactions (Rigidbodies, Colliders)
  - Implemented game state management
- Used **DeepSeek AI** for:
  - Initial script templates
  - Troubleshooting Unity-specific issues
  - Best practice recommendations

### Original Work:
While I referenced AI-generated code for:
- Basic movement scripts
- UI setup patterns
- Git configuration

All game design, implementation decisions, and debugging efforts are my original work.

## Future Improvements(Currently in trial)
- Fix UI visibility in builds
- Implement proper pause functionality
- Add sound effects and visual feedback
- Create more varied target patterns


