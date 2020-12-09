using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BP.Core
{
    public class LaunchOptionsMenu : MonoBehaviour
    {
        Pause pause;

        private void Start()
        {
            pause = GetComponent<Pause>();
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                ToggleMenu();
            }
        }

        private void ToggleMenu()
        {
            if (SceneLoader.instance.IsMenuSceneActive() && SceneLoader.instance.IsThereAnotherLiveScene())
            {
                SceneLoader.instance.CloseOptionsMenu();
                pause.TogglePause(false);
            }
            else
            {
                pause.TogglePause(true);
                SceneLoader.instance.LoadOptionsMenu();
            }
        }
    }
}


