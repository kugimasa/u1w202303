using System;
using System.Collections.Generic;
using Script.Model;
using Script.View;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace Script.Presenter
{
    public class KeyBindPresenter : MonoBehaviour
    {
        [SerializeField] private RhymeInputModel[] _rhymeInputModels = new RhymeInputModel[INPUT_NUM];
        [SerializeField] private KeyBindView[] _keyBindViews = new KeyBindView[INPUT_NUM];
        [SerializeField] private Button[] _keyBindButtons = new Button[INPUT_NUM];
        [SerializeField] private GameObject _confirmationPanel;
        private const int INPUT_NUM = 4;
        private int _waitingId = -1;

        void Start()
        {
            for (int i = 0; i < INPUT_NUM; i++)
            {
                var id = i;
                _keyBindButtons[id].OnClickAsObservable().Subscribe(_ => OnBindButtonClick(id)).AddTo(this);
            }
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
                        // TODO: 同じKeyはだめだYO
                        return;
                    }
                }
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
            // パネルの表示
            _confirmationPanel.SetActive(true);
            _waitingId = id;
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
                    _keyBindViews[id].SetKeyCodeText(Input.inputString);
                    _confirmationPanel.SetActive(false);
                    break;
                }
            }
        }

        private List<KeyCode> GetAssignedKeyCodes()
        {
            var keyCodes = new List<KeyCode>();
            for (int i = 0; i < INPUT_NUM; i++)
            {
                keyCodes.Add(_rhymeInputModels[i].KeyCode);
            }
            // マウスを弾いておく
            // FIXME: ここも何とかしたい
            keyCodes.Add(KeyCode.Mouse0);
            keyCodes.Add(KeyCode.Mouse1);
            keyCodes.Add(KeyCode.Mouse2);
            keyCodes.Add(KeyCode.Mouse3);
            keyCodes.Add(KeyCode.Mouse4);
            keyCodes.Add(KeyCode.Mouse5);
            return keyCodes;
        }
    }
}