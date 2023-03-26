using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

namespace UBAI
{

    public enum ElectionPolicy
    {
        Best,
        WeightedTopN,
    }

    public abstract class UBAIBrain : MonoBehaviour
    {
        private ElectionPolicy _electionPolicy;
        private int _topCount;
        private UBAIAction[] _actions;

        public UBAIBrain(ElectionPolicy electionPolicy, UBAIAction[] actions, int topCount = 3)
        {
            _electionPolicy = electionPolicy;
            _topCount = topCount;
            _actions = actions;
        }

        public bool Execute()
        {
            UBAIAction action = null;

            if (_electionPolicy == ElectionPolicy.Best)
            {
                bool vetoed;
                float bestUtility = 0;
                foreach (UBAIAction a in _actions)
                {
                    float utility = _EvaluateAction(a, out vetoed);
                    if (!vetoed && utility > bestUtility)
                    {
                        action = a;
                        bestUtility = utility;
                    }
                }
            }
            else if (_electionPolicy == ElectionPolicy.WeightedTopN)
            {
                List<(UBAIAction, float)> actionUtilities = new List<(UBAIAction, float)>();
                bool vetoed;
                float total = 0;
                foreach (UBAIAction a in _actions)
                {
                    float utility = _EvaluateAction(a, out vetoed);
                    if (!vetoed)
                    {
                        actionUtilities.Add((a, utility));
                        total += utility;
                    }
                }

                if (actionUtilities.Count > 0)
                {
                    actionUtilities.Sort(((UBAIAction, float) item1, (UBAIAction, float) item2)
                        => item1.Item2 > item2.Item2 ? -1 : 1);

                    var topActions = actionUtilities.GetRange(0, Mathf.Min(_topCount, actionUtilities.Count));

                    float choice = Random.Range(0f, 1f) * total;
                    float accumulation = 0;
                    foreach ((UBAIAction a, float u) in topActions)
                    {
                        accumulation += u / total;
                        if (choice < accumulation)
                        {
                            action = a;
                            break;
                        }
                    }
                }

            }

            if (action != null)
            {
                GetType().GetMethod(action.func, BindingFlags.NonPublic | BindingFlags.Instance).Invoke(this, null);
                return false;
            }

            return true;
        }

        private float _EvaluateAction(UBAIAction action, out bool vetoed)
        {
            foreach (var veto in action.vetoes)
                if (veto.Evaluate() == 0)
                {
                    vetoed = true;
                    return 0f;
                }

            vetoed = false;
            return action.utility.Evaluate();
        }
    }

}
