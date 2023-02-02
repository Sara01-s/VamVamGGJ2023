using UnityEngine.SceneManagement;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.UI;
using UnityEngine;

namespace VamVamGGJ {
    
    public sealed class LevelLoader : Singleton<LevelLoader> {

        [Header("Child UI")]
        [SerializeField] private GameObject _levelManagerUI;
        [SerializeField] private Image _progressBarImage;

        private List<AsyncOperation> _scenesToLoad = new List<AsyncOperation>();

        protected override void Awake() => base.Awake();
        
        public void LoadScene(int index, GameState newState) {

            GameStateChanger.Instance.UpdateGameState(GameState.LoadingScreen);

            _scenesToLoad.Add(SceneManager.LoadSceneAsync(index));

            StartCoroutine(LoadingScreen(newState));
        }

        private IEnumerator LoadingScreen(GameState newState) {
            
            float totalProgress = 0f;

            for (int i = 0; i < _scenesToLoad.Count; i++) {
                
                while (!_scenesToLoad[i].isDone) {
                    // Opcion 1: totalProgress += _scenesToLoad[i].progress;
                    totalProgress++;
                    _progressBarImage.fillAmount = totalProgress / _scenesToLoad.Count;

                    // Don't exit the coroutine until the scene load is done
                    // (this is done by returning null every frame)
                    yield return null;
                }
            }

            GameStateChanger.Instance.UpdateGameState(newState);
        }

        public void ReloadLevel() {
            LoadScene(SceneManager.GetActiveScene().buildIndex, GameState.Playing);
        }
    }
}