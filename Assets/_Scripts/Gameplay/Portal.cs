using UnityEngine;

namespace VamVamGGJ {

    [RequireComponent(typeof(Animator))]
    internal sealed class Portal : MonoBehaviour {

        [SerializeField] private Animator _animator;

        private void Awake() {
            _animator = gameObject.GetComponent<Animator>();
        }

        internal void FinalHitAnimation() {
            _animator.SetTrigger("PortalFinalHit");
            print("ANIMACION PORTAL FINAL HIT");
        }

        internal void NormalHitAnimation() {
            _animator.SetTrigger("PortalNormalHit");
            print("ANIMACION NORMAL HIT");
        }
    }
}