using DG.Tweening;
using naichilab.EasySoundPlayer.Scripts;
using Script.Data;
using Script.Model;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Script.View
{
    public class RhymeView : MonoBehaviour
    {
        [SerializeField] private Image _noteImage;
        [SerializeField] private Image _myRhymeImage;
        [SerializeField] private Image _playerImage;
        [SerializeField] private Sprite _playerSprite;
        [SerializeField] private Sprite _playerSprite2;
        [SerializeField] private CanvasGroup _challengerPanel;
        [SerializeField] private CanvasGroup _rhymePanel;
        [SerializeField] private CanvasGroup _myRhyme;
        [SerializeField] private TextMeshProUGUI _rhymeLabel;
        [SerializeField] private EvaluateModel _evaluateModel;
        [SerializeField] private NoteView _noteView;
        private Sequence _noteImagesSequence;
        private Sequence _playerImageSequence;
        private Sequence _rhymeSequence;
        
        private void Awake()
        {
            // 初期状態では透明
            _challengerPanel.DOFade(0.0f, 0.0f).SetLink(gameObject);
            _rhymePanel.DOFade(0.0f, 0.0f).SetLink(gameObject);
            _myRhyme.DOFade(0.0f, 0.0f).SetLink(gameObject);
            _playerImage.sprite = _playerSprite;
        }

        /// <summary>
        ///     ノーツUIの中心のスプライト演出
        /// </summary>
        private void NoteImageAnimation()
        {
            _noteImagesSequence?.Kill();
            var scaleIn = _noteImage.rectTransform.DOScale(1.25f, 0.6666f).SetEase(Ease.OutElastic);
            _noteImagesSequence = DOTween.Sequence()
                .OnStart(() => _noteImage.rectTransform.localScale = Vector3.one)
                .Append(scaleIn)
                .SetLink(gameObject);
            _noteImagesSequence.Play();
        }

        /// <summary>
        ///     入力があった場合の表示
        /// </summary>
        public void OnRhymeSpit(RhymeData rhymeData)
        {
            // 画像切り替え
            _playerImageSequence?.Kill();
            _playerImageSequence = DOTween.Sequence()
                .OnStart(() => _playerImage.sprite = _playerSprite)
                .Append(DOVirtual.DelayedCall(0.1f, () => _playerImage.sprite = _playerSprite2))
                .SetLoops(2, LoopType.Restart)
                .OnComplete(() => _playerImage.sprite = _playerSprite)
                .SetLink(gameObject);
            _playerImageSequence.Play();
            // SE再生
            SePlayer.Instance.Play(rhymeData.Clip.name);
            // ライムテキスト表示
            _rhymeLabel.text = rhymeData.Text;
            _rhymeSequence?.Kill();
            _rhymeSequence = DOTween.Sequence()
                .OnStart(() =>_myRhymeImage.rectTransform.localScale = Vector3.one)
                .Append(_myRhyme.DOFade(1.0f, 0.0f))
                .Append(_myRhymeImage.rectTransform.DOScale(1.2f, 1.0f).SetEase(Ease.OutElastic))
                .Insert(0.75f, _myRhyme.DOFade(0.0f, 0.2f))
                .SetLink(gameObject);
            _rhymeSequence.Play();
            // ノート演出
            NoteImageAnimation();
        }

        /// <summary>
        ///     タイミングよく入力できた際の表示
        /// </summary>
        public void OnJustTiming()
        {
            // 演出
            SePlayer.Instance.Play("audience");
            // TODO: コンボUI
            // スコア
            Debug.Log($"<color=green>Nice Timing!!</color>");
        }

        /// <summary>
        /// チャレンジャーの表示演出
        /// </summary>
        public void ShowChallengerPanel()
        {
            _challengerPanel.DOFade(1.0f, 2.0f).SetEase(Ease.OutCubic).SetLink(gameObject);
        }

        public void OnMyTurnStart()
        {
            // 表示するたびにスライダーの初期化をする
            // TODO: このタイミングではセットされていない
            // FIXME: PrecisionはEvaluateModelでセットしたほうが良いかも...
            _noteView.InitNoteSlider(_evaluateModel.Precision);
            DOVirtual.Float(0.0f, 1.0f, 0.6f, value => _rhymePanel.alpha = value)
                .SetEase(Ease.InOutCubic)
                .SetLink(gameObject);
        }
        
        public void OnMyTurnEnd()
        {
            
            // フェードアウト
            DOVirtual.Float(1.0f, 0.0f, 0.6f, value => _rhymePanel.alpha = value)
                .SetEase(Ease.InOutCubic)
                .SetLink(gameObject);
        }
    }
}