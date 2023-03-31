using DG.Tweening;
using naichilab.EasySoundPlayer.Scripts;
using Script.Data;
using Script.Model;
using Script.Util;
using Script.View;
using UnityEngine;

namespace Script.Presenter
{
    public class EvaluatePresenter : MonoBehaviour
    {
        [SerializeField] private EvaluateModel _evaluateModel;
        [SerializeField] private GaugeSliderView _gaugeSliderView;
        [SerializeField] private OpponentView _opponentView;
        [SerializeField] private RhymeView _challengerView;
        [SerializeField] private TitleView _titleView;
        [SerializeField] private TimelineManager _timelineManager;
        [SerializeField] private FadeView _fadeView;

        /// <summary>
        /// 点数の可視化
        /// </summary>
        public void OnEvaluateChallenger()
        {
            _gaugeSliderView.AnimateChallengerEvaluate(_evaluateModel.Score01);
        }

        /// <summary>
        /// 点数の可視化
        /// </summary>
        public void OnEvaluateOpponent()
        {
            _gaugeSliderView.AnimateOpponentEvaluate(_evaluateModel.GetOpponentScore01(_opponentView.OpponentId));
        }

        /// <summary>
        /// クリア判定
        /// </summary>
        public void OnEvaluateWinner()
        {
            if (_evaluateModel.EvaluateWinner(_opponentView.OpponentId))
            {
                // チャレンジャー勝利のSE
                SePlayer.Instance.Play("winner_challenger");
                DOVirtual.DelayedCall(3.0f, () =>
                {
                    SePlayer.Instance.Play("siren");
                }).OnComplete(() =>
                {
                    // フェードイン
                    _fadeView.FadeIn(2.0f);
                    _challengerView.ChallengerExit();
                    _opponentView.OpponentExit();
                    // 評価スライダーの非表示
                    _gaugeSliderView.HideEvaluateSlider();
                    switch (_opponentView.OpponentId)
                    {
                        case 0:
                            // 毛布ハムマ戦へ
                            _timelineManager.PlayBattle(1);
                            // Timelineを切り替えた後にフラグを立てる
                            PlayerPrefs.SetString(StaticConst.BATTLE1_CLEAR_KEY, "HasKey");
                            PlayerPrefs.Save();
                            break;
                        case 1:
                            // 星 a.k.a HAMU戦へ
                            _timelineManager.PlayBattle(2);
                            // Timelineを切り替えた後にフラグを立てる
                            PlayerPrefs.SetString(StaticConst.BATTLE2_CLEAR_KEY, "HasKey");
                            PlayerPrefs.Save();
                            break;
                        case 2:
                            // エンディングを再生
                            _timelineManager.PlayEnding();
                            break;
                    }
                }).SetLink(gameObject);
            }
            else
            {
                // 相手の勝利
                string opponentWinSe = "";
                var delayToSiren = 3.0f;
                switch (_opponentView.OpponentId)
                {
                    case 0:
                        opponentWinSe = "winner_kotaro";
                        break;
                    case 1:
                        opponentWinSe = "winner_mofu_hamuma";
                        break;
                    case 2:
                        opponentWinSe = "winner_sei_aka_hamu";
                        delayToSiren = 4.0f;
                        break;
                    default:
                        opponentWinSe = "winner_kotaro";
                        break;
                }
                SePlayer.Instance.Play(opponentWinSe);
                DOVirtual.DelayedCall(delayToSiren, () =>
                {
                    SePlayer.Instance.Play("siren");
                    // タイトルに遷移するボタンを表示
                    _titleView.ShowReturnToTitleButton();
                    // 以降はDOTween処理をさせない
                }).SetLink(gameObject);
            }
        }
    }
}