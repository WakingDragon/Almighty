using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BP.Almighty
{
    public enum GameStateName { None, Initialising, Selection1, Selection2, Ready, Playing, Paused, Unloading, Unloaded }

    public class GameStateClass : MonoBehaviour
    {
        private GameStateName gameState = GameStateName.None;

        public delegate void OnGameStateChange(GameStateName newGameState);
        public static event OnGameStateChange onGameStateChange;

        ///TODO consider switching this to a more intelligent state machine
        ///state names given in GameStateName enum
        ///for each state trigger OnEnter####State and OnExit#####State
        ///when state is entered, trigger OnEnter####State()
        ///this should include storing the state
        ///when new state is entered, trigger OnExit for the old state (if it exists)
        ///TryChangeGameState() should take statename and use this to select the relevant entry and exit methods - this removes the switching needed in other classes.

        public void TryChangeGameState(GameStateName newGameState)
        {
            if (newGameState != gameState)
            {
                gameState = newGameState;
                onGameStateChange?.Invoke(gameState);
                Debug.Log("new game state:" + gameState);
            }
        }
    }
}

