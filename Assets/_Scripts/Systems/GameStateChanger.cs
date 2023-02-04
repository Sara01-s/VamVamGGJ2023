using UnityEngine.SceneManagement;
using UnityEngine;
using System;

namespace VamVamGGJ {

    [ExecuteInEditMode]
    public sealed class GameStateChanger : Singleton<GameStateChanger> {

        internal GameState State { get; private set; }

        public static bool CanTogglePause { get; private set; } = false;

        protected override void Awake() {
            base.Awake();

        #if UNITY_EDITOR
            // If we start the game in the Prototype Scene we just set state to Testing
            if (SceneManager.GetActiveScene().name == "Prototype") {
                UpdateGameState(GameState.Testing);
                return;
            }

            // Check if we started the game from a scene different to Main menu 
            // then, we update the context of the game accordingly
            switch (SceneManager.GetActiveScene().buildIndex) {
                case 0: // Main title
                    UpdateGameState(GameState.OnMainMenu);
                break;
                case 1: // In-Game
                    UpdateGameState(GameState.Playing);
                break;
            }
        #else
            UpdateGameState(GameState.OnMainMenu);
        #endif
        }

        public void UpdateGameState(GameState newState) {
            State = newState;

            switch (State) {
                case GameState.OnMainMenu:
                    HandleMainMenu();
                break;
                case GameState.Playing:
                    HandlePlayingState();
                break;
                case GameState.Paused:
                    HandlePause();
                break;
                case GameState.Fail:
                    HandleFail();
                break;
                case GameState.LoadingScreen:
                    HandleLoading();
                break;
                case GameState.Testing:
                    HandleTestMode();
                break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(newState), newState, null);
            }

            Debug.Log($"Game state: {newState}");

            // This is the same as OnGameStateChanged(); but checking if the delegate is null
            EventDispatcher.OnGameStateChanged?.Invoke(newState);
        }

        private void HandleMainMenu() {
            CanTogglePause = false;
        }

        private void HandlePlayingState() {
            CanTogglePause = true;
        }

        private void HandlePause() {
            CanTogglePause = true;
        }

        private void HandleFail() {
            CanTogglePause = false;
        }

        private void HandleLoading() {
            CanTogglePause = false;
        }
        
        private void HandleTestMode() {
            CanTogglePause = true;
        }

    }
}