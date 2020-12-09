using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BP.Almighty
{
    public class UIWeaponButtons : MonoBehaviour
    {
        [SerializeField] private GameObject controlButtonsGroup = null;

        private void OnEnable()
        {
            RegisterObserver();
        }

        private void OnDisable()
        {
            UnregisterObserver();
        }


        public void MinionButtonSelected()
        {
            Debug.Log("minions selected");
        }

        public void FireButtonSelected()
        {
            Debug.Log("fire selected");
        }

        private void ToggleVisibility(bool makeVisible)
        {
            if(makeVisible)
            {
                controlButtonsGroup.GetComponent<CanvasGroup>().alpha = 1f;
                controlButtonsGroup.GetComponent<CanvasGroup>().interactable = true;
                controlButtonsGroup.GetComponent<CanvasGroup>().blocksRaycasts = true;
            }
            else
            {
                controlButtonsGroup.GetComponent<CanvasGroup>().alpha = 0f;
                controlButtonsGroup.GetComponent<CanvasGroup>().interactable = false;
                controlButtonsGroup.GetComponent<CanvasGroup>().blocksRaycasts = false;
            }
        }

        private void Initialise(GameStateName state)
        {
            ToggleVisibility(false);
        }

        private void RegisterObserver()
        {
            GameStateClass.onGameStateChange += Initialise;
        }

        private void UnregisterObserver()
        {
            GameStateClass.onGameStateChange -= Initialise;
        }
    }
}

