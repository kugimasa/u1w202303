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
        /// <param name="t">[0, 1]の値</param>
        public void SetBeatT(double t)
        {
            _evaluateModel.SetT(t);
            _noteView.UpdateView((float)t);
        }
    }
}