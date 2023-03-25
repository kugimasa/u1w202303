using System.Collections.Generic;
using Script.Data;
using Script.Presenter;
using UnityEngine;
using UnityEngine.Playables;

namespace Script.Timeline.Behaviour
{
    public class VerseBehaviour : PlayableBehaviour
    {
        private VersePresenter _versePresenter;
        private List<RhymeType> _rhymeTypes;
        private double _speed;
        private double _precision;
        private double _secPerBar;
        private int _currentIndex;

        private int GetCurrentIndex(double div)
        {
            int index = (int)(div - _precision);
            index = Mathf.Max(0, index);
            if (index >= _rhymeTypes.Count)
            {
                return 0;
            }
            return index;
        }

        /// <summary>
        /// 初期化処理
        /// </summary>
        public void SetVersePresenter(VersePresenter versePresenter, List<RhymeType> rhymeTypes, double bpm, double speed, double precision)
        {
            _versePresenter = versePresenter;
            _rhymeTypes = rhymeTypes;
            // 1小節あたりにかける時間
            _secPerBar = StaticConst.MIN_AS_SEC * StaticConst.BEAT_NUM / bpm;
            _speed = speed;
            _precision = precision;
        }
    
        /// <summary>
        ///     Timeline上での再生処理
        /// </summary>
        /// <param name="playable"></param>
        /// <param name="info"></param>
        /// <param name="playerData"></param>
        public override void ProcessFrame(Playable playable, FrameData info, object playerData)
        {
            // 現在の再生位置を取得
            var time = playable.GetTime();
            var div = time * _speed / _secPerBar;
            var t = div - (int)div;
            // 現在のインデックスの更新
            _currentIndex = GetCurrentIndex(div);
            // TODO: 表示の更新は毎フレーム出なくて良い
            var isUpdateRhymeData = true;
            var rhymeType = _rhymeTypes[_currentIndex];
            // ビート位置をセット
            _versePresenter.SetBeatParam(isUpdateRhymeData, rhymeType, t, _precision);
            base.ProcessFrame(playable, info, playerData);
        }
    }
}