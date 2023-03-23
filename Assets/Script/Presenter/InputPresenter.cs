using Script.Model;
using Script.View;
using UnityEngine;

namespace Script.Presenter
{
    public class InputPresenter : MonoBehaviour
    {
        [SerializeField] private RhymeInputModel[] _rhymeInputs = new RhymeInputModel[INPUT_NUM];
        [SerializeField] private EvaluateModel _evaluateModel;
        [SerializeField] private RhymeView _rhymeView;
        [SerializeField, Range(0, 3.0f)] private float _delay;
        [SerializeField] private AudioClip _sampleSe;
        private const int INPUT_NUM = 4;

        void Update()
        {
            foreach (var rhymeInput in _rhymeInputs)
            {
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
                        // 表示 & 音声処理
                        // TODO: データから現状のSEを取得
                        _rhymeView.OnRhymeSpit(rhymeInput.KeyCode, _sampleSe);
                    }
                    else
                    {
                        Debug.Log($"<color=red>{rhymeInput.KeyCode} is used.</color>");
                    }
                }
            }
        }
    }
}