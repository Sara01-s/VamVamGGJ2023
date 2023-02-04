using System;

namespace VamVamGGJ {

    internal sealed class EventDispatcher : Singleton<EventDispatcher> {
        
        protected override void Awake() => base.Awake();

        // Input Manager //
        internal static Action OnTogglePause;
        internal static Action<string> OnTextSubmitted;

        // Game Manager //
        internal static Action<GameState> OnGameStateChanged;

    }
}