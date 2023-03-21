using UnityEngine;
using UnityEngine.UI;

namespace Script.View
{
    public class NoteView : MonoBehaviour
    {
        [SerializeField] private Slider _noteSliderR;
        [SerializeField] private Slider _noteSliderL;
        [SerializeField] private Image _noteImage;

        private const float C4 = 2 * Mathf.PI / 3;

        /// <summary>
        ///     tの値に応じて演出させる
        /// </summary>
        /// <param name="t"></param>
        public void UpdateView(float t)
        {
            SetSliderValue(t);
        }

        /// <summary>
        ///     スライダー位置のセット
        /// </summary>
        /// <param name="t"></param>
        private void SetSliderValue(float t)
        {
            _noteSliderR.value = Mathf.Clamp01(t);
            _noteSliderL.value = Mathf.Clamp01(t);
            AnimateNoteImage(t);
        }

        /// <summary>
        ///     ノーツUIの中心のスプライト演出
        /// </summary>
        /// <param name="t"></param>
        private void AnimateNoteImage(float t)
        {
            // Reference: https://easings.net/#easeOutElastic
            var easeOutElastic = Mathf.Pow(2, -10 * t) * Mathf.Sin((t * 10 - 0.75f) * C4) + 1;
            _noteImage.rectTransform.localScale = Vector3.one + Vector3.one * easeOutElastic;
        }
    }
}