using System.Collections.Generic;
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
        [SerializeField] private List<RhymeDataSet> _rhymeDataSets;
        [SerializeField] private GaugeSliderView _gaugeSliderView;
        private RhymeDataSet _currentSet;
        private bool _isMyTurn;

        void Update()
        {
            if (!_isMyTurn) return;
            if (_currentSet == null) return;
            for (var index = 0; index < StaticConst.INPUT_NUM; index++)
            {
                var rhymeInput = _rhymeInputs[index];
                if (Input.GetKeyDown(rhymeInput.KeyCode))
                {
                    // ライムをスピット
                    if (rhymeInput.TryRhymeSpit(_delay))
                    {
                        // ライムタイプ判定
                        if (_evaluateModel.EvaluateRhymeType(rhymeInput.RhymeType))
                        {
                            // タイミング判定処理
                            if (_evaluateModel.EvaluateT())
                            {
                                _rhymeView.OnJustTiming();
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
        // TODO: ライムデータの更新はどのタイミングで行う？
        public void UpdateRhymeData()
        {
            _currentSet = _rhymeDataSets[0];
            for (int i = 0; i < StaticConst.INPUT_NUM; i++)
            {
                _rhymeInputViews[i].SetRhymeText(_currentSet.RhymeDataArray[i].Text);
            }
        }

        /// <summary>
        /// ターン開始
        /// </summary>
        public void OnMyTurnStart()
        {
            _isMyTurn = true;
        }

        /// <summary>
        /// ターン終了
        /// </summary>
        public void OnMyTurnEnd()
        {
            _isMyTurn = false;
        }
    }
}