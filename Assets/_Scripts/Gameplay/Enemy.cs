using Random = UnityEngine.Random;
using UnityEngine;
using TMPro;

namespace VamVamGGJ {

    internal abstract class Enemy : MonoBehaviour {
        
        [SerializeField] protected EnemyLane _enemyLane = EnemyLane.MiddleLane; 
        [SerializeField] protected AnimationCurve _translationCurve;
        [SerializeField] protected TextMeshProUGUI EnemyText;
        [SerializeField] protected float _relentization = 10f;
        [SerializeField] protected Vector3 _initPos;
        [SerializeField] protected Vector3 _goalPos;

        
        private float _animationTimePosition = 0f;
        private float _spawnedTime = 0f;
        private string _enemyWord = "";


        private void Update() {
            _initPos = new Vector3(  10f, (float) _enemyLane, 0f  );
            _goalPos = new Vector3( -10f, (float) _enemyLane, 0f  );

            _animationTimePosition += Time.deltaTime;
            transform.localPosition = Vector3.Lerp(_initPos, _goalPos,
                                      _translationCurve.Evaluate(_animationTimePosition / _relentization));
        }


        private void OnEnable() {
            EventDispatcher.OnTextSubmitted += KillEnemy;
            _spawnedTime = Time.time;
        }

        private void OnDisable() {
            GameData.EnemyList.Remove(this);
            EventDispatcher.OnTextSubmitted -= KillEnemy;
        }
        
        internal void KillEnemy(string playerSubmittedString) {
            print(playerSubmittedString);

            var validWord = playerSubmittedString != null && (playerSubmittedString.CompareTo(_enemyWord) == 0);
            if (!validWord) return;

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
            _enemyWord = GameData.AllGameWords[Random.Range(0, GameData.AllGameWords.Count)];
            EnemyText.text = _enemyWord;
        }

    }
}