using UnityEngine;

namespace Script.Model
{
    public class EvaluateModel : MonoBehaviour
    {
        [SerializeField, Range(0, 1)] private double _percision;
        
        private double _t;

        /// <summary>
        ///     現在の入力位置をセット
        /// </summary>
        public void SetT(double t)
        {
            _t = t;
        }

        /// <summary>
        ///     タイミングの判定を行う
        /// </summary>
        /// <returns>判定</returns>
        public bool EvaluateT()
        {
            if (_t <= _percision || 1.0 - _percision <= _t)
            {
                return true;
            }
            return false;
        }
    }
}