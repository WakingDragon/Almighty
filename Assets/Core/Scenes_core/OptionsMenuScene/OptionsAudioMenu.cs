using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace BP.Core
{
    public class OptionsAudioMenu : MonoBehaviour
    {
        [Header("UI elements")]
        [SerializeField] private Slider masterVolSlider = null;
        [SerializeField] private Slider soundtrackVolSlider = null;

        [Header("float vars")]
        [SerializeField] private FloatVariable masterVolVar = null;
        [SerializeField] private FloatVariable soundtrackVolVar = null;

        private void OnEnable()
        {
            SetListenersForSliders();
            SetSlidersToStoredValues();
        }

        public void SetMasterVolume(float newVol)
        {
            GameAudioManager.instance.SetMasterVolumeLevel(newVol);
        }

        public void SetSoundtrackVolume(float newVol)
        {
            GameAudioManager.instance.SetSoundtrackVolumeLevel(newVol);
        }

        private void SetListenersForSliders()
        {
            masterVolSlider.onValueChanged.AddListener(delegate { SetMasterVolume(masterVolSlider.value); });
            soundtrackVolSlider.onValueChanged.AddListener(delegate { SetSoundtrackVolume(soundtrackVolSlider.value); });
        }

        private void SetSlidersToStoredValues()
        {
            masterVolSlider.value = masterVolVar.Value;
            soundtrackVolSlider.value = soundtrackVolVar.Value;
        }
    }
}

