using Script.View;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class VersePresenter : MonoBehaviour
{
    [SerializeField] private NoteView _noteView;
    
    /// <summary>
    ///     ビート位置をセット
    /// </summary>
    /// <param name="t">[0, 1]の値</param>
    public void SetBeatT(float t)
    {
        _noteView.SetSliderValue(t);
    }
}