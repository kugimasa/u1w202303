using System;
using Script.Data;
using UnityEngine;
using UnityEngine.UI;

namespace Script.View
{
    public class NoteView : MonoBehaviour
    {
        [SerializeField] private Slider _noteSliderR;
        [SerializeField] private Slider _noteSliderL;
        [SerializeField] private Image _noteFillR;
        [SerializeField] private Image _noteFillL;
        [SerializeField] private Color[] _rhymeTypeColors = new Color[StaticConst.INPUT_NUM];

        private void Awake()
        {
            // 初期化
            _noteSliderR.value = 1.0f;
            _noteSliderL.value = 1.0f;
        }

        /// <summary>
        ///     tの値に応じて演出させる
        /// </summary>
        /// <param name="rhymeType"></param>
        /// <param name="t"></param>
        public void UpdateView(RhymeType rhymeType, float t)
        {
            SetRhymeColor(rhymeType);
            SetSliderValue(t);
        }
        
        private void SetRhymeColor(RhymeType rhymeType)
        {
            _noteFillR.color = _rhymeTypeColors[(int)rhymeType];
            _noteFillL.color = _rhymeTypeColors[(int)rhymeType];
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