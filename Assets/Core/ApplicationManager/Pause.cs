using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BP.Core
{
    public class Pause : MonoBehaviour
    {
        //two methods for controlling pause state: observer pattern or boolvar
        [SerializeField] private bool pauseTimeAsWell = false;  //NB: pauses time.time as well as action. Only dev option
        private bool isPaused = false;
        [SerializeField] private BoolVariable isPausedBoolVar = null;

        public delegate void OnPause(bool isPaused);
        public static event OnPause onPause;

        private void Start()
        {
            if (!isPausedBoolVar) { Debug.Log("no isPaused boolvar set!"); return; }
            isPausedBoolVar.Value = false;
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.P) && !SceneLoader.instance.IsMenuSceneActive())
            {
                TogglePause();
            }
        }

        public void TogglePause()
        {
            isPaused = !isPaused;
            DoPausing(isPaused);
        }

        public void TogglePause(bool newIsPaused)
        {
            isPaused = newIsPaused;
            DoPausing(isPaused);
        }

        private void DoPausing(bool newIsPaused)
        {
            isPausedBoolVar.Value = isPaused;
            onPause?.Invoke(isPaused);

            if (pauseTimeAsWell)
            {
                if (newIsPaused == true)
                {
                    Time.timeScale = 0;
                }
                else
                {
                    Time.timeScale = 1;
                }
            }
        }
    }
}

