using UnityEngine;
using CustomAttributes;
using SOMD;

namespace UBAI
{
    [System.Serializable]
    public class UtilityComponent
    {
        public Evaluatable variable;
        public float weight;
    }

    [CreateAssetMenu(menuName = "Scriptable Objects/UBAI/Utility Combinator")]
    public class UtilityCombinator : Evaluatable
    {
        public UtilityComponent[] components;

        public bool hasMin;
        [DrawIf("hasMin", true, ComparisonType.Equals)] public float min;

        public bool hasMax;
        [DrawIf("hasMax", true, ComparisonType.Equals)] public float max;

        protected override float _Evaluate()
        {
            float value = 0f;
            foreach (UtilityComponent c in components)
                value += c.variable.Evaluate() * c.weight;
            if (hasMin && value < min) value = min;
            if (hasMax && value > max) value = max;
            return value;
        }
    }

}
