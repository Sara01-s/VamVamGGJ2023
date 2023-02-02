using UnityEngine;

namespace VamVamGGJ {

    public abstract class StaticInstance<T> : MonoBehaviour where T : MonoBehaviour {

        public static T Instance { get; private set; }

        protected StaticInstance() { }

        protected virtual void Awake() {
            Instance = this as T;
        }

        protected virtual void OnAppQuit() {
            Instance = null;
            Debug.LogWarning("Static instance destroyed");
            Destroy(gameObject);
        }
    }

    public abstract class Singleton<T> : StaticInstance<T> where T : MonoBehaviour {

        protected override void Awake() {
            if (Instance != null) {
                Debug.LogWarning("A singleton duplicate has been destroyed");
                Destroy(gameObject);
            }
            base.Awake(); // instance = this;
        }
    }

    public abstract class PersistentSingleton<T> : Singleton<T> where T : MonoBehaviour {

        protected override void Awake() {
            base.Awake(); // instance = this;
            DontDestroyChildOnLoad(gameObject);
        }

        public static void DontDestroyChildOnLoad(GameObject child) {
            Transform parentTransform = child.transform;

            // If this transform doesn't have a parent, keep searching
            while (parentTransform.parent != null) {

                parentTransform = parentTransform.parent;
            }

            GameObject.DontDestroyOnLoad(parentTransform.gameObject);
        }
    }
}