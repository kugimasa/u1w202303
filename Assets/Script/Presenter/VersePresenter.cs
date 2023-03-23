using Script.View;
using UnityEngine;

namespace Script.Presenter
{
    public class VersePresenter : MonoBehaviour
    {
        [SerializeField] private NoteView _noteView;
    
        /// <summary>
        ///     ビート位置をセット
        /// </summary>
        /// <param name="t">[0, 1]の値</param>
        public void SetBeatT(float t)
        {
            _noteView.UpdateView(t);
        }
    }
}