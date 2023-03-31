using System;
using DG.Tweening;
using Script.Data;
using UnityEngine;
using UnityEngine.UI;

namespace Script.Model
{
    public class RhymeInputModel : MonoBehaviour
    {
        [SerializeField] private KeyCode _keyCode;
        [SerializeField] private RhymeType _rhymeType;
        [SerializeField] private Button _button;
        private bool _isSpeaking;
        public KeyCode KeyCode => _keyCode;
        public RhymeType RhymeType => _rhymeType;

        public bool IsButtonClicked { get; set; }

        private void Awake()
        {
            _button.onClick.AddListener(() => IsButtonClicked = true);
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