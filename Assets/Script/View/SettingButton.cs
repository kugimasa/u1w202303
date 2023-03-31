using System;
using DG.Tweening;
using UnityEngine;

namespace Script.View
{
    public class SettingButton : MonoBehaviour
    {
        [SerializeField] private RectTransform _settingPanel;
        [SerializeField] private CanvasGroup _canvasGroup;

        private void Awake()
        {
            _canvasGroup.alpha = 0.0f;
            _canvasGroup.interactable = false;
            _canvasGroup.blocksRaycasts = false;
            _settingPanel.DOLocalMoveX(-1220.0f, 0.0f).SetLink(gameObject);
        }

        
        public void OnOpen()
        {
            _canvasGroup.DOFade(1.0f, 1.0f)
                .SetEase(Ease.OutCubic)
                .OnComplete(() =>
                {
                    _canvasGroup.interactable = true;
                    _canvasGroup.blocksRaycasts = true;
                })
                .SetLink(gameObject);
            _settingPanel.DOLocalMoveX(-700.0f, 0.6f).SetEase(Ease.OutCubic).SetLink(gameObject);
        }
        
        public void OnClose()
        {
            _canvasGroup.DOFade(0.0f, 2.0f)
                .SetEase(Ease.OutCubic)
                .OnComplete(() =>
                {
                    _canvasGroup.interactable = false;
                    _canvasGroup.blocksRaycasts = false;
                })
                .SetLink(gameObject);
            _settingPanel.DOLocalMoveX(-1220.0f, 0.6f).SetEase(Ease.OutCubic).SetLink(gameObject);
        }
    }
}