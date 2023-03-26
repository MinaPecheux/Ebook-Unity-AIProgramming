using UnityEngine;

using FSM;

public class Dying : BaseState
{
    private Animator _animator;
    private BoxCollider _collider;

    public Dying(UnitFSM fsm) : base("Dying", fsm)
    {
        _animator = fsm.animator;
        _collider = fsm.GetComponent<BoxCollider>();
    }

    public override void Enter(params object[] transitionArgs)
    {
        _animator.SetTrigger("Die");
        _collider.enabled = false;
    }

}
