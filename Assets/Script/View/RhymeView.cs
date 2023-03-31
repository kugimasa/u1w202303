using DG.Tweening;
using naichilab.EasySoundPlayer.Scripts;
using Script.Data;
using Script.Model;
using Script.Util;
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
        [SerializeField] private SoundManager _soundManager;
        [SerializeField] private RectTransform[] _comboText = new RectTransform[StaticConst.BEAT_NUM];
        private Sequence _noteImagesSequence;
        private Sequence _playerImageSequence;
        private Sequence _rhymeSequence;

        private void Awake()
        {
            // 初期状態では透明
            _challengerPanel.DOFade(0.0f, 0.0f).SetLink(gameObject);
            _rhymePanel.DOFade(0.0f, 0.0f).SetLink(gameObject);
            _rhymePanel.interactable = false;
            _myRhyme.DOFade(0.0f, 0.0f).SetLink(gameObject);
            _playerImage.sprite = _playerSprite;
            foreach (var comboText in _comboText)
            {
                comboText.GetComponent<TextMeshProUGUI>().alpha = 0.0f;
            }
        }

        private void PlayComboSequence(int index)
        {
            var comboSequence = DOTween.Sequence()
                .OnStart(() => _comboText[index].GetComponent<TextMeshProUGUI>().alpha = 1.0f)
                .SetLoops(4, LoopType.Yoyo)
                .Append(_comboText[index].DOScale(1.3f, 0.3f))
                .Join(_comboText[index].DOScale(2.0f, 0.3f))
                .Join(_comboText[index].DOLocalRotate(new Vector3(0.0f, 0.0f, 10.0f), 0.3f))
                .OnComplete(() => _comboText[index].GetComponent<TextMeshProUGUI>().alpha = 0.0f)
                .SetLink(gameObject);
            comboSequence.Play();
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
            _soundManager.PlaySe(rhymeData.Clip);
            // SePlayerによる再生を止める
            // SePlayer.Instance.Play(rhymeData.Clip.name);
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
        public void OnJustTiming(int combo)
        {
            // 演出
            for (int i = 0; i < combo; i++)
            {
                PlayComboSequence(i);
                switch (i)
                {
                    case 0:
                        SePlayer.Instance.Play("ei");
                        break;
                    case 1:
                        SePlayer.Instance.Play("yeah");
                        break;
                    case 2:
                        SePlayer.Instance.Play("wayyo");
                        break;
                    case 3:
                        SePlayer.Instance.Play("audience");
                        break;
                }
            }
        }

        /// <summary>
        /// チャレンジャーの表示演出
        /// </summary>
        public void ChallengerEnter()
        {
            _challengerPanel.DOFade(1.0f, 2.0f).SetEase(Ease.OutCubic).SetLink(gameObject);
        }

        /// <summary>
        /// チャレンジャー退場
        /// </summary>
        public void ChallengerExit()
        {
            _challengerPanel.DOFade(0.0f, 2.0f).SetEase(Ease.OutCubic).SetLink(gameObject);
        }

        public void OnMyTurnStart()
        {
            // 表示するたびにスライダーの初期化をする
            // FIXME: PrecisionはEvaluateModelでセットしたほうが良いかも...
            _noteView.InitNoteSlider(_evaluateModel.Precision);
            // ボタン入力可能
            _rhymePanel.interactable = true;
            DOVirtual.Float(0.0f, 1.0f, 0.6f, value => _rhymePanel.alpha = value)
                .SetEase(Ease.InOutCubic)
                .SetLink(gameObject);
        }
        
        public void OnMyTurnEnd()
        {
            // ボタン入力不可
            _rhymePanel.interactable = false;
            // フェードアウト
            DOVirtual.Float(1.0f, 0.0f, 0.6f, value => _rhymePanel.alpha = value)
                .SetEase(Ease.InOutCubic)
                .SetLink(gameObject);
        }
    }
}