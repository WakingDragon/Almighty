using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace BP.Almighty
{
    public class HealthBars : MonoBehaviour
    {
        [SerializeField] private Slider p1HealthBar = null;
        [SerializeField] private Slider p2HealthBar = null;
        private Player_asset player1Asset;
        private Player_asset player2Asset;

        private void OnEnable()
        {
            RegisterObserver();
        }

        private void OnDisable()
        {
            UnregisterObserver();
        }

        private void Start()
        {
            player1Asset = GameManager.instance.GetPlayer1Asset();
            player2Asset = GameManager.instance.GetPlayer2Asset();
        }

        private void ChangeHealthBar(Player_asset playerAsset, float currentHealth, float startingHealth)
        {
            var x = Utils.ClampedPercent(currentHealth, startingHealth);

            if (playerAsset == player1Asset) { p1HealthBar.value = x; }
            if (playerAsset == player2Asset) { p2HealthBar.value = x; }
        }

        private void ToggleVisibility(bool makeVisible)
        {
            p1HealthBar.gameObject.SetActive(makeVisible);
            p2HealthBar.gameObject.SetActive(makeVisible);
        }

        private void Initialise(GameStateName state)
        {
            ToggleVisibility(false);
        }

        private void RegisterObserver()
        {
            GameStateClass.onGameStateChange += Initialise;
            Player.onHealthChanges += ChangeHealthBar;
        }

        private void UnregisterObserver()
        {
            GameStateClass.onGameStateChange -= Initialise;
            Player.onHealthChanges -= ChangeHealthBar;
        }
    }
}

