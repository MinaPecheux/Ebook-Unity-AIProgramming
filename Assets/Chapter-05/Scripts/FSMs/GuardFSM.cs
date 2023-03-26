using System.Collections.Generic;
using UnityEngine;

using FSM;

public class GuardFSM : UnitFSM
{
    public Transform[] waypoints;

    protected override void Awake()
    {
        base.Awake();
        _states = new Dictionary<string, BaseState>()
        {
            { "idle", new Patrolling(this, waypoints) },
            { "chasing", new Chasing(this) },
            { "attacking", new Attacking(this) },
            { "dying", new Dying(this) },
        };
    }

  protected override BaseState GetInitialState() => _states["idle"];

  protected override void OnDrawGizmos()
  {
        //if (waypoints == null || waypoints.Length == 0) return;
        ////base.OnDrawGizmos();
        //Gizmos.color = Color.green;
        //Vector3 h = Vector3.up * 0.02f;
        //Gizmos.DrawLine(waypoints[0].position + h, waypoints[1].position + h);
        //Gizmos.DrawLine(waypoints[1].position + h, waypoints[2].position + h);
        //Gizmos.DrawLine(waypoints[2].position + h, waypoints[3].position + h);
        //Gizmos.DrawLine(waypoints[3].position + h, waypoints[0].position + h);

        //Gizmos.DrawSphere(waypoints[0].position, 0.2f);
        //Gizmos.DrawSphere(waypoints[1].position, 0.2f);
        //Gizmos.DrawSphere(waypoints[2].position, 0.2f);
        //Gizmos.DrawSphere(waypoints[3].position, 0.2f);
    }
}
