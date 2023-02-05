using UnityEngine;

namespace VamVamGGJ {

    internal sealed class InitGame : MonoBehaviour {

        [SerializeField] private AudioClip _firstWaveClip;

        public void PlayFirstWaveMusic() {
            AudioController.Instance.PlayMusic(_firstWaveClip, false);
        }

    }
}
