using Script.Data;
using UnityEngine;

namespace Script.Model
{
    public class EvaluateModel : MonoBehaviour
    {
        [SerializeField] private int _justTimingScore;
        [SerializeField] private int _justRhymeTypeScore;
        private RhymeType _rhymeType;
        private double _t;
        private double _precision;
        private int _score;

        /// <summary>
        /// バトル開始時はリセット
        /// </summary>
        public void OnBattleStart()
        {
            _score = 0;
        }

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
                // スコア加算
                _score += _justTimingScore;
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
                // スコア加算
                _score += _justRhymeTypeScore;
                return true;
            }
            return false;
        }

        /// <summary>
        ///     勝者の判定
        /// </summary>
        /// <param name="score"></param>
        /// <returns></returns>
        public bool EvaluateWinner(int opponentScore)
        {
            // FIXME: 同率だった場合は勝ち
            if (_score == opponentScore)
            {
                _score += 10;
            }
            if (_score > opponentScore)
            {
                return true;
            }
            return false;
        }
    }
}