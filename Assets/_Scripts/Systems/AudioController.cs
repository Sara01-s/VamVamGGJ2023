using UnityEngine.Audio;
using UnityEngine;

namespace VamVamGGJ {
    
    [RequireComponent(typeof(AudioSource))]
    public sealed class AudioController : Singleton<AudioController> {

        [SerializeField] private AudioMixer _mixer;
        [SerializeField] private AudioSource _musicSource;
        [SerializeField] private AudioSource _sfxSource;

        private const float MIN_VOLUME_VALUE = 0.00001F;

        protected override void Awake() => base.Awake();


        // AUDIO FACADE //

        public void PlaySFX(AudioClip clip) => _sfxSource.PlayOneShot(clip);
        
        public void PlayMusic(AudioClip musicClip, bool loop) {
            _musicSource.loop = loop;
            _musicSource.PlayOneShot(musicClip);
        }

        // Master
        public void ChangeMasterVolume(float value) {
            _mixer.SetFloat("MasterVolume", Mathf.Log10(value) * 20);
        }

        // Music
        public void ChangeMusicVolume(float value) {
            if (value <= MIN_VOLUME_VALUE) 
                _musicSource.mute = true;
            else {
                _musicSource.mute = false;
                _mixer.SetFloat("MusicVolume", Mathf.Log10(value) * 20);           
            }
        }

        // SFX
        public void ChangeSFXVolume(float value) {
            if (value <= MIN_VOLUME_VALUE)
                _sfxSource.mute = true;
            else {
                _sfxSource.mute = false;
                _mixer.SetFloat("SFXVolume", Mathf.Log10(value) * 20);            
            }
        }

        public void ToggleMusic() => _musicSource.mute = !_musicSource.mute;
        public void ToggleSFX() => _sfxSource.mute = !_sfxSource.mute;

    }
}