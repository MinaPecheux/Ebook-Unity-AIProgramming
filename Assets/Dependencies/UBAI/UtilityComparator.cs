using System.Data;
using UnityEngine;
using SOMD;
using CustomAttributes;

namespace UBAI
{
    public enum ComparisonOp
    {
        LessThan,
        LessOrEqual,
        Equal,
        NotEqual,
        GreaterThan,
        GreatherOrEqual
    }

    [CreateAssetMenu(menuName = "Scriptable Objects/UBAI/Utility Comparator")]
    public class UtilityComparator : Evaluatable
    {
        public ExpressionVariable[] variables;
        public string leftExpression;
        public ComparisonOp op;
        public string rightExpression;

        protected override float _Evaluate()
        {
            DataTable dt = new DataTable();
            foreach (ExpressionVariable v in variables)
                dt.Columns.Add(v.name, typeof(float));
            dt.Columns.Add("left", typeof(float), leftExpression);
            dt.Columns.Add("right", typeof(float), rightExpression);
            dt.Rows.Add(Utils.GetValues(variables));

            float left = (float)dt.Rows[0]["left"];
            float right = (float)dt.Rows[0]["right"];
            switch (op)
            {
                case ComparisonOp.LessThan:
                    return (left < right) ? 1 : 0;
                case ComparisonOp.LessOrEqual:
                    return (left <= right) ? 1 : 0;
                case ComparisonOp.Equal:
                    return (left == right) ? 1 : 0;
                case ComparisonOp.NotEqual:
                    return (left != right) ? 1 : 0;
                case ComparisonOp.GreaterThan:
                    return (left > right) ? 1 : 0;
                case ComparisonOp.GreatherOrEqual:
                default:
                    return (left >= right) ? 1 : 0;
            }
        }
    }

}
