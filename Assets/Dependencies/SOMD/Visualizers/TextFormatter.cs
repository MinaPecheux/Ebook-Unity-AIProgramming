using System.Linq;
using UnityEngine;
using TMPro;

namespace SOMD.Visualizers
{

    [RequireComponent(typeof(TextMeshProUGUI))]
    public class TextFormatter : BaseMultiVisualizer
    {
        private TextMeshProUGUI _text;
        [SerializeField] private string _format;

        private void Awake()
        {
            _text = GetComponent<TextMeshProUGUI>();
        }

        protected override void _UpdateDisplay()
        {
            _text.text = System.String.Format(_format, data.Select(
              (Evaluatable item) => item.RawEvaluate().ToString()).ToArray());
        }
    }

}
