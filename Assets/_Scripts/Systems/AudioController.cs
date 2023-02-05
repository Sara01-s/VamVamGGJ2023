using UnityEngine.Audio;
using UnityEngine;

namespace VamVamGGJ {
    
    internal sealed class AudioController : Singleton<AudioController> {

        [SerializeField] private AudioMixer _mixer;
        [SerializeField] private AudioSource _musicSource;
        [SerializeField] private AudioSource _sfxSource;

        private const float MIN_VOLUME_VALUE = 0.00001F;

        protected override void Awake() => base.Awake();

        // AUDIO FACADE //

        internal void PlaySFX(AudioClip clip) => _sfxSource.PlayOneShot(clip);
        
        internal void PlayMusic(AudioClip musicClip, bool loop) {
            _musicSource.clip = musicClip;
            _musicSource.loop = loop;
            _musicSource.Play();
        }

        // Master
        internal void ChangeMasterVolume(float value) {
            _mixer.SetFloat("MasterVolume", Mathf.Log10(value) * 20);
        }

        // Music
        internal void ChangeMusicVolume(float value) {
            if (value <= MIN_VOLUME_VALUE) 
                _musicSource.mute = true;
            else {
                _musicSource.mute = false;
                _mixer.SetFloat("MusicVolume", Mathf.Log10(value) * 20);
            }
        }

        // SFX
        internal void ChangeSFXVolume(float value) {
            if (value <= MIN_VOLUME_VALUE)
                _sfxSource.mute = true;
            else {
                _sfxSource.mute = false;
                _mixer.SetFloat("SFXVolume", Mathf.Log10(value) * 20);
            }
        }

        internal void PauseMusic() => _musicSource.Pause();
        internal void ResumeMusic() => _musicSource.Play();
        internal void ToggleMusic() => _musicSource.mute = !_musicSource.mute;
        internal void ToggleSFX() => _sfxSource.mute = !_sfxSource.mute;

    }
}