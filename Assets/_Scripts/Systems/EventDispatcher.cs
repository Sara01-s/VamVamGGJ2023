using System;

namespace VamVamGGJ {

    public sealed class EventDispatcher : Singleton<EventDispatcher> {
        
        protected override void Awake() => base.Awake();

        // Input Manager //
        public static Action OnTogglePause;

        // Game Manager // 
        public static Action<GameState> OnGameStateChanged;

    }
}