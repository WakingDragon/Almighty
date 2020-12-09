using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BP.Core
{
    public class OptionsMenuSFX : MonoBehaviour
    {
        [SerializeField] private AudioCue btnOverSFX = null;
        [SerializeField] private AudioCue menuSoundtrack = null;

        private void OnEnable()
        {
            GameAudioManager.instance.PlayAudio(menuSoundtrack, AudioPlayType.PlayLooped, 0);
        }

        private void OnDisable()
        {
            GameAudioManager.instance.StopAllSoundsOnTrack(AudioTrack.Soundtrack);
        }

        public void PlayButtonSFX()
        {
            GameAudioManager.instance.PlayAudio(btnOverSFX, AudioPlayType.PlayOneShot, 0);
        }

    }
}

