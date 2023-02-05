using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace VamVamGGJ
{
    public class Player : MonoBehaviour
    {
        
        internal int _playerHealth = 5;

        // Update is called once per frame
        void Update()
        {
            if (_playerHealth == 0){
                
                UIToggler.Instance._gameOverMenu.gameObject.SetActive(true);
                SceneManager.LoadScene(0);
            }
        }

        public void OnTriggerEnter2D(Collider2D other){
            if (other.CompareTag("Enemy")){
                _playerHealth -= 1;
                print("vida: " + _playerHealth);
            }
        }

    }
}
