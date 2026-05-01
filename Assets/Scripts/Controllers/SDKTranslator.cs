using TMPro;
using UnityEngine;

namespace YG.Example
{
    public class SDKTranslator : MonoBehaviour
    {
        public string ru, en;

        private TextMeshProUGUI _textComponent;

        private void Awake()
        {
            _textComponent = GetComponent<TextMeshProUGUI>();
            SwitchLanguage(YG2.lang);
        }

        private void Update()
        {
            _textComponent = GetComponent<TextMeshProUGUI>();
            SwitchLanguage(YG2.lang);
        }

        private void OnEnable()
        {
            YG2.onSwitchLang += SwitchLanguage;
        }
        private void OnDisable()
        {
            YG2.onSwitchLang -= SwitchLanguage;
        }

        public void SwitchLanguage(string language)
        {
            switch (language)
            {
                case "en":
                    _textComponent.text = en;
                    break;
                default:
                    _textComponent.text = ru;
                    break;
            }
        }
    }
}