using TMPro;
using UnityEngine;

namespace Script.View
{
    public class RhymeInputView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _rhymeLabel;

        /// <summary>
        ///     ラベルの設定
        /// </summary>
        /// <param name="text"></param>
        public void SetRhymeText(string text)
        {
            _rhymeLabel.text = text;
        }
    }
}