using UnityEngine;

namespace BP.Almighty
{
    public class Player : MonoBehaviour
    {
        private Player_asset playerAsset;
        private bool isShooting = false;

        public delegate void OnHealthChanges(Player_asset playerAsset, float currentHealth, float startingHealth);
        public static event OnHealthChanges onHealthChanges;

        public void SetPlayerAsset(Player_asset newAsset) { playerAsset = newAsset; }

        public void TryClickAction(Vector3 pos)
        {
            if(isShooting)
            {

            }
            else
            {
                playerAsset.SpawnMinion(pos);
            }
        }

        public void DoDmg(float dmg)
        {
            playerAsset.AdjustHealth(-dmg);
            onHealthChanges?.Invoke(playerAsset,playerAsset.GetCurrentHealth(),playerAsset.GetStartHealth());
            if (playerAsset.CheckIfDead())
            {
                PlayerDeath();
            }
        }

        private void PlayerDeath()
        {
            Debug.Log("GAME OVER! Player dead");
        }
    }
}

