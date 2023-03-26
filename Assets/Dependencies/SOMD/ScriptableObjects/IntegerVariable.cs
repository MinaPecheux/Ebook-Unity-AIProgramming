using UnityEngine;

namespace SOMD
{

    [CreateAssetMenu(menuName = "Scriptable Objects/Variables/Integer")]
    public class IntegerVariable : NumberVariable<int>
    {
        protected override float _Evaluate() => value;
    }

}
