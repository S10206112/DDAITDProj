# Debugs The Game

DeBugs is a virtual game that uses VR headsets to immerse the player in a digital world where you explore the bug world on a closer level, creating friendly relationships along the way.

This is a studio-based Assignment involving ITD & DDA.

### Group Members of the Project
- Kirdesh : Group Leader
- Jonathan : All Rounder 
- Dan : DDA Master
- Zavier : ITD COO
- Nyan Lin : Modeller/Level Creator
 
## Design Process
 
The game is targeted towards primary to secondary school students, with the goal of teaching them to appreciate bugs, as well as educating through pop ups with trivia.
## Features

### DDA Portion
For DDA, we are storing our player stats and achievement sections. In the achievement section firebase would save the achievements that the player has earned, like the collectibles which the player has gotten. The player stats would include data such as the player’s username, a bug relationship meter which concludes players relations with the bug in the game, a completion rate to see if players have completed the level. Lastly, there is a level timing which shows the best timing of the player in which they have completed the maze on that level.

[Database Structure]

1. Player Statistics
    - Ant Hill Level Completion
    - Queen Ant Relationship Meter
    - Best Timing for Ant Maze Trail
    - Time of update
2. User Profile
    - User ID
    - Online/Active
    - Account Creation Time
    - Last Logged in Time
    - Username
    - Email
3. Achievements 
    - (AchievementName)
      -> AchievementUnlocked
    
4. AchievementDetails (One Example)
    - Love At First Mite
      -> "Max Out Relationship Meter With the Queen Ant"
      -> "Love At First Mite"

### ITD Portion
For ITD, we have come up with continuous movement for our player to allow smoother flow of gameplay leading to a more immersive experience. 
Next, we have interactions. For example, grabbing objects and engaging in a dialogue with the bugs.

Another section would be the achievements, which involves completing mini objectives to earn achievements and also finding collectibles which the players can find in the game as they explore. Lastly, there is a UI Display section which includes clicking the menu button on the right controller which will trigger the pause menu and also when players interact with the bugs, a side display will open with the bugs info. 

1. Continuous Movement - Smoother flow of Gameplay leading to a more immersive experience.
2. Interactions - Grab objects. Engage in Dialogue with Bugs.
3. Achievements - Complete mini objectives to earn achievements. Find collectibles scattered around the levels. 
4. UI Display - Clicking the MenuButton on the right controller will trigger the pause menu. Interacting with bugs opens up a side display with bug info



### Features Left to Implement (TBC)
- Additional Levels as DLC Content (Spider Level)
- Leaderboard for the Ant Maze Timings



## Credits

### Sketch Fab Assets Used
https://sketchfab.com/3d-models/apple-bitten-mythical-beasts-jousting-assets-14288680d62a4fa5b8240c633012729e
https://sketchfab.com/3d-models/leaf-bug-31fb4afe3fb34d5a9481c0c47cdec4c9
https://sketchfab.com/3d-models/low-poly-ant-model-3635180b9b7d44dba5f7eb007693ded5

### VR Tutorial Playlist
https://youtube.com/playlist?list=PLrk7hDwk64-a_gf7mBBduQb3PEBYnG4fU

### Firebase References
https://firebase.google.com/docs
https://stackoverflow.com/


