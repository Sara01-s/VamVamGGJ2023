using DG.Tweening;
using UnityEngine;
using System.Text;
using TMPro;

namespace VamVamGGJ {

    internal sealed class TextWeaponSystem : MonoBehaviour {

        [SerializeField] private TextMeshProUGUI _playerTextUI;
        [SerializeField] private float _characterLimit = 11f;

        private StringBuilder _playerInputSB = new StringBuilder();
        private Vector3 _initPlayerTextUIPosition;

        private const char BACKSPACE = '\b';
        private const char RETURN = '\r';
        private const char ENTER = '\n';

        private void Start() {
            _initPlayerTextUIPosition = _playerTextUI.transform.position;
        }

        private void Update() {
            
            foreach (char character in Input.inputString) {

                if (character == BACKSPACE) {
                    // Delete

                    if (_playerInputSB.Length != 0) {
                        _playerInputSB.Length--;
                    }
                    
                    EventDispatcher.OnTextChanged?.Invoke(_playerInputSB.ToString());
                    _playerTextUI.text = _playerInputSB.ToString();
                    
                }
                else if ((character == RETURN || character == ENTER) && _playerInputSB.Length != 0) {
                    // On submit
                    EventDispatcher.OnTextSubmitted?.Invoke(_playerInputSB.ToString());
                    EventDispatcher.OnTextChanged?.Invoke(string.Empty);

                    _playerInputSB.Clear();
                    _playerTextUI.text = _playerInputSB.ToString();

                }
                else {
                    if (character == BACKSPACE || character == RETURN || character == ENTER) return;

                    if (_playerInputSB.Length >= _characterLimit) {
                        _playerTextUI.transform.DOShakePosition(0.1f, 3f).OnComplete(() => {
                            _playerTextUI.transform.position = _initPlayerTextUIPosition;
                        });
                        break;
                    }

                    _playerInputSB.Append(character);
                    
                    if (_playerInputSB.ToString().CompareTo(" ") == 0) 
                        _playerInputSB.Clear();

                    EventDispatcher.OnTextChanged?.Invoke(_playerInputSB.ToString());
                    _playerTextUI.text = _playerInputSB.ToString();
                }

               
            }
        }
    }
}
