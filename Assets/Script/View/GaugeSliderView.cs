using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Script.View
{
    public class GaugeSliderView : MonoBehaviour
    {
        [SerializeField] private CanvasGroup _challengerSliderGroup;
        [SerializeField] private CanvasGroup _opponentSliderGroup;
        [SerializeField] private CanvasGroup _evaluateChallengerSliderGroup;
        [SerializeField] private CanvasGroup _evaluateOpponentSliderGroup;
        [SerializeField] private Slider _challengerSlider;
        [SerializeField] private Slider _opponentSlider;
        [SerializeField] private Slider _evaluateChallengerSlider;
        [SerializeField] private Slider _evaluateOpponentSlider;

        private void Awake()
        {
            _challengerSliderGroup.interactable = false;
            _opponentSliderGroup.interactable = false;
            _challengerSliderGroup.alpha = 0.0f;
            _opponentSliderGroup.alpha = 0.0f;
            _evaluateChallengerSliderGroup.alpha = 0.0f;
            _evaluateOpponentSliderGroup.alpha = 0.0f;
            _challengerSlider.value = 0.0f;
            _opponentSlider.value = 0.0f;
            _evaluateChallengerSlider.value = 0.0f;
            _evaluateOpponentSlider.value = 0.0f;
        }

        public void ShowChallengerGauge()
        {
            _challengerSliderGroup.DOFade(1.0f, 1.0f).SetEase(Ease.OutCubic).SetLink(gameObject);
            _challengerSlider.value = 0.0f;
        }

        public void ShowOpponentGauge()
        {
            _opponentSliderGroup.DOFade(1.0f, 1.0f).SetEase(Ease.OutCubic).SetLink(gameObject);
            _opponentSlider.value = 0.0f;
        }

        public void HideSliderGauges()
        {
            _challengerSliderGroup.DOFade(0.0f, 1.0f).SetEase(Ease.OutCubic).SetLink(gameObject);
            _opponentSliderGroup.DOFade(0.0f, 1.0f).SetEase(Ease.OutCubic).SetLink(gameObject);
        }

        public void SetChallengerSliderGauge(float value)
        {
            _challengerSlider.value = value;
        }
        
        public void SetOpponentSliderGauge()
        {
            _opponentSlider.value += 0.05f;
        }

        public void AnimateChallengerEvaluate(float endValue)
        {
            _evaluateChallengerSliderGroup.DOFade(1.0f, 1.0f).SetEase(Ease.OutCubic).SetLink(gameObject);
            DOVirtual.Float(0.0f, endValue, 2.0f, value => _evaluateChallengerSlider.value = value)
                .SetLink(gameObject);
        }
        
        public void AnimateOpponentEvaluate(float endValue)
        {
            _evaluateOpponentSliderGroup.DOFade(1.0f, 1.0f).SetEase(Ease.OutCubic).SetLink(gameObject);
            DOVirtual.Float(0.0f, endValue, 2.0f, value => _evaluateOpponentSlider.value = value)
                .SetLink(gameObject);
        }

        public void HideEvaluateSlider()
        {
            _evaluateChallengerSliderGroup.DOFade(0.0f, 1.0f).SetEase(Ease.OutCubic).SetLink(gameObject);
            _evaluateOpponentSliderGroup.DOFade(0.0f, 1.0f).SetEase(Ease.OutCubic).SetLink(gameObject);
        }
    }
}