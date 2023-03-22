using TMPro;
using UnityEngine;

namespace Script.View
{
    public class KeyBindView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _keyCodeText;

        /// <summary>
        ///     キーコード表示を更新
        /// </summary>
        /// <param name="keyStr">キーコードの文字列</param>
        public void SetKeyCodeText(string keyStr)
        {
            _keyCodeText.text = keyStr;
        }
    }
}