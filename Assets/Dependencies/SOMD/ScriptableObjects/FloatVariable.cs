using UnityEngine;

namespace SOMD
{

    [CreateAssetMenu(menuName = "Scriptable Objects/Variables/Float")]
    public class FloatVariable : NumberVariable<float>
    {
        protected override float _Evaluate() => value;
    }

}
