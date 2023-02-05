using UnityEngine;

namespace VamVamGGJ {

    internal sealed class InitGame : MonoBehaviour {

        [SerializeField] private AudioClip _firstWaveClip;

        public void GoToMainMenu() {
            LevelLoader.Instance.LoadScene(0, GameState.OnMainMenu);
        }

        public void MuteAllSounds() {
            AudioController.Instance.ToggleMusic();
        }

        public void PlayFirstWaveMusic() {
            AudioController.Instance.ToggleMusic();
            AudioController.Instance.PlayMusic(_firstWaveClip, false);
        }

    }
}
