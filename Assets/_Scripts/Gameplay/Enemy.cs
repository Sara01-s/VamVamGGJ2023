using UnityEngine;
using TMPro;

namespace VamVamGGJ {

    internal abstract class Enemy : MonoBehaviour {
        
        [SerializeField] protected TextMeshProUGUI EnemyText;
        [SerializeField] protected string[] EnemyWords;


        internal void Kill() {
            // Play death animation
            // Play death sound

            Destroy(gameObject);
        }

        private void OnTriggerEnter2D(Collider2D otherCollider) {

            if (otherCollider == GameData.EnemyActivationCollider) {
                InitializeThisEnemy();
            }
        }

        private void InitializeThisEnemy() {
            GameData.EnemyList.Add(this);
            EnemyText.text = GameData.AllGameWords[Random.Range(0, GameData.AllGameWords.Count)];
            print($"Enemigo {gameObject.name} añadido a la lista de atacables");
            
        }

        private void OnDisable() {
            GameData.EnemyList.Remove(this);
        }


        
    }
}