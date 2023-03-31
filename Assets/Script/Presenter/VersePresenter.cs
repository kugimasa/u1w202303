using System.Collections.Generic;
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
        /// <param name="type">ライムタイプ</param>
        /// <param name="t">[0, 1]の値</param>
        public void SetBeatParam(RhymeType type, double t)
        {
            _evaluateModel.SetParam(type, t);
            _noteView.UpdateView(type, (float)t);
        }

        public bool UpdateRhymeData()
        {
            return _inputPresenter.UpdateRhymeData();
        }

        public RhymeType[] GetRhymeTypeArray()
        {
            var rhymeTypeList = new List<RhymeType>();
            foreach (var index in _inputPresenter.GetRhymeTypeArray())
            {
                rhymeTypeList.Add((RhymeType)index);
            }
            return rhymeTypeList.ToArray();
        }
    }
}