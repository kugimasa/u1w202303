using System.Collections.Generic;
using System.Linq;
using Script.Data;
using Script.Model;
using Script.View;
using UnityEngine;

namespace Script.Presenter
{
    public class InputPresenter : MonoBehaviour
    {
        [SerializeField] private RhymeInputModel[] _rhymeInputs = new RhymeInputModel[StaticConst.INPUT_NUM];
        [SerializeField] private EvaluateModel _evaluateModel;
        [SerializeField] private RhymeView _rhymeView;
        [SerializeField] private RhymeInputView[] _rhymeInputViews = new RhymeInputView[StaticConst.INPUT_NUM];
        [SerializeField, Range(0, 3.0f)] private float _delay;
        [SerializeField] private GaugeSliderView _gaugeSliderView;
        [SerializeField] private RhymeDataModel _rhymeDataModel;
        // FIXME: こんなことしちゃだめだよ(戒め)
        [SerializeField] private OpponentView _opponentView;
        private RhymeDataSet _currentSet;
        private List<RhymeDataSet> _currentRhymeDataSets;
        private bool _isMyTurn;

        void Update()
        {
            if (!_isMyTurn) return;
            for (var index = 0; index < StaticConst.INPUT_NUM; index++)
            {
                var rhymeInput = _rhymeInputs[index];
                if (Input.GetKeyDown(rhymeInput.KeyCode) || rhymeInput.IsButtonClicked)
                {
                    rhymeInput.IsButtonClicked = false;
                    // ライムをスピット
                    if (rhymeInput.TryRhymeSpit(_delay))
                    {
                        var isJustTiming = _evaluateModel.EvaluateT(out int comboNum);
                        // ライムタイプ判定
                        if (_evaluateModel.EvaluateRhymeType(rhymeInput.RhymeType))
                        {
                            // タイミング判定処理
                            if (isJustTiming)
                            {
                                _rhymeView.OnJustTiming(comboNum);
                                // ゲージの更新
                                _gaugeSliderView.SetChallengerSliderGauge(_evaluateModel.Score01);
                            }
                        }
                        // 表示 & 音声処理
                        _rhymeInputViews[index].OnInput();
                        _rhymeView.OnRhymeSpit(_currentSet.RhymeDataArray[index]);
                        // 同時押しはNG
                        return;
                    }
                }
            }
        }

        /// <summary>
        /// ライムセットを更新する
        /// </summary>
        public void UpdateRhymeData()
        {
            // FIXME: RhymeViewにindexを持たせるんじゃない！！
            _currentSet = _currentRhymeDataSets[_rhymeView.RhymeDataSetIndex];
            for (int i = 0; i < StaticConst.INPUT_NUM; i++)
            {
                _rhymeInputViews[i].SetRhymeText(_currentSet.RhymeDataArray[i].Text);
                // TODO: 順序を記録しておく
            }
            _rhymeView.RhymeDataSetIndex++;
            // コンボのリセット
            _evaluateModel.ResetCombo();
        }

        /// <summary>
        /// ターン開始
        /// </summary>
        public void OnMyTurnStart()
        {
            _isMyTurn = true;
            _rhymeView.OnMyTurnStart();
            // FIXME: OpponentIdから取ってくるんじゃない!!!
            _currentRhymeDataSets = _rhymeDataModel.GetCurrentRhymeDataSet(_opponentView.OpponentId);
            UpdateRhymeData();
        }

        /// <summary>
        /// ターン終了
        /// </summary>
        public void OnMyTurnEnd()
        {
            _isMyTurn = false;
            _rhymeView.OnMyTurnEnd();
        }
    }
}