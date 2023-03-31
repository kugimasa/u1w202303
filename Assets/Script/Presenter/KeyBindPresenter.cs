using System;
using System.Collections.Generic;
using DG.Tweening;
using Script.Data;
using Script.Model;
using Script.View;
using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace Script.Presenter
{
    public class KeyBindPresenter : MonoBehaviour
    {
        [SerializeField] private RhymeInputModel[] _rhymeInputModels = new RhymeInputModel[StaticConst.INPUT_NUM];
        [SerializeField] private KeyBindView[] _keyBindViews = new KeyBindView[StaticConst.INPUT_NUM];
        [SerializeField] private Button[] _keyBindButtons = new Button[StaticConst.INPUT_NUM];
        [SerializeField] private TextMeshProUGUI _confirmationText;
        private int _waitingId = -1;

        void Start()
        {
            for (int i = 0; i < StaticConst.INPUT_NUM; i++)
            {
                var id = i;
                _keyBindButtons[id].OnClickAsObservable().Subscribe(_ => OnBindButtonClick(id)).AddTo(this);
            }
            _confirmationText.text = "キーを変えたいボタンをクリック";
        }

        private void Update()
        {
            // 入力の待機
            if (_waitingId != -1)
            {
                // FIXME 非推奨 InputSystemに変更したい
                // 他のキーは割り当てNG
                foreach (var assignedKeyCode in GetAssignedKeyCodes())
                {
                    if (Input.GetKeyDown(assignedKeyCode))
                    {
                        _confirmationText.text = "既に使われているキーだ!";
                        DOVirtual.DelayedCall(2.0f, () =>
                        {
                            _confirmationText.text = "キーを変えたいボタンをクリック";
                        }).SetLink(gameObject);
                        return;
                    }
                }
                // マウスは無視
                if (Input.GetKeyDown(KeyCode.Mouse0) ||
                    Input.GetKeyDown(KeyCode.Mouse1) ||
                    Input.GetKeyDown(KeyCode.Mouse2) || 
                    Input.GetKeyDown(KeyCode.Mouse3) ||
                    Input.GetKeyDown(KeyCode.Mouse4) ||
                    Input.GetKeyDown(KeyCode.Mouse5) || 
                    Input.GetKeyDown(KeyCode.Mouse6))
                    return;
                // 何らかのキーが入力された
                if (Input.anyKey)
                {
                    var bindKeyId = _waitingId;
                    _waitingId = -1;
                    OnBindKeyDown(bindKeyId);
                }
            }
        }

        /// <summary>
        ///     キーバインドボタンクリック
        /// </summary>
        /// <param name="id">バインドID</param>
        private void OnBindButtonClick(int id)
        {
            _waitingId = id;
            _confirmationText.text = $"キーを入力だ!";
        }

        /// <summary>
        ///     キーバインド登録
        /// </summary>
        /// <param name="id">バインドID</param>
        private void OnBindKeyDown(int id)
        {
            foreach (KeyCode bindKeyCode in Enum.GetValues(typeof(KeyCode)))
            {
                if (Input.GetKeyDown(bindKeyCode))
                {
                    // 割り当て変更
                    _rhymeInputModels[id].SetKeyCode(bindKeyCode);
                    // 表示関連
                    // FIXME: 同時押しでテキストに入ってしまう
                    _keyBindViews[id].SetKeyCodeText(bindKeyCode.ToString());
                    _confirmationText.text = $"{bindKeyCode.ToString()} に変わったぞ!";
                    DOVirtual.DelayedCall(2.0f, () =>
                    {
                        _confirmationText.text = "キーを変えたいボタンをクリック";
                    }).SetLink(gameObject);
                    break;
                }
            }
        }

        private List<KeyCode> GetAssignedKeyCodes()
        {
            var keyCodes = new List<KeyCode>();
            for (int i = 0; i < StaticConst.INPUT_NUM; i++)
            {
                keyCodes.Add(_rhymeInputModels[i].KeyCode);
            }
            return keyCodes;
        }
    }
}