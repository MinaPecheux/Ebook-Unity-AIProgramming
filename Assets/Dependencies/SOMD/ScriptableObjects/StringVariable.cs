using UnityEngine;

namespace SOMD
{

    [CreateAssetMenu(menuName = "Scriptable Objects/Variables/String")]
    public class StringVariable : BaseVariable
    {
        public string value;

        public override string Str() => value;
        protected override float _Evaluate()
        {
            throw new System.NotSupportedException();
        }
    }

}
