using UnityEngine;
using SOMD;

namespace UBAI
{

    [CreateAssetMenu(menuName = "Scriptable Objects/UBAI/Utility Evaluator")]
    public class UtilityEvaluator : Evaluatable
    {
        public Evaluatable variable;
        public AnimationCurve responseCurve;

        protected override float _Evaluate() => responseCurve.Evaluate(variable.Evaluate());
    }

}
