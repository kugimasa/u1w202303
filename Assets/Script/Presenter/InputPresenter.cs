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
        private RhymeDataSet _currentSet;
        // TODO: 入力ストップフラグ

        void Update()
        {
            if (_currentSet == null) return;
            for (var index = 0; index < StaticConst.INPUT_NUM; index++)
            {
                var rhymeInput = _rhymeInputs[index];
                if (Input.GetKeyDown(rhymeInput.KeyCode))
                {
                    // ライムをスピット
                    if (rhymeInput.TryRhymeSpit(_delay))
                    {
                        // タイミング判定処理
                        if (_evaluateModel.EvaluateT())
                        {
                            _rhymeView.OnJustTiming();
                        }
                        // ライムタイプ判定
                        if (_evaluateModel.EvaluateRhymeType(rhymeInput.RhymeType))
                        {
                            _rhymeView.OnJustRhyme();
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
            _currentSet = _rhymeDataSets[0];
            for (int i = 0; i < StaticConst.INPUT_NUM; i++)
            {
                _rhymeInputViews[i].SetRhymeText(_currentSet.RhymeDataArray[i].Text);
            }
        }
    }
}