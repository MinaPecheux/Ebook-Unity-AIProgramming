using UnityEngine;
using UnityEngine.Events;

namespace SOMD
{

    public abstract class Evaluatable : ScriptableObject
    {
        [HideInInspector] public UnityEvent updated;

        private void OnEnable()
        {
            if (updated == null)
                updated = new UnityEvent();
        }

        private void OnValidate() { _OnValidate(); }
        protected virtual void _OnValidate()
        {
            updated.Invoke();
        }

        public float Evaluate()
        {
            float r = _Evaluate();
            updated.Invoke();
            return r;
        }
        public float RawEvaluate() => _Evaluate();
        protected abstract float _Evaluate();
    }

}
