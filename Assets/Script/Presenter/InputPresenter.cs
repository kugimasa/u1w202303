using Script.Model;
using UnityEngine;

namespace Script.Presenter
{
    public class InputPresenter : MonoBehaviour
    {
        [SerializeField] private RhymeInputModel[] _rhymeInputs = new RhymeInputModel[INPUT_NUM];
        [SerializeField, Range(0, 3.0f)] private float _delay;
        private const int INPUT_NUM = 4;
        void Update()
        {
            foreach (var rhymeInput in _rhymeInputs)
            {
                if (Input.GetKeyDown(rhymeInput.KeyCode))
                {
                    // ライムをスピット
                    if (rhymeInput.TrySpeak(_delay))
                    {
                        Debug.Log($"<color=cyan>{rhymeInput.KeyCode} Down</color>");
                    }
                    else
                    {
                        
                        Debug.Log($"<color=red>{rhymeInput.KeyCode} is used.</color>");
                    }
                }
            }
        }
    }
}
