using UnityEngine;
using CustomAttributes;

namespace SOMD
{

    public abstract class NumberVariable<T> : BaseVariable where T : System.IComparable<T>
    {
        [SerializeField] private T _value;

        public bool hasMin;
        [DrawIf("hasMin", true, ComparisonType.Equals)] public T min;

        public bool hasMax;
        [DrawIf("hasMax", true, ComparisonType.Equals)] public T max;

        public T value
        {
            get { return _value; }
            set
            {
                _value = value;
                _CheckValue();
            }
        }

        public override string Str() => $"{_value}";
        protected override void _CheckValue()
        {
            if (hasMin && _value.CompareTo(min) < 0) _value = min;
            if (hasMax && _value.CompareTo(max) > 0) _value = max;
        }
    }

}
