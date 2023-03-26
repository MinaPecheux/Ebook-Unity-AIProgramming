using UnityEngine;
using FSM;

public class Chasing : BaseState
{
    private Animator _animator;
    private Transform _transform;

    private float _speed;
    private float _fov;
    private float _attackRadius;

    private Transform _target;

    public Chasing(UnitFSM fsm) : base("Chasing", fsm)
    {
        _animator = fsm.animator;
        _transform = fsm.transform;

        _speed = fsm.speed;
        _fov = fsm.fieldOfVision;
        _attackRadius = fsm.attackRadius;
    }

    public override void Enter(params object[] transitionArgs)
    {
        if (transitionArgs == null) return;

        _target = (Transform)transitionArgs[0];
        _animator.SetBool("Running", true);
    }

    public override void Update()
    {
        base.Update();

        float d = Vector3.Distance(_transform.position, _target.position);
        if (d < _attackRadius)
        {
            _fsm.ChangeState(_fsm.GetState("attacking"), _target);
        }
        else if (d > _fov + 0.5f)
        {
            _fsm.ChangeState(_fsm.GetState("idle"));
        }
        else
        {
            _transform.position = Vector3.MoveTowards(_transform.position, _target.position, _speed * Time.deltaTime);
            _transform.LookAt(_target.position);
        }
    }

}
