using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BP.Core
{
    public class OptionsMenuButtons : MonoBehaviour
    {
        [SerializeField] private GameObject quitBtn = null;

        private bool showQuitButton = false;

        private void OnEnable()
        {
            ToggleQuitButtonVisibility();
        }

        private void ToggleQuitButtonVisibility()
        {
            showQuitButton = SceneLoader.instance.ShowQuitButton();
            quitBtn.SetActive(showQuitButton);
        }

        private void ToggleQuitButtonVisibility(bool showButton)
        {
            showQuitButton = showButton;
            quitBtn.SetActive(showQuitButton);
        }

        public void ClickSandboxButton()
        {
            SceneLoader.instance.LoadSandboxScene();
        }

        public void ClickPlayButton()
        {
            SceneLoader.instance.LoadGameScene();
        }

        public void ClickQuitButton()
        {
            SceneLoader.instance.QuitGameplay();
            ToggleQuitButtonVisibility(false);
        }
    }
}

