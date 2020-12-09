A game state manager for a single scene
Game functions and components only need to subscribe to the different states
The states can be changed by certain transition method calls

NOTE: Because this is for a single scene game, it is operating within the CORE+menu framework so it already has main menu function

> Just pop it into the scene
> reference it from any script that changes game state
> need to register to observe onGameStateChange(GameState enum)