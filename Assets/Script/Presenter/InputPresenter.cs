using System;
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
        private int _rhymeDataSetIndex;
        private bool _isVerseOne;
        private bool _isMyTurn;
        private bool _isRhyming;
        private int[] _rhymeTypeArray = {0, 1, 2, 3};
        

        void Update()
        {
            if (!_isMyTurn) return;
            _isRhyming = false;
            for (var index = 0; index < StaticConst.INPUT_NUM; index++)
            {
                var rhymeInput = _rhymeInputs[_rhymeTypeArray[index]];
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
                        _rhymeInputViews[_rhymeTypeArray[index]].OnInput();
                        _rhymeView.OnRhymeSpit(_currentSet.RhymeDataArray[index]);
                        _isRhyming = true;
                        // 同時押しはNG
                        return;
                    }
                }
            }
        }

        /// <summary>
        /// ライムセットを更新する
        /// </summary>
        public bool UpdateRhymeData()
        {
            if (_currentRhymeDataSets == null) return false;
            // 以降は更新なし
            if (_isVerseOne && _rhymeDataSetIndex == StaticConst.BEAT_NUM) return false;
            if (!_isVerseOne && _rhymeDataSetIndex == StaticConst.BEAT_NUM * 2) return false;
            if (_isRhyming || !_isMyTurn)
            {
                return false;
            }
            _currentSet = _currentRhymeDataSets[_rhymeDataSetIndex];
            var index = Enumerable.Range(0, 4).Select(i => i).OrderBy(i => Guid.NewGuid());
            // ライムタイプ配列を更新
            _rhymeTypeArray = index.ToArray();
            for (int i = 0; i < StaticConst.INPUT_NUM; i++)
            {
                _rhymeInputViews[_rhymeTypeArray[i]].SetRhymeText(_currentSet.RhymeDataArray[i].Text);
            }
            _rhymeDataSetIndex++;
            // コンボのリセット
            _evaluateModel.ResetCombo();
            return true;
        }

        /// <summary>
        /// 入れ替えライムタイプを取得
        /// </summary>
        public int[] GetRhymeTypeArray()
        {
            return _rhymeTypeArray;
        }

        /// <summary>
        /// ターン開始
        /// </summary>
        public void OnMyTurnStart(bool isVerseOne)
        {
            _isVerseOne = isVerseOne;
            _isMyTurn = true;
            _rhymeView.OnMyTurnStart();
            // FIXME: OpponentIdから取ってくるんじゃない!!!
            _currentRhymeDataSets = new List<RhymeDataSet>(_rhymeDataModel.GetCurrentRhymeDataSet(_opponentView.OpponentId));
            if (_isVerseOne)
            {
                _rhymeDataSetIndex = 0;
            }
            else
            {
                _rhymeDataSetIndex = 4;
            }
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