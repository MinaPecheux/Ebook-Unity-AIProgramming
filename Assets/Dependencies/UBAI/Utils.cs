using System.Reflection;
using UnityEngine;
using SOMD;

namespace UBAI
{

    [System.Serializable]
    public class ExpressionVariable
    {
        public string name;
        public ScriptableObject variable;
    }


    public static class Utils
    {

        public static object[] GetValues(ExpressionVariable[] variables)
        {
            object[] values = new object[variables.Length];
            int i = 0;
            foreach (ExpressionVariable v in variables)
            {
                if (v.name.Contains("."))
                {
                    string fieldName = v.name.Split(".")[1];
                    FieldInfo field = v.variable.GetType().GetField(fieldName);
                    values[i++] = field.GetValue(v.variable);
                }
                else
                    values[i++] = ((Evaluatable) v.variable).Evaluate();
            }
            return values;
        }

    }

}
