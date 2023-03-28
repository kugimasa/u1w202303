using System;
using DG.Tweening;
using Script.Data;
using UnityEngine;

namespace Script.View
{
    public class BeatCheckView : MonoBehaviour
    {
        [SerializeField] private CanvasGroup _beatCheckPanel;
        [SerializeField] private RectTransform _beatCheckRect;
        [SerializeField] private RectTransform _hamHandleRect;
        [SerializeField] private RectTransform _starHandleRect;
        [SerializeField] private RectTransform _hamDiskRect;
        [SerializeField] private RectTransform _starDiskRect;
        [SerializeField] private AudioSource _audioSource;
        [SerializeField] private AudioClip[] _hamBeats = new AudioClip[StaticConst.OPPONENT_NUM];
        [SerializeField] private AudioClip[] _starBeats = new AudioClip[StaticConst.OPPONENT_NUM];

        private Sequence _hamDiskSequence;
        private Sequence _starDiskSequence;

        private void Awake()
        {
            _beatCheckPanel.alpha = 0.0f;
            _beatCheckPanel.interactable = false;
            _beatCheckRect.DOLocalMoveY(-1080, 0.0f).SetLink(gameObject);
            _hamHandleRect.DOLocalRotate(new Vector3(0, 0, 20), 0.0f).SetLink(gameObject);
            _starHandleRect.DOLocalRotate(new Vector3(0, 0, 20), 0.0f).SetLink(gameObject);
        }

        public void ShowBeatCheckView(int id)
        {
            _beatCheckPanel.DOFade(1.0f, 1.0f).SetLink(gameObject);
            _beatCheckPanel.interactable = true;
            _beatCheckRect.DOLocalMoveY(0.0f, 1.0f).SetLink(gameObject);
            DOVirtual.DelayedCall(1.0f,() =>
            {
                // HamBeatを必ず最初に再生
                PlayHamBeat(id);
            }).SetLink(gameObject);
        }

        public void HideBeatCheckView()
        {
            // 再生を止める
            _audioSource.Stop();
            
            _beatCheckPanel.DOFade(0.0f, 1.0f).SetLink(gameObject);
            _beatCheckPanel.interactable = false;
            _beatCheckRect.DOLocalMoveY(-1080, 1.0f).SetLink(gameObject);
            // ハンドルの移動
            _hamHandleRect.DOLocalRotate(new Vector3(0, 0, 20), 0.6f).SetLink(gameObject);
            _starHandleRect.DOLocalRotate(new Vector3(0, 0, 20), 0.6f).SetLink(gameObject);
            _hamDiskSequence?.Kill();
            _starDiskSequence?.Kill();
        }
        
        public void PlayHamBeat(int id)
        {
            // 直前の再生を止める
            _audioSource.Stop();
            _audioSource.time = 0;
            _audioSource.loop = true;
            // ハンドルの移動
            _hamHandleRect.DOLocalRotate(new Vector3(0, 0, 0), 0.6f).SetLink(gameObject);
            _starHandleRect.DOLocalRotate(new Vector3(0, 0, 20), 0.6f).SetLink(gameObject);
            // Diskの回転
            _hamDiskSequence?.Kill();
            _starDiskSequence?.Kill();
            _hamDiskSequence = DOTween.Sequence()
                .Append(_hamDiskRect.DOLocalRotate(new Vector3(0, 0, -360), 3, RotateMode.FastBeyond360).SetEase(Ease.Linear))
                .SetLoops(int.MaxValue)
                .SetLink(gameObject);
            _hamDiskSequence.Play();
            _audioSource.clip = _hamBeats[id];
            _audioSource.time = 0;
            _audioSource.loop = true;
            _audioSource.Play();
        }
        
        public void PlayStarBeat(int id)
        {
            // 直前の再生を止める
            _audioSource.Stop();
            // ハンドルの移動
            _hamHandleRect.DOLocalRotate(new Vector3(0, 0, 20), 0.6f).SetLink(gameObject);
            _starHandleRect.DOLocalRotate(new Vector3(0, 0, 0), 0.6f).SetLink(gameObject);
            // Diskの回転
            _hamDiskSequence?.Kill();
            _starDiskSequence?.Kill();
            _starDiskSequence = DOTween.Sequence()
                .Append(_starDiskRect.DOLocalRotate(new Vector3(0, 0, -360), 3, RotateMode.FastBeyond360).SetEase(Ease.Linear))
                .SetLoops(int.MaxValue)
                .SetLink(gameObject);
            _starDiskSequence.Play();
            _audioSource.clip = _starBeats[id];
            _audioSource.time = 0;
            _audioSource.loop = true;
            _audioSource.Play();
        }
    }
}