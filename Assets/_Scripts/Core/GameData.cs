using System.Collections.Generic;
using UnityEngine;

namespace VamVamGGJ {

    [RequireComponent(typeof(BoxCollider2D))]
    internal sealed class GameData : MonoBehaviour {

        internal static List<Enemy> EnemyList = new List<Enemy>();
        internal static BoxCollider2D EnemyActivationCollider;

        private void Awake() {
            EnemyActivationCollider = GetComponent<BoxCollider2D>();
        }

        internal static readonly List<string> AllGameWords = new List<string> {
            "building",
            "old man",
            "hello",
            "house",
            "water",
            "stick",
            "fish",
            "tree",
            "dog",
            "sea",
        };


    }
    
}