using System;
using DG.Tweening;
using Script.Data;
using TMPro;
using UnityEngine;

namespace Script.View
{
    public class NarratorView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI[] _lines = new TextMeshProUGUI[StaticConst.INTRO_NARRATOR_LINE_NUM];
        [SerializeField] private float _lineShowDuration = 2.0f;

        private void Awake()
        {
            // 最初は非表示
            foreach (var line in _lines)
            {
                line.DOFade(0.0f, 0.0f).SetLink(gameObject);
            }
        }

        /// <summary>
        /// イントロナレーションテキストの表示
        /// </summary>
        public void ShowLine(int index)
        {
            if (index >= StaticConst.INTRO_NARRATOR_LINE_NUM) return;
            _lines[index].DOFade(1.0f, _lineShowDuration).SetEase(Ease.OutCubic).SetLink(gameObject);
        }

        /// <summary>
        /// ナレーションテキストを非表示
        /// </summary>
        public void FadeOutLines()
        {
            _lines[0].DOFade(0.0f, _lineShowDuration).SetEase(Ease.OutCubic).SetLink(gameObject);
            _lines[1].DOFade(0.0f, _lineShowDuration).SetEase(Ease.OutCubic).SetLink(gameObject);
            _lines[2].DOFade(0.0f, _lineShowDuration).SetEase(Ease.OutCubic).SetLink(gameObject);
        }
        
        /// <summary>
        /// 最後のナレーションテキストを非表示
        /// </summary>
        public void FadeOutLastLine()
        {
            _lines[3].DOFade(0.0f, _lineShowDuration).SetEase(Ease.OutCubic).SetLink(gameObject);
        }
    }
}