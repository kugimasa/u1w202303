using Script.Data;
using Script.Model;
using Script.View;
using UnityEngine;

namespace Script.Presenter
{
    public class VersePresenter : MonoBehaviour
    {
        [SerializeField] private EvaluateModel _evaluateModel;
        [SerializeField] private NoteView _noteView;
        [SerializeField] private InputPresenter _inputPresenter;

        /// <summary>
        ///     ビート位置をセット
        /// </summary>
        /// <param name="isUpdateRhymeData">ライムセットを更新するか</param>
        /// <param name="type">ライムタイプ</param>
        /// <param name="t">[0, 1]の値</param>
        public void SetBeatParam(bool isUpdateRhymeData, RhymeType type, double t)
        {
            _evaluateModel.SetParam(type, t);
            _noteView.UpdateView(type, (float)t);
            if (isUpdateRhymeData)
            {
                _inputPresenter.UpdateRhymeData();
            }
        }
    }
}