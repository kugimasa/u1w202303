using UnityEngine;

namespace Script.Model
{
    public class EvaluateModel : MonoBehaviour
    {
        private RhymeType _rhymeType;
        private double _t;
        private double _precision;

        /// <summary>
        ///     現在の入力位置をセット
        /// </summary>
        public void SetParam(RhymeType rhymeType, double t, double precision)
        {
            _rhymeType = rhymeType;
            _t = t;
            _precision = precision;
        }

        /// <summary>
        ///     タイミングの判定を行う
        /// </summary>
        /// <returns>判定</returns>
        public bool EvaluateT()
        {
            if (_t <= _precision || 1.0 - _precision <= _t)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        ///     ライムタイプの判定
        /// </summary>
        /// <param name="inputRhymeType">入力</param>
        /// <returns></returns>
        public bool EvaluateRhymeType(RhymeType inputRhymeType)
        {
            if (inputRhymeType == _rhymeType)
            {
                return true;
            }
            return false;
        }
    }
}