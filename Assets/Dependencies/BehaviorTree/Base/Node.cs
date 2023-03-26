using System.Collections.Generic;

namespace BehaviorTree
{
    public enum NodeState
    {
        RUNNING,
        SUCCESS,
        FAILURE
    }

    public abstract class Node
    {
        protected NodeState _state;
        public NodeState State => _state;

        public static uint LAST_ID = 0;
        private uint _id;
        public uint Id => _id;

        private Node _parent;
        public Node Parent => _parent;

        private List<Node> _children;
        public List<Node> Children => _children;
        public bool HasChildren => _children.Count > 0;

        private Node _root;
        public Node Root => _root;

        private Dictionary<string, object> _data = new Dictionary<string, object>();

        public Node()
        {
            _id = LAST_ID++;
            _parent = null;
            _children = new List<Node>();
            _root = this;
        }
        public Node(List<Node> children) : this()
        {
            SetChildren(children);
        }

        public abstract NodeState Evaluate();

        public void SetChildren(List<Node> children, bool setRoot = false)
        {
            foreach (Node c in children) Attach(c);
            if (setRoot) SetRoot(this);
        }

        public void Attach(Node child)
        {
            _children.Add(child);
            child._parent = this;
            child._root = _root;
        }

        public void Detach(Node child)
        {
            _children.Remove(child);
            child._parent = null;
            child._root = null;
        }

        public void SetRoot(Node root)
        {
            _root = root;
            foreach (Node child in _children)
                child.SetRoot(root);
        }

        public object GetData(string key)
        {
            object val = null;
            if (_data.TryGetValue(key, out val))
                return val;

            Node node = _parent;
            if (node != null)
                val = node.GetData(key);
            return val;
        }

        public void SetData(string key, object value)
        {
          _data[key] = value;
        }

        public bool ClearData(string key)
        {
            bool cleared = false;
            if (_data.ContainsKey(key))
            {
                _data.Remove(key);
                return true;
            }

            Node node = _parent;
            if (node != null)
                cleared = node.ClearData(key);
            return cleared;
        }
    }
}
