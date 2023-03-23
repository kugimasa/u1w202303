using UnityEngine;
using UnityEngine.UI;

namespace Script.View
{
    public class NoteView : MonoBehaviour
    {
        [SerializeField] private Slider _noteSliderR;
        [SerializeField] private Slider _noteSliderL;

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
        }
    }
}