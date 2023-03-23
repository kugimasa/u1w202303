using DG.Tweening;
using naichilab.EasySoundPlayer.Scripts;
using UnityEngine;
using UnityEngine.UI;

namespace Script.View
{
    public class RhymeView : MonoBehaviour
    {
        [SerializeField] private Image _noteImage;
        private Sequence _noteImagesSequence;

        /// <summary>
        /// 入力があった場合の表示
        /// </summary>
        /// <param name="keyCode"></param>
        public void OnRhymeSpit(KeyCode keyCode, AudioClip rhymeSe)
        {
            // SE再生
            SePlayer.Instance.Play(rhymeSe.name);
            // ノート演出
            NoteImageAnimation();
            Debug.Log($"<color=cyan>{keyCode} Down</color>");
        }
        
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
    }
}