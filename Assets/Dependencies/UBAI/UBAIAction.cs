using SOMD;

namespace UBAI
{

    [System.Serializable]
    public class UBAIAction
    {
        public Evaluatable utility;
        public string func;
        public Evaluatable[] vetoes;
    }

}
