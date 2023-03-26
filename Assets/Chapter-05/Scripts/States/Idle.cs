using UnityEngine;

using FSM;

public class Idle : BaseState
{
    private Animator _animator;
    private Transform _transform;

    private float _fov;
    private const int _playerLayerMask = 1 << 8;

    public Idle(UnitFSM fsm) : base("Idle", fsm)
    {
        _animator = fsm.animator;
        _transform = fsm.transform;
        _fov = fsm.fieldOfVision;
    }

    public override void Enter(params object[] transitionArgs)
    {
        _animator.SetBool("Running", false);
    }

    public override void LateUpdate()
    {
        base.LateUpdate();

        Collider[] targets = Physics.OverlapSphere(_transform.position, _fov, _playerLayerMask);
        if (targets.Length > 0)
        {
            _fsm.ChangeState(_fsm.GetState("chasing"), targets[0].transform);
        }
    }

}
