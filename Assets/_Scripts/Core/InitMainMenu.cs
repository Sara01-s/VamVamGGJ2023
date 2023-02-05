using UnityEngine;

namespace VamVamGGJ {

    internal sealed class InitMainMenu : MonoBehaviour {

        [SerializeField] private AudioClip _mainMenuClip;

        private void Awake() {
            AudioController.Instance.PlayMusic(_mainMenuClip, true);
        }

        private void Update(){
            
        }

    }
}