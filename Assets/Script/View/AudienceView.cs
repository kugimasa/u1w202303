using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Script.View
{
    public class AudienceView : MonoBehaviour
    {
        [SerializeField] private Image _audience;

        private void Awake()
        {
            _audience.DOFade(0.0f, 0.0f).SetLink(gameObject);
        }

        public void AudienceFadeIn(float delay)
        {
            _audience.DOFade(1.0f, delay).SetLink(gameObject);
        }

        public void AudienceFadeOut(float delay)
        {
            _audience.DOFade(0.0f, delay).SetLink(gameObject);
        }
    }
}