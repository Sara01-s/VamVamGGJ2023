using UnityEngine;

namespace VamVamGGJ {

    internal sealed class InitGame : MonoBehaviour {

        [SerializeField] private AudioClip _firstWaveClip;

        private void Awake() {
            AudioController.Instance.PlayMusic(_firstWaveClip, false);
        }

    }
}
