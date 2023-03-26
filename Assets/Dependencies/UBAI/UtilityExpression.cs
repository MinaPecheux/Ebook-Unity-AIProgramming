using System.Data;
using UnityEngine;
using SOMD;
using CustomAttributes;

namespace UBAI
{
    [CreateAssetMenu(menuName = "Scriptable Objects/UBAI/Utility Expression")]
    public class UtilityExpression : Evaluatable
    {
        public ExpressionVariable[] variables;
        public string expression;

        public bool hasMin;
        [DrawIf("hasMin", true, ComparisonType.Equals)] public float min;

        public bool hasMax;
        [DrawIf("hasMax", true, ComparisonType.Equals)] public float max;

        protected override float _Evaluate()
        {
            DataTable dt = new DataTable();
            foreach (ExpressionVariable v in variables)
                dt.Columns.Add(v.name, typeof(float));
            dt.Columns.Add("result", typeof(float), expression);
            dt.Rows.Add(Utils.GetValues(variables));

            float result = (float) dt.Rows[0]["result"];
            if (hasMin && result < min) result = min;
            if (hasMax && result > max) result = max;
            return result;
        }
    }

}
