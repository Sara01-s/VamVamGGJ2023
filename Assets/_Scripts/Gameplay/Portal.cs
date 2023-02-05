using UnityEngine;

namespace VamVamGGJ {

    [RequireComponent(typeof(Animator))]
    internal sealed class Portal : MonoBehaviour {

        [SerializeField] private Animator _animator;

        private void Awake() {
            gameObject.GetComponent<Animator>();
        }

        /*private void OnEnable() => EventDispatcher.OnTextSubmitted += FinalHitAnimation;
        private void OnDisable() => EventDispatcher.OnTextSubmitted -= FinalHitAnimation;*/

        internal void FinalHitAnimation() {
            _animator.SetTrigger("PortalFinalHit");
        }

        internal void NormalHitAnimation() {
            _animator.SetTrigger("PortalNormalHit");
        }
    }
}