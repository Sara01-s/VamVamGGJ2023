using UnityEngine.UI;
using UnityEngine;
using System;
using TMPro;

namespace VamVamGGJ {

    public sealed class UIToggler : Singleton<UIToggler> {

        [SerializeField] private GameObject _mainMenu, _settingsMenu, _pauseMenu;

        private GameState _selectedStateInInspector;
        private bool _isPaused = false;

        protected override void Awake() {
            base.Awake();
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
            if (_isPaused) EventDispatcher.OnTogglePause?.Invoke();
            LevelLoader.Instance.LoadScene(index, _selectedStateInInspector);
        }

        public void TogglePause() {
            if (!GameStateChanger.CanTogglePause) return;

            _isPaused = !_isPaused;
            _pauseMenu.SetActive(_isPaused);

            if (_isPaused) GameStateChanger.Instance.UpdateGameState(GameState.Paused);
            else GameStateChanger.Instance.UpdateGameState(GameState.Playing);

            Time.timeScale = (_isPaused) ? 0 : 1;
        }

        private void UpdateUI(GameState newState) {

            gameObject.SetActive(newState != GameState.Testing);
            
        }
    }
}