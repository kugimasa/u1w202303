using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Script.View
{
    public class FadeView : MonoBehaviour
    {
        [SerializeField] private Image _fadePanel;
        
        private void Awake()
        {
            // 暗転開始
            _fadePanel.DOFade(1.0f, 0.0f);
        }
        
        /// <summary>
        /// 暗転
        /// </summary>
        public Tween FadeInTween(float duration)
        {
            return _fadePanel.DOFade(1.0f, duration).SetEase(Ease.OutCubic);
        }

        /// <summary>
        /// 明転
        /// </summary>
        public Tween FadeOutTween(float duration)
        {
            return _fadePanel.DOFade(0.0f, duration).SetEase(Ease.OutCubic);
        }
        
        /// <summary>
        /// 暗転
        /// </summary>
        public void FadeIn(float duration)
        {
            _fadePanel.DOFade(1.0f, duration).SetEase(Ease.OutCubic);
        }

        /// <summary>
        /// 明転
        /// </summary>
        public void FadeOut(float duration)
        {
            _fadePanel.DOFade(0.0f, duration).SetEase(Ease.OutCubic);
        }
    }
}