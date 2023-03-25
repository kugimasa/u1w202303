using DG.Tweening;
using naichilab.EasySoundPlayer.Scripts;
using Script.Data;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Script.View
{
    public class OpponentView : MonoBehaviour
    {
        [SerializeField] private Sprite[] _opponents = new Sprite[StaticConst.OPPONENT_NUM];
        [SerializeField] private Image _rhymeImage;
        [SerializeField] private Image _opponentImage;
        [SerializeField] private TextMeshProUGUI _rhymeLabel;
        [SerializeField] private CanvasGroup _canvasGroup;
        [SerializeField] private AudioSource _rhymeSpitSource;

        private void Awake()
        {
            // 初期状態では透明
            _canvasGroup.DOFade(0.0f, 0.0f).SetLink(gameObject);
        }

        /// <summary>
        /// 相手が入れ替わる
        /// </summary>
        /// <param name="id"></param>
        public void ChangeOpponent(int id)
        {
            _opponentImage.sprite = _opponents[id];
        }

        /// <summary>
        /// 相手が入場
        /// </summary>
        public void OpponentEnter()
        {
            _opponentImage.DOFade(1.0f, 1.5f).SetEase(Ease.InCirc).SetLink(gameObject);
        }

        /// <summary>
        /// 相手が退場
        /// </summary>
        public void OpponentExit()
        {
            _opponentImage.DOFade(0.0f, 1.5f).SetEase(Ease.InCirc).SetLink(gameObject);
        }

        /// <summary>
        /// ライムをスピット
        /// </summary>
        public void RhymeSpit(RhymeData rhymeData)
        {
            // SE再生
            var se = rhymeData.Clip;
            _rhymeSpitSource.PlayOneShot(se);
            // ライムテキスト表示
            _rhymeLabel.text = rhymeData.Text;
            DOTween.Sequence()
                .OnStart(() =>
                {
                    _canvasGroup.DOFade(1.0f, 0.0f).SetLink(gameObject);
                    _rhymeImage.rectTransform.localScale = Vector3.one;
                })
                .Append(_rhymeImage.rectTransform.DOScale(1.2f, 2.0f).SetEase(Ease.OutElastic))
                .OnComplete(() =>
                {
                    _canvasGroup.DOFade(0.0f, 0.0f).SetLink(gameObject);
                })
                .SetLink(gameObject);
        }
    }
}