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

        /// <summary>
        ///     ビート位置をセット
        /// </summary>
        /// <param name="type">ライムタイプ</param>
        /// <param name="t">[0, 1]の値</param>
        /// <param name="precision">精度</param>
        public void SetBeatParam(RhymeType type, double t, double precision)
        {
            _evaluateModel.SetParam(type, t, precision);
            _noteView.UpdateView((float)t);
        }
    }
}