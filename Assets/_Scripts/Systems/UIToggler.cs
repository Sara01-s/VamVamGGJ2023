using UnityEngine.UI;
using UnityEngine;
using System;
using TMPro;

namespace VamVamGGJ {

    public sealed class UIToggler : Singleton<UIToggler> {

        [SerializeField] private GameObject _mainMenu, _levelSelection, _pauseMenu, _failMenu, _scoreMenu;
        [SerializeField] private Slider _masterSlider, _musicSlider, _sfxSlider;

        private GameState _selectedStateInInspector;
        private bool isPaused = false;

        [SerializeField] private TMP_Text _bindingZDisplayText;
        [SerializeField] private GameObject _startRebindingZButton;
        [SerializeField] private GameObject _waitingForInputZButton;

        [SerializeField] private TMP_Text _bindingsXDisplayText;
        [SerializeField] private GameObject _startRebindingXButton;
        [SerializeField] private GameObject _waitingForInputXButton;

        private int _bindingNumber;

        private float _masterVolume;
        private float _musicVolume;
        private float _sfxVolume;

        protected override void Awake() 
        {
            base.Awake();


            _masterSlider.value = PlayerPrefs.GetFloat("MasterVolume");
            _musicSlider.value = PlayerPrefs.GetFloat("MusicVolume");
            _sfxSlider.value = PlayerPrefs.GetFloat("SFXVolume");

        }


        public void OnApplicationQuit() => Application.Quit();
        public void InvokeTogglePause() => EventDispatcher.OnTogglePause?.Invoke();


        private void OnEnable() {
           EventDispatcher.OnTogglePause += TogglePause;
           EventDispatcher.OnGameStateChanged += UpdateUI;
        }
            
        private void OnDisable() {
           EventDispatcher.OnTogglePause -= TogglePause;
           EventDispatcher.OnGameStateChanged -= UpdateUI;
        }

        public void ReloadScene() {
           EventDispatcher.OnTogglePause?.Invoke();
            LevelLoader.Instance.ReloadLevel();
        }


        public void SetNextState(string nextState) {
            _selectedStateInInspector = (GameState)Enum.Parse(typeof(GameState), nextState);
        }

        public void LoadScene(int index) {
            if (isPaused)EventDispatcher.OnTogglePause?.Invoke();
            LevelLoader.Instance.LoadScene(index, _selectedStateInInspector);
        }

        public void UpdateMasterVolume() 
        {
            AudioController.Instance.ChangeMasterVolume(_masterSlider.value);

            PlayerPrefs.SetFloat("MasterVolume", _masterSlider.value);
        }
        public void UpdateMusicVolume() 
        {
            AudioController.Instance.ChangeMusicVolume(_musicSlider.value);

            PlayerPrefs.SetFloat("MusicVolume", _musicSlider.value);
        }
        public void UpdateSFXVolume()
        {
            AudioController.Instance.ChangeSFXVolume(_sfxSlider.value);

            PlayerPrefs.SetFloat("SFXVolume", _sfxSlider.value);
        }

        private const string RebindsKeyZ = "rebindsZ";
        private const string RebindsKeyX = "rebindsX";

        private void Start()
        {
            string rebindsX = PlayerPrefs.GetString(RebindsKeyX, string.Empty);
            string rebindsZ = PlayerPrefs.GetString(RebindsKeyZ, string.Empty);

            if (string.IsNullOrEmpty(rebindsX) || string.IsNullOrEmpty(rebindsZ)) { return; }

        }
        public void Save()
        {
            if (_bindingsXDisplayText.text == _bindingZDisplayText.text)
            {
                print("No se puede asignar la misma tecla, eliga otra");
                return;
            }
        }


        public void TogglePause() {
            if (!GameStateChanger.CanTogglePause) return;

            isPaused = !isPaused;
            _pauseMenu.SetActive(isPaused);

            if (isPaused) GameStateChanger.Instance.UpdateGameState(GameState.Paused);
            else GameStateChanger.Instance.UpdateGameState(GameState.Playing);

            Time.timeScale = (isPaused) ? 0 : 1;
        }

        private void UpdateUI(GameState newState) {

            gameObject.SetActive(newState != GameState.Testing);
            
            _levelSelection.SetActive(newState == GameState.SelectingLevel);
            _scoreMenu.SetActive(newState == GameState.ShowingScore);
            _mainMenu.SetActive(newState == GameState.OnMainMenu);
            _failMenu.SetActive(newState == GameState.Fail);
        }
    }
}