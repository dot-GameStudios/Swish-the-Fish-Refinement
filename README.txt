# Swish-the-Fish
Swish the Fish Prototype

HOW TO PLAY: Click and Drag the shark around the screen and eat the opposite emotion the shark has. If you eat 5 you lose. 

Here is a document on how this prototype works and the scripts behind the functionality and the design choices of the prototype. 

The Main Scene:

The game has 3 managers: THe Emotion Manager, Fish Manager, and Game Manager. All the game objects have the according manager script connected to it
  GameManager.cs:     Handles the game functionality which are starting the game, tracking scores, end the game, assign the area boundaries 
  EmotionManager.cs:  This holds all the available Emotions within the game
  FishManager.cs:     Spawns and destroys the fishes

Main Camera has:  
  CameraFollow.cs:     Follows the target and clamps the target within the screenspace

The Canvas has the GameUI script which maintains all UI elements on screen which are: Buttons and Text.
  - Under the Canvas there are 3 Game objects that represent each game screen
  - All button elements use onClick events in Inspector
  GameUI.cs:          Maintains each UI Screen and handles the In-Game UI elements (The score and Red Xs) 
 
The Player Shark object is what the player is going to control. child objects are the shark mesh, emotion text, and emotion sprite.
  PlayerMovement.cs:    Gets the mouse input and handles the movement of the character
  Shark.cs:             Handles the Sharks properties for the game
  
  
Other scripts

  Fish.cs:              Handles all the fish properties and its behaviour within the game
  
  Emotion.cs:           Holds the main emotion type, list of opposite emotions (Incase the emotion matches with more than one), and the sprite associated with the emotion
  The emotions are scriptable objects because I believe it makes it mroe dynamic for the designers/developer to easily implement as many emotions as they can 
  
  
  
