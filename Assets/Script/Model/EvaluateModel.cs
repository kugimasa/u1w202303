using Script.Data;
using UnityEngine;

namespace Script.Model
{
    public class EvaluateModel : MonoBehaviour
    {
        [SerializeField] private float _maxScore;
        [SerializeField] private int _justTimingScore;
        [SerializeField] private int[] _opponentClearScore = new int[StaticConst.OPPONENT_NUM];
        [SerializeField] private double[] _precisions = new double[StaticConst.OPPONENT_NUM];
        private RhymeType _rhymeType;
        private double _t;
        private int _score;

        public float Score01 => _score / _maxScore;
        public double Precision => _precisions[0];

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
        public void SetParam(RhymeType rhymeType, double t)
        {
            _rhymeType = rhymeType;
            _t = t;
        }

        /// <summary>
        ///     タイミングの判定を行う
        /// </summary>
        /// <returns>判定</returns>
        public bool EvaluateT()
        {
            if (1.0 - _precisions[0] <= _t)
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
                return true;
            }
            return false;
        }

        /// <summary>
        ///     勝者の判定
        /// </summary>
        public bool EvaluateWinner(int opponentId)
        {
            var opponentScore = _opponentClearScore[opponentId];
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

        public float GetOpponentScore01(int opponentId)
        {
            return _opponentClearScore[opponentId] / _maxScore;
        }
    }
}