using UnityEngine;
using FSM;

public class Attacking : BaseState
{
    private Animator _animator;
    private Transform _transform;

    private float _attackRadius;
    private float _attackRate;
    private float _attackCounter = 0f;

    private Transform _target;

    public Attacking(UnitFSM fsm) : base("Attacking", fsm)
    {
        _animator = fsm.animator;
        _transform = fsm.transform;

        _attackRadius = fsm.attackRadius;
        _attackRate = fsm.attackRate;
    }

    public override void Enter(params object[] transitionArgs)
    {
        if (transitionArgs == null) return;

        _target = (Transform)transitionArgs[0];
        _animator.SetBool("Running", false);
        _attackCounter = _attackRate; // hit immediately
    }

    public override void Exit()
    {
        base.Exit();
        _animator.ResetTrigger("Attack");
    }

    public override void Update()
    {
        base.Update();

        _attackCounter += Time.deltaTime;
        if (_attackCounter >= _attackRate)
        {
            _animator.SetTrigger("Attack");
            _attackCounter = 0f;
            bool targetIsDead = _target.GetComponent<UnitFSM>().TakeDamage();
            if (targetIsDead)
                _fsm.ChangeState(_fsm.GetState("idle"));
        }

        if (Vector3.Distance(_transform.position, _target.position) > _attackRadius)
        {
            _fsm.ChangeState(_fsm.GetState("chasing"), _target);
        }
    }

}
