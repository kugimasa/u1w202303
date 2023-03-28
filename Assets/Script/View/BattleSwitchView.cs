using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Script.View
{
    public class BattleSwitchView : MonoBehaviour
    {
        [SerializeField] private CanvasGroup _battle1StartButton;

        private void Awake()
        {
            _battle1StartButton.interactable = false;
            _battle1StartButton.DOFade(0.0f, 0.0f).SetLink(gameObject);
        }

        public void ShowBattle1()
        {
            _battle1StartButton.DOFade(1.0f, 2.0f)
                .OnComplete(() => _battle1StartButton.interactable = true)
                .SetLink(gameObject);
        }
        
        public void HideBattle1()
        {
            _battle1StartButton.interactable = false;
            _battle1StartButton.DOFade(0.0f, 2.0f).SetLink(gameObject);
        }
    }
}