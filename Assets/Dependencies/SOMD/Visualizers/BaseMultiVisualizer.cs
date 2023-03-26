using UnityEngine;

namespace SOMD.Visualizers
{

    public abstract class BaseMultiVisualizer : MonoBehaviour
    {
        public Evaluatable[] data;

        private void OnEnable()
        {
            foreach (Evaluatable item in data)
                item.updated.AddListener(_UpdateDisplay);
        }

        private void OnDisable()
        {
            foreach (Evaluatable item in data)
                item.updated.RemoveListener(_UpdateDisplay);
        }

        private void Start()
        {
            _UpdateDisplay();
        }

        protected abstract void _UpdateDisplay();
    }

}
