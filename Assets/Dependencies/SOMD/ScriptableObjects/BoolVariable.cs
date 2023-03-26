using UnityEngine;

namespace SOMD
{

    [CreateAssetMenu(menuName = "Scriptable Objects/Variables/Bool")]
    public class BoolVariable : BaseVariable
    {
        public bool value;

        public override string Str() => $"{value}";
        protected override float _Evaluate() => value ? 1f : 0f;
    }

}
