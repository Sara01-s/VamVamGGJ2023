using UnityEngine;

namespace VamVamGGJ {
    /// <summary>
    /// This class instantiate "System", The prefab that contains all persistent Managers,
    /// this happens before the scene is loaded
    /// </summary>
    public static class GameInitializer {

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        public static void InitializeSystems() {

            Object.DontDestroyOnLoad(
                Object.Instantiate(Resources.Load("Core/GameCore")) as GameObject);
        }
    }
}
