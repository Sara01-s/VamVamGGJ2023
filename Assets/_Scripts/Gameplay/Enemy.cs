using Random = UnityEngine.Random;
using System.Text;
using UnityEngine;
using DG.Tweening;
using TMPro;

namespace VamVamGGJ {

    internal abstract class Enemy : MonoBehaviour {
        
        [SerializeField] protected EnemyLane _enemyLane = EnemyLane.MiddleUpLane; 
        [SerializeField] protected AnimationCurve _translationCurve;
        [SerializeField] protected Portal _topPortal, _frontPortal;
        [SerializeField] protected float _relentization = 10f;
        [SerializeField] protected TextMeshProUGUI _enemyTextUI;
        [SerializeField] protected Vector3 _initPos;
        [SerializeField] protected Vector3 _goalPos;

        
        
        private float _animationTimePosition = 0f;
        private float _spawnedTime = 0f;
        private string _enemyWord = "";
        private bool _isInitiliazed = false;

        private const string RICH_TEXT_GREEN = "<color=\"green\">";
        private const string RICH_TEXT_RED = "<color=\"red\">";


        private void Update() {
            _initPos = new Vector3(  10f, (float) _enemyLane, 7f  );
            _goalPos = new Vector3( -10f, (float) _enemyLane, 7f  );

            _animationTimePosition += Time.deltaTime;
            transform.localPosition = Vector3.Lerp(_initPos, _goalPos,
                                      _translationCurve.Evaluate(_animationTimePosition / _relentization));
        }


        private void OnEnable() {
            EventDispatcher.OnTextChanged += ReceiveDamage;
            EventDispatcher.OnTextSubmitted += KillEnemy;
            _spawnedTime = Time.time;
        }

        private void OnDisable() {
            GameData.EnemyList.Remove(this);
            EventDispatcher.OnTextChanged -= ReceiveDamage;
            EventDispatcher.OnTextSubmitted -= KillEnemy;
        }


        internal void ReceiveDamage(string inputText) {
            if (!_isInitiliazed) return;
            var modifiedInputText = inputText.Replace(' ', '_');

            if (_enemyWord.StartsWith(inputText)) {
                var _lastChar = inputText.Length;
                _enemyTextUI.text = ColorizeChar(modifiedInputText, RICH_TEXT_GREEN) + _enemyWord[_lastChar.._enemyWord.Length];
            }
            else if (inputText.StartsWith(_enemyWord)) {
                var _lastChar = _enemyWord.Length;
                _enemyTextUI.text = ColorizeChar(modifiedInputText[0.._lastChar], RICH_TEXT_GREEN) +
                                    ColorizeChar(modifiedInputText[_lastChar..inputText.Length], RICH_TEXT_RED);
            } 
            else return;

            _topPortal.gameObject.SetActive(true);
            _topPortal.NormalHitAnimation();
        }
        
        internal void KillEnemy(string playerSubmittedString) {
            var validWord = playerSubmittedString != null && (playerSubmittedString.CompareTo(_enemyWord) == 0);
            if (!validWord) return;

            // Play death sound

            // Play death animation
            _frontPortal.gameObject.SetActive(true);
            _topPortal.gameObject.SetActive(false);
            _topPortal.FinalHitAnimation();

            Destroy(gameObject, .5f);
        }

        private string ColorizeChar(string inputString, string richTextColorCode) {
            var strBuilder = new StringBuilder();

            strBuilder.Append(richTextColorCode + inputString + "</color>");
            return strBuilder.ToString();
        }

        private void OnTriggerEnter2D(Collider2D otherCollider) {

            if (otherCollider == GameData.EnemyActivationCollider) {
                InitializeThisEnemy();
            }
        }

        private void InitializeThisEnemy() {
            _isInitiliazed = true;
            GameData.EnemyList.Add(this);
            _enemyWord = GameData.AllGameWords[Random.Range(0, GameData.AllGameWords.Count)];
            _enemyTextUI.text = _enemyWord;
        }

    }
}