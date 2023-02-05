using System.Collections.Generic;
using UnityEngine;

namespace VamVamGGJ {

    [RequireComponent(typeof(BoxCollider2D))]
    internal sealed class GameData : Singleton<GameData> {

        internal static List<Enemy> EnemyList = new List<Enemy>();
        internal static BoxCollider2D EnemyActivationCollider;

        protected override void Awake() {
            base.Awake();
            EnemyActivationCollider = GetComponent<BoxCollider2D>();
        }

        internal static List<string> AllGameWords = new List<string> {
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

        internal static List<string> HarderWords = new List<string> {
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

        [SerializeField] public List<AudioClip> AllHitSounds;
        [SerializeField] public List<AudioClip> AllFinalHitSounds;



    }
    
}