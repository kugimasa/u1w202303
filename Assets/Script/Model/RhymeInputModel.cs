using DG.Tweening;
using UnityEngine;

namespace Script.Model
{
    public class RhymeInputModel : MonoBehaviour
    {
        [SerializeField, Range(0, 3)] private int _id;
        [SerializeField] private KeyCode _keyCode;
        private bool _isSpeaking;

        public KeyCode KeyCode => _keyCode;

        /// <summary>
        ///     入力受付状態を見て発言する
        /// </summary>
        /// <param name="delay">入力受付しない遅延</param>>
        /// <returns></returns>
        public bool TrySpeak(float delay)
        {
            if (_isSpeaking)
            {
                return false;
            }
            _isSpeaking = true;
            // 解除
            DOVirtual.DelayedCall(delay, () => _isSpeaking = false).SetLink(gameObject);
            return true;
        }

        /// <summary>
        ///     キーバインドの変更
        /// </summary>
        /// <param name="keyCode"></param>
        public void SetKeyCode(KeyCode keyCode)
        {
            _keyCode = keyCode;
        }
    }
}