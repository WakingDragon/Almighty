using System;
using UnityEngine;
using UnityEditorInternal;

namespace BP.Almighty
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private Player_asset player1Asset = null;
        [SerializeField] private Player_asset player2Asset = null;
        [SerializeField] private Arena_asset arenaAsset = null;
        [SerializeField] private GameObject targetCursorPrefab = null;

        private GameObject player1, player2;
        private Player player;
        private Arena arena;
        private GameStateClass stateMachine;

        [HideInInspector] public static GameManager instance;

        private void Awake()
        {
            instance = this;
        }

        private void OnEnable()
        {
            RegisterObserver();
        }

        private void Start()
        {
            stateMachine = FindObjectOfType<GameStateClass>();
            if(!stateMachine) { Debug.LogWarning("no gamestate found by GM"); }
            //new...
            //set state machine
            stateMachine.TryChangeGameState(GameStateName.Initialising);

            //kick off menu
            stateMachine.TryChangeGameState(GameStateName.Selection1);

            //old...
            SetupLevel();
        }

        private void Update()
        {
            if(Input.GetKeyDown(KeyCode.M))
            {
                Debug.Log("M pressed");
            }
        }

        private void OnDisable()
        {
            UnregisterObserver();
        }

        #region observer
        private void RegisterObserver()
        {

        }

        private void UnregisterObserver()
        {

        }
        #endregion

        private void SetupLevel()
        {
            arena = arenaAsset.SetupArena(Vector3.zero);
            player1 = player1Asset.SetupPlayer(new Vector3(-10f, 0f, 0f), true);
            player2 = player2Asset.SetupPlayer(new Vector3(10f, 0f, 0f), false);

            player = player1.GetComponent<Player>();
            SetupCursor();
        }

        public Player_asset GetPlayer1Asset()
        {
            return player1Asset;
        }
        public Player_asset GetPlayer2Asset()
        {
            return player2Asset;
        }
        public GameObject GetPlayer2()
        {
            return player2;
        }

        public Player GetPlayer1PlayerComponent()
        {
            return player;
        }

        public Player GetEnemyPlayerComponentOf(Player_asset asset)
        {
            if (asset == player1Asset) { return player2.GetComponent<Player>(); }
            if (asset == player2Asset) { return player1.GetComponent<Player>(); }
            return null;
        }

        private void SetupCursor()
        {
            var targetCursor = Instantiate(targetCursorPrefab);
            targetCursor.transform.position = Vector3.zero;
        }
    }
}

