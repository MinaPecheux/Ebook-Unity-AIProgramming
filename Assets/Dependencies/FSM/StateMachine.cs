using System.Collections.Generic;
using UnityEngine;

namespace FSM
{

    public abstract class StateMachine : MonoBehaviour
    {
#if UNITY_EDITOR
        private static uint _LAST_UID = 0; // IDs are only used to stack debugs in column
        private uint _id;
        [SerializeField] protected bool _showDebug;
#endif

        protected Dictionary<string, BaseState> _states;
        private BaseState _currentState;

        void Start()
        {
#if UNITY_EDITOR
            _id = _LAST_UID++;
#endif
            _currentState = GetInitialState();
            if (_currentState != null)
                _currentState.Enter(null);
        }

        void Update()
        {
            if (_currentState != null)
                _currentState.Update();
        }

        void LateUpdate()
        {
            if (_currentState != null)
                _currentState.LateUpdate();
        }

        protected abstract BaseState GetInitialState();
        public BaseState GetState(string code) => _states[code];

        public void ChangeState(BaseState newState, params object[] transitionArgs)
        {
            if (_currentState != null) _currentState.Exit();
            _currentState = newState;
            if (_currentState != null) _currentState.Enter(transitionArgs);
        }

#if UNITY_EDITOR
        private void OnGUI()
        {
            if (_showDebug)
            {
                GUILayout.BeginArea(new Rect(10f, 10f + 70f * _id, 500f, 70f));
                string content = _currentState != null ? _currentState.name : "(no current state)";
                content = $"{gameObject.name}: {content}";
                GUILayout.Label($"<color='white'><size=40>{content}</size></color>");
                GUILayout.EndArea();
            }
        }
#endif
    }
}
