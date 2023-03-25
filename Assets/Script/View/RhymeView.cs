using DG.Tweening;
using naichilab.EasySoundPlayer.Scripts;
using UnityEngine;
using UnityEngine.UI;

namespace Script.View
{
    public class RhymeView : MonoBehaviour
    {
        [SerializeField] private Image _noteImage;
        [SerializeField] private AudioClip _justTimingSe;
        [SerializeField] private AudioClip _justRhymeSe;
        private Sequence _noteImagesSequence;

        /// <summary>
        ///     ノーツUIの中心のスプライト演出
        /// </summary>
        private void NoteImageAnimation()
        {
            _noteImagesSequence?.Kill();
            var scaleIn = _noteImage.rectTransform.DOScale(1.25f, 0.6666f).SetEase(Ease.OutElastic);
            _noteImagesSequence = DOTween.Sequence()
                .OnStart(() => _noteImage.rectTransform.localScale = Vector3.one)
                .Append(scaleIn)
                .SetLink(gameObject);
            _noteImagesSequence.Play();
        }

        /// <summary>
        ///     入力があった場合の表示
        /// </summary>
        /// <param name="keyCode"></param>
        public void OnRhymeSpit(KeyCode keyCode, AudioClip rhymeSe)
        {
            // SE再生
            SePlayer.Instance.Play(rhymeSe.name);
            // ノート演出
            NoteImageAnimation();
        }

        /// <summary>
        ///     タイミングよく入力できた際の表示
        /// </summary>
        public void OnJustTiming()
        {
            // SE再生
            // SePlayer.Instance.Play(_justTimingSe.name);
            Debug.Log($"<color=green>Nice Timing!!</color>");
        }

        /// <summary>
        ///     ライムタイプを正しく入力できた際の表示
        /// </summary>
        public void OnJustRhyme()
        {
            // SE再生
            // SePlayer.Instance.Play(_justRhymeSe.name);
            Debug.Log($"<color=yellow>Nice RhymeType!!</color>");
        }
    }
}