using UnityEngine;

namespace BP.Almighty
{
    [CreateAssetMenu(fileName ="Player_asset",menuName ="Almighty!/New Player")]
    public class Player_asset : ScriptableObject
    {
        [SerializeField] private string playerName = "New Player";
        [SerializeField] private GameObject playerPrefab = null;
        [SerializeField] private GameObject minionPrefab = null;

        [SerializeField] private float startingHealth = 1000f;
        private float currentHealth;

        public string GetPlayerName() { return playerName; }
        public GameObject SetupPlayer(Vector3 pos, bool isPlayer)
        {
            var playerGO = Instantiate(playerPrefab);
            playerGO.transform.position = pos;
            var player = playerGO.GetComponent<Player>();
            if (!player) { player = playerGO.AddComponent<Player>(); }
            player.SetPlayerAsset(this);

            currentHealth = startingHealth;

            if(!isPlayer)
            {
                playerGO.AddComponent<AIPlayer>();
            }

            return playerGO;
        }

        #region minions
        public GameObject GetMinion() { return minionPrefab; }

        public void SpawnMinion(Vector3 pos)
        {
            var minion = Instantiate(minionPrefab);
            minion.transform.position = pos;
            minion.GetComponent<Minion>().SetEnemy(GameManager.instance.GetEnemyPlayerComponentOf(this));
        }
        #endregion

        #region health
        public float GetCurrentHealth() { return currentHealth; }
        public float GetStartHealth() { return startingHealth; }

        public void AdjustHealth(float healthChange)
        {
            currentHealth += healthChange;
        }

        public bool CheckIfDead()
        {
            if(currentHealth <= 0f) { return true; }
            return false;
        }
        #endregion
    }
}

