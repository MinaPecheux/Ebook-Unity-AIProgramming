using UnityEngine;

using FSM;

public class Patrolling : BaseState {
    private Animator _animator;
    private Transform _transform;

    private Transform[] _waypoints;
    private int _waypointIndex;
    private float _speed;

    private float _waitTime = 1f; // in seconds
    private float _waitCounter = 0f;
    private bool _waiting = false;

    private float _fov;
    private const int _enemyLayerMask = 1 << 7;

    public Patrolling(UnitFSM fsm, Transform[] waypoints) : base("Patrolling", fsm)
    {
        _animator = fsm.animator;
        _transform = fsm.transform;
        _waypoints = waypoints;
        _waypointIndex = 0;
        _speed = fsm.speed;
        _fov = fsm.fieldOfVision;

        _transform.position = waypoints[0].position;
    }

    public override void Enter(params object[] transitionArgs)
    {
        Transform wp = _waypoints[_waypointIndex];
        bool onWaypoint = Vector3.Distance(_transform.position, wp.position) < 0.01f;
        _animator.SetBool("Running", !onWaypoint);
    }

    public override void Update()
    {
        base.Update();

        if (_waiting)
        {
            _waitCounter += Time.deltaTime;
            if (_waitCounter < _waitTime) return;
            _waiting = false;
            _animator.SetBool("Running", true);
        }
        Transform wp = _waypoints[_waypointIndex];
        if (Vector3.Distance(_transform.position, wp.position) < 0.01f)
        {
            _transform.position = wp.position;
            _waypointIndex = (_waypointIndex + 1) % _waypoints.Length;
            _waitCounter = 0f;
            _waiting = true;
            _animator.SetBool("Running", false);
        }
        else
        {
            _transform.position = Vector3.MoveTowards(
                _transform.position, wp.position, _speed * Time.deltaTime);
            _transform.LookAt(wp.position);
        }
    }

    public override void LateUpdate()
    {
        base.LateUpdate();

        Collider[] targets = Physics.OverlapSphere(_transform.position, _fov, _enemyLayerMask);
        if (targets.Length > 0)
            _fsm.ChangeState(_fsm.GetState("chasing"), targets[0].transform);
    }
}
