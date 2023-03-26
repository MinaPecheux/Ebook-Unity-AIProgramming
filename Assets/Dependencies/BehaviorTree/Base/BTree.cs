using UnityEngine;

namespace BehaviorTree
{
    public abstract class BTree : MonoBehaviour
    {
        protected Node _root = null;

        protected virtual void Awake()
        {
            Node.LAST_ID = 0;
            _root = SetupTree();
        }

        private void Update()
        {
            if (_root != null)
                _root.Evaluate();
        }

        public Node Root => _root;
        protected abstract Node SetupTree();
    }
}
