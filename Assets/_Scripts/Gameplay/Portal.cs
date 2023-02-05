using UnityEngine;

namespace VamVamGGJ {

    [RequireComponent(typeof(Animator))]
    internal sealed class Portal : MonoBehaviour {

        private Animator _animator;

        private void OnEnable() => EventDispatcher.OnTextSubmitted += Foo;
        private void OnDisable() => EventDispatcher.OnTextSubmitted -= Foo;

        private void Foo(string s) {
            
        }

        internal void PlayHitAnimation() {
            // _animator.SetTrigger("Hit");
        }



    }
}