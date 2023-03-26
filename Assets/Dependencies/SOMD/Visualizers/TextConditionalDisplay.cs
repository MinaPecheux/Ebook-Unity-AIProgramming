using UnityEngine;
using TMPro;

namespace SOMD.Visualizers
{

    [RequireComponent(typeof(TextMeshProUGUI))]
    public class TextConditionalDisplay : BaseVisualizer
    {
        private TextMeshProUGUI _text;
        [SerializeField] private string _trueText;
        [SerializeField] private string _falseText;

        private BoolVariable _boolData;

        private void Awake()
        {
            _text = GetComponent<TextMeshProUGUI>();
            _boolData = (BoolVariable)data;
        }

        protected override void _UpdateDisplay()
        {
            _text.text = _boolData.value ? _trueText : _falseText;
        }
    }

}
