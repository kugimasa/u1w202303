using DG.Tweening;
using Script.Data;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Script.View
{
    public class OpponentView : MonoBehaviour
    {
        [SerializeField] private Sprite[] _opponentSprites = new Sprite[StaticConst.OPPONENT_NUM];
        [SerializeField] private Sprite[] _opponentSprites2 = new Sprite[StaticConst.OPPONENT_NUM];
        [SerializeField] private string[] _opponentNames = new string[StaticConst.OPPONENT_NUM];
        [SerializeField] private Image _rhymeImage;
        [SerializeField] private Image _opponentImage;
        [SerializeField] private TextMeshProUGUI _opponentNameLabel;
        [SerializeField] private TextMeshProUGUI _rhymeLabel;
        [SerializeField] private CanvasGroup _opponentPanel;
        [SerializeField] private CanvasGroup _opponentRhyme;
        [SerializeField] private AudioSource _rhymeSpitSource;
        private int _opponentId;
        private Sequence _rhymeSequence;
        private Sequence _opponentImageSequence;

        // FIXME: 本来であれば、OpponentPresenterを用意するべきだった
        // だがしかし、間に合わないのでええいままよ！！
        public int OpponentId => _opponentId;

        private void Awake()
        {
            // 初期状態では透明
            _opponentPanel.DOFade(0.0f, 0.0f).SetLink(gameObject);
            _opponentImage.DOFade(0.0f, 0.0f).SetLink(gameObject);
            _opponentRhyme.DOFade(0.0f, 0.0f).SetLink(gameObject);
        }

        /// <summary>
        /// 相手が入れ替わる
        /// </summary>
        /// <param name="id"></param>
        public void ChangeOpponent(int id)
        {
            _opponentId = id;
            _opponentImage.sprite = _opponentSprites[id];
            _opponentNameLabel.text = _opponentNames[id];
        }

        /// <summary>
        /// 相手が入場
        /// </summary>
        public void OpponentEnter()
        {
            _opponentPanel.DOFade(1.0f, 2.0f).SetEase(Ease.OutCubic).SetLink(gameObject);
            _opponentImage.DOFade(1.0f, 2.0f).SetEase(Ease.OutCubic).SetLink(gameObject);
        }

        /// <summary>
        /// 相手が退場
        /// </summary>
        public void OpponentExit()
        {
            _opponentPanel.DOFade(0.0f, 2.0f).SetEase(Ease.OutCubic).SetLink(gameObject);
            _opponentImage.DOFade(0.0f, 2.0f).SetEase(Ease.OutCubic).SetLink(gameObject);
        }

        public void RhymeSpit(string rhymeStr)
        {
            // 画像切り替え
            _opponentImageSequence?.Kill();
            _opponentImageSequence = DOTween.Sequence()
                .OnStart(() => _opponentImage.sprite = _opponentSprites[_opponentId])
                .Append(DOVirtual.DelayedCall(0.1f, () => _opponentImage.sprite = _opponentSprites2[_opponentId]))
                .SetLoops(2, LoopType.Restart)
                .OnComplete(() => _opponentImage.sprite = _opponentSprites[_opponentId])
                .SetLink(gameObject);
            _opponentImageSequence.Play();
            // ライムテキスト表示
            _rhymeLabel.text = rhymeStr;
            _rhymeSequence?.Kill();
            _rhymeSequence = DOTween.Sequence()
                .OnStart(() => _rhymeImage.rectTransform.localScale = Vector3.one)
                .Append(_opponentRhyme.DOFade(1.0f, 0.0f))
                .Append(_rhymeImage.rectTransform.DOScale(1.2f, 1.5f).SetEase(Ease.OutElastic))
                .Insert(1.25f, _opponentRhyme.DOFade(0.0f, 0.2f))
                .SetLink(gameObject);
            _rhymeSequence.Play();
        }

        /// <summary>
        /// ライムをSEと一緒にスピット
        /// </summary>
        public void RhymeSpitWithAudio(RhymeData rhymeData)
        {
            // SE再生
            var se = rhymeData.Clip;
            _rhymeSpitSource.PlayOneShot(se);
            RhymeSpit(rhymeData.Text);
        }
    }
}