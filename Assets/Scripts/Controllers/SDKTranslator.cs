using TMPro;
using UnityEngine;

namespace YG.Example
{
    public class SDKTranslator : MonoBehaviour // This class is provided from plugin YG2 for translation.
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

        /// <summary>
        /// Sets the text component to the specified language.
        /// </summary>
        /// <param name="language">The language code to switch to, such as "ru" for Russian.</param>
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