using UnityEngine;
using System;

namespace VamVamGGJ {

    public sealed class UIToggler : Singleton<UIToggler> {

        [SerializeField] private GameObject _mainMenu, _settingsMenu, _pauseMenu;
        public GameObject _gameOverMenu, _victoryMenu;

        private GameState _selectedStateInInspector;
        private bool _isPaused = false;

        protected override void Awake() {
            base.Awake();
        }

        private void Update() {

            if (Input.GetKeyDown(KeyCode.Escape)) {
                TogglePause();
            }
        }

        public void OnApplicationQuit() => Application.Quit();

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
            _selectedStateInInspector = Enum.Parse<GameState>(nextState);
        }

        public void LoadScene(int index) {
            if (_isPaused) EventDispatcher.OnTogglePause?.Invoke();
            LevelLoader.Instance.LoadScene(index, _selectedStateInInspector);
        }

        public void TogglePause() {
            if (!GameStateChanger.CanTogglePause) return;

            _isPaused = !_isPaused;
            _pauseMenu.SetActive(_isPaused);

            if (_isPaused) {
                GameStateChanger.Instance.UpdateGameState(GameState.Paused);
                AudioController.Instance.PauseMusic();
            }
            else {
                GameStateChanger.Instance.UpdateGameState(GameState.Playing);
                AudioController.Instance.ResumeMusic();
            } 

            Time.timeScale = (_isPaused) ? 0 : 1;
        }

        public void MainMenuActivator(){
            _mainMenu.gameObject.SetActive(true);
        }

        public void UIMenuDesactivator(int _numeroIngresado){
            if (_numeroIngresado == 1)
            _gameOverMenu.gameObject.SetActive(false);
            if (_numeroIngresado == 2)
            _victoryMenu.gameObject.SetActive(false);
        }

        private void UpdateUI(GameState newState) {

            gameObject.SetActive(newState != GameState.Testing);
            
        }
    }
}