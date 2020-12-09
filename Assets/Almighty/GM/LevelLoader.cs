using UnityEngine;

namespace BP.Almighty
{
    public class LevelLoader : MonoBehaviour
    {
        [SerializeField] private Arena_asset currentArenaAsset = null;
        [SerializeField] private Player_asset currentP1Asset = null;
        [SerializeField] private Player_asset currentP2Asset = null;

        private Player player1;
        private Player player2;
        private Arena arena;

        private void OnEnable()
        {
            RegisterObserver();
        }

        private void OnDisable()
        {
            UnregisterObserver();
        }
        
        private void GameStateSwitcher(GameStateName state)
        {
            if(state == GameStateName.Initialising)
            {
                Initialise();
            }
            if(state == GameStateName.Selection1)
            {
                //wait
            }
        }

        private void Initialise()
        {
            if (player1) { Destroy(player1.gameObject); }
            if (player2) { Destroy(player2.gameObject); }
            if (arena) { Destroy(arena.gameObject); }

            currentArenaAsset = null;
            currentP1Asset = null;
            currentP2Asset = null;
        }

        public void SelectPlayer(PlayerNumber newPlayerNumber, Player_asset newPlayerAsset)
        {
            Debug.Log("new player:" + newPlayerNumber + " (" + newPlayerAsset.GetPlayerName() + ")");
        }

        #region observer
        private void RegisterObserver()
        {
            GameStateClass.onGameStateChange += GameStateSwitcher;
            UIPlayerSelection.onPlayerSelected += SelectPlayer;
        }

        private void UnregisterObserver()
        {
            GameStateClass.onGameStateChange -= GameStateSwitcher;
            UIPlayerSelection.onPlayerSelected -= SelectPlayer;
        }
        #endregion
    }
}

