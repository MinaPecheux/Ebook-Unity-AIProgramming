namespace SOMD
{

    public abstract class BaseVariable : Evaluatable
    {
        protected override void _OnValidate()
        {
            _CheckValue();
            base._OnValidate();
        }

        public abstract string Str();
        protected virtual void _CheckValue() { }
    }

}
