using UnityEngine;
using UnityEngine.UI;

namespace Script.View
{
    public class NoteView : MonoBehaviour
    {
        [SerializeField] private Slider _noteSliderR;
        [SerializeField] private Slider _noteSliderL;

        /// <summary>
        ///     スライダー位置のセット
        /// </summary>
        /// <param name="t"></param>
        public void SetSliderValue(float t)
        {
            _noteSliderR.value = Mathf.Clamp01(t);
            _noteSliderL.value = Mathf.Clamp01(t);
        }
    }
}