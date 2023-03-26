using UnityEngine;
using TMPro;

namespace SOMD.Visualizers
{

    [RequireComponent(typeof(TextMeshProUGUI))]
    public class TextDisplay : BaseVisualizer
    {
        private TextMeshProUGUI _text;
        private StringVariable _strData;

        private void Awake()
        {
            _text = GetComponent<TextMeshProUGUI>();
            _strData = (StringVariable)data;
        }

        protected override void _UpdateDisplay()
        {
            _text.text = _strData.value;
        }
    }

}
