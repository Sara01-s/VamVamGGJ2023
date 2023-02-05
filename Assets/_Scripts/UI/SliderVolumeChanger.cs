using UnityEngine.UI;
using UnityEngine;

namespace VamVamGGJ {
    
    [RequireComponent(typeof(Slider))]
    internal class SliderVolumeChanger : MonoBehaviour {

        private Slider _thisSlider;

        private void Awake() {
            _thisSlider = GetComponent<Slider>();
            _thisSlider.value = _thisSlider.maxValue;
        }

        public void ChangeMasterVolume() {
            AudioController.Instance.ChangeMasterVolume(_thisSlider.value);
        }

        public void ChangeMusicVolume() {
            AudioController.Instance.ChangeMusicVolume(_thisSlider.value);
        }

        public void ChangeSFXVolume() {
            AudioController.Instance.ChangeSFXVolume(_thisSlider.value);
        }

    }
}