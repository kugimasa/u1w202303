using UnityEngine;
using UnityEngine.UI;

namespace Script.View
{
    public class BattleSwitchView : MonoBehaviour
    {
        [SerializeField] private Button _battle1StartButton;

        private void Awake()
        {
            _battle1StartButton.interactable = false;
            // TODO: 初期化時は透明
        }

        public void ShowBattle1()
        {
            // TODO: 表示フェードイン
            _battle1StartButton.interactable = true;
        }
    }
}