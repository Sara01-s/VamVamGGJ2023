using UnityEngine.Audio;
using UnityEngine;

namespace VamVamGGJ {
    
    [RequireComponent(typeof(AudioSource))]
    public sealed class AudioController : Singleton<AudioController> {

        [SerializeField] private AudioMixer _mixer;
        public static float[] _spectrumSamples = new float[512];
        public static float[] _frequencyBand = new float[8];

        public AudioSource MusicSource { get; private set; }

        private AudioSource _sfxSource;

        protected override void Awake() {
            base.Awake();

            MusicSource = GetComponentInChildren<AudioSource>();
            _sfxSource = GetComponentInChildren<AudioSource>();
        }


        // AUDIO FACADE //

        public void PlaySFX(AudioClip clip) => _sfxSource.PlayOneShot(clip);
        
        public void PlayMusic(AudioClip musicClip, bool loop) {
            MusicSource.loop = loop;
            MusicSource.PlayOneShot(musicClip);
        }

        // Master
        public void ChangeMasterVolume(float value) {
            _mixer.SetFloat("MasterVolume", Mathf.Log10(value) * 20);
        }

        // Music
        public void ChangeMusicVolume(float value) {
            if (value <= 0.00001) 
                MusicSource.mute = true;
            else {
                MusicSource.mute = false;
                _mixer.SetFloat("MusicVolume", Mathf.Log10(value) * 20);           
            }
        }

        // SFX
        public void ChangeSFXVolume(float value) {
            if (value <= 0.00001)
                _sfxSource.mute = true;
            else {
                _sfxSource.mute = false;
                _mixer.SetFloat("SFXVolume", Mathf.Log10(value) * 20);            
            }
        }

        public void ToggleMusic() => MusicSource.mute = !MusicSource.mute;
        public void ToggleSFX() => _sfxSource.mute = !_sfxSource.mute;

    }
}