using UnityEngine;
using System.Text;

namespace VamVamGGJ {

    internal sealed class TextWeaponSystem : MonoBehaviour {

        private StringBuilder _playerInputSB = new StringBuilder();
        private WordsData _wordsData;

        private const char BACKSPACE = '\b';
        private const char RETURN = '\r';
        private const char ENTER = '\n';

        private void Update() {
            
            foreach (char character in Input.inputString) {

                switch (character) {
                    case BACKSPACE:
                        if (_playerInputSB.Length != 0)
                            _playerInputSB.Length--;
                        break;

                    case RETURN: case ENTER when _playerInputSB.Length != 0:
                        print($"Output string: {_playerInputSB.ToString()}");
                        _playerInputSB.Clear();
                        break;
                    
                    default:
                        _playerInputSB.Append(character);
                        break;
                }

            }
        }
    }
}
