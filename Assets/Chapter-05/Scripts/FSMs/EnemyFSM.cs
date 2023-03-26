using System.Collections.Generic;

using FSM;

public class EnemyFSM : UnitFSM
{
    protected override void Awake()
    {
        base.Awake();
        _states = new Dictionary<string, BaseState>()
        {
            { "idle", new Idle(this) },
            { "chasing", new Chasing(this) },
            { "attacking", new Attacking(this) },
            { "dying", new Dying(this) },
        };
    }

    protected override BaseState GetInitialState() => _states["idle"];
}
