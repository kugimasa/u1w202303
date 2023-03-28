using System;
using DG.Tweening;
using Script.Data;
using UnityEngine;
using UnityEngine.UI;

namespace Script.View
{
    public class TitleView : MonoBehaviour
    {
        [SerializeField] private CanvasGroup _canvasGroup;
        [SerializeField] private CanvasGroup _buttonGroup;
        [SerializeField] private Image _titleImage;
        [SerializeField] private FadeView _fadeView;

        private Sequence _bounceSequence;

        private void Awake()
        {
            // 初期化時は見えない
            _buttonGroup.alpha = 0.0f;
            _buttonGroup.interactable = false;
            _canvasGroup.alpha = 1.0f;
            _canvasGroup.interactable = false;
        }

        /// <summary>
        ///  初回プレイ時の演出
        /// </summary>
        private Sequence OnFirstPlaySequence()
        {
            var titleBgmBpm = 110.0f;
            // 1小節あたりにかける時間
            var secPerBar = StaticConst.MIN_AS_SEC * StaticConst.BEAT_NUM / titleBgmBpm;
            var titleFadeInDuration = secPerBar * 4.0f;
            // タイトル表示登場
            var sequence = DOTween.Sequence()
                .OnStart(() =>
                {
                    // 初期値
                    _titleImage.rectTransform.localScale = Vector3.one * 2.0f;
                    // ボタンは見えない
                    _buttonGroup.alpha = 0.0f;
                })
                // タイトル縮小開始
                .Append(_titleImage.rectTransform.DOScale(Vector3.one, titleFadeInDuration))
                // フェードアウト開始
                .Join(_fadeView.FadeOutTween(titleFadeInDuration))
                // ボタン表示
                .Append(_buttonGroup.DOFade(1.0f, 0.6f).SetEase(Ease.OutCubic))
                .Append(DOVirtual.DelayedCall(0.0f, () =>
                {
                    _buttonGroup.interactable = true;
                    _canvasGroup.interactable = true;
                }))
                .SetLink(gameObject);
            return sequence;
        }

        /// <summary>
        /// 2回目以降のプレイ
        /// </summary>
        /// <returns></returns>
        private Sequence OnSecondPlaySequence()
        {
            // タイトルの初期化演出
            var sequence = DOTween.Sequence()
                // フェードアウト開始
                .Join(_fadeView.FadeOutTween(0.6f))
                // ボタン表示
                .Append(_buttonGroup.DOFade(1.0f, 0.6f).SetEase(Ease.OutCubic))
                .Append(DOVirtual.DelayedCall(0.0f, () =>
                {
                    _buttonGroup.interactable = true;
                    _canvasGroup.interactable = true;
                }))
                .SetLink(gameObject);
            return sequence;
        }

        /// <summary>
        ///     タイトル初期化演出
        /// </summary>
        public void TitleInitialize(bool isFirstPlay)
        {
            var titleBgmBpm = 110.0f;
            // 1小節あたりにかける時間
            var secPerBar = StaticConst.MIN_AS_SEC * StaticConst.BEAT_NUM / titleBgmBpm;
            // 初回プレイシーケンスの作成
            var sequence = DOTween.Sequence();
            if (isFirstPlay)
            {
                sequence.Append(OnFirstPlaySequence());
            }
            else
            {
                sequence.Append(OnSecondPlaySequence());
            }
            // バウンスループシーケンス
            _bounceSequence = DOTween.Sequence()
                .Append(
                    DOVirtual.Vector3(Vector3.one * 1.05f, Vector3.one, secPerBar,
                            value => _titleImage.rectTransform.localScale = value)
                        .SetLoops(2, LoopType.Restart)
                        .SetEase(Ease.OutElastic))
                // FIXME: 便宜上
                .SetLoops(Int32.MaxValue, LoopType.Restart)
                .SetLink(gameObject);
            // 結合する
            sequence.Append(_bounceSequence);
            sequence.Play();
        }

        /// <summary>
        ///     ゲーム開始入力
        /// </summary>
        public void TitleFadeOut()
        {
            // 念の為、KILL
            _bounceSequence?.Kill();
            // タイトルのアルファをゼロに
            DOVirtual.Float(1.0f, 0.0f, 2.0f, value => _canvasGroup.alpha = value)
                .SetEase(Ease.OutCirc)
                .SetLink(gameObject);
            // ボタンは押せないようにする
            _canvasGroup.interactable = false;
        }
        
        // TODO: タイトルへのフェードイン
    }
}