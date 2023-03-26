namespace FSM
{

    public abstract class BaseState
    {
        public string name;
        protected StateMachine _fsm;

        public BaseState(string name, StateMachine fsm)
        {
            this.name = name;
            _fsm = fsm;
        }

        public virtual void Enter(params object[] transitionArgs) { }
        public virtual void Update() { }
        public virtual void LateUpdate() { }
        public virtual void Exit() { }
    }

}
