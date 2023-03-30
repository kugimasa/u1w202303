using DG.Tweening;
using Script.Data;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Script.View
{
    public class RhymeInputView : MonoBehaviour
    {
        [SerializeField] private Image _image;
        [SerializeField] private Color _highLightColor;
        [SerializeField] private TextMeshProUGUI _rhymeLabel;

        private Sequence _inputSequence;

        /// <summary>
        ///     ラベルの設定
        /// </summary>
        /// <param name="text"></param>
        public void SetRhymeText(string text)
        {
            _rhymeLabel.text = text;
        }

        /// <summary>
        /// パッド入力演出
        /// </summary>
        public void OnInput()
        {
            _inputSequence?.Kill();
            _inputSequence = DOTween.Sequence()
                .Append(_image.DOColor(_highLightColor, 0.2f).SetEase(Ease.InFlash, 2))
                .Join(_image.rectTransform.DOScale(1.2f, 0.2f).SetEase(Ease.Flash, 2))
                .SetLink(gameObject);
        }
    }
}