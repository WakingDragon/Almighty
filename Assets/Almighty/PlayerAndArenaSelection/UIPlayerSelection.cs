using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BP.Almighty
{
    public enum PlayerNumber { one, two }

    public class UIPlayerSelection : MonoBehaviour
    {
        [SerializeField] private PlayerList_asset playerLibrary = null;

        [Header("select player")]
        [SerializeField] private GameObject p1SelectButton = null;
        [SerializeField] private GameObject p2SelectButton = null;
        private PlayerNumber playerNumber = PlayerNumber.one;
        


        private Player_asset playerAsset;

        public delegate void OnPlayerSelected(PlayerNumber newPlayerNumber, Player_asset newPlayerAsset);
        public static event OnPlayerSelected onPlayerSelected;

        private void OnEnable()
        {
            //if (!p1SelectButton) { Debug.Log("no player 1 select button set"); }
            //if (!p2SelectButton) { Debug.Log("no player 2 select button set"); }
            RegisterObserver();
        }

        private void OnDisable()
        {
            UnregisterObserver();
        }

        public void SelectPlayer1()
        {
            playerNumber = PlayerNumber.one;
            Debug.Log("player 1 selected");
        }
        public void SelectPlayer2()
        {
            playerNumber = PlayerNumber.two;
            Debug.Log("player 2 selected");
        }

        public void SelectCharacter()
        {

        }

        private void GameStateSwitcher(GameStateName state)
        {
            if (state == GameStateName.Initialising)
            {
                ToggleButtons(false);
            }
            if (state == GameStateName.Selection1)
            {
                ToggleButtons(true);
            }
            if (state == GameStateName.Selection2)
            {
                ToggleButtons(false);
            }
        }

        private void ToggleButtons(bool isVisible)
        {
            p1SelectButton.SetActive(isVisible);
            p2SelectButton.SetActive(isVisible);
        }

        #region observer
        private void RegisterObserver()
        {
            GameStateClass.onGameStateChange += GameStateSwitcher;
        }

        private void UnregisterObserver()
        {
            GameStateClass.onGameStateChange -= GameStateSwitcher;
        }
        #endregion
    }
}


