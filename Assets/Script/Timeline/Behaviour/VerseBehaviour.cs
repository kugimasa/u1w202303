using System;
using System.Collections.Generic;
using System.Linq;
using Script.Data;
using Script.Presenter;
using UnityEngine;
using UnityEngine.Playables;

namespace Script.Timeline.Behaviour
{
    public class VerseBehaviour : PlayableBehaviour
    {
        private VersePresenter _versePresenter;
        private double _speed;
        private double _secPerBar;
        private int _currentIndex;
        private RhymeType[] _rhymeTypes = { RhymeType.RED, RhymeType.PURPLE, RhymeType.GREEN, RhymeType.BLUE};
        private RhymeType _currentRhymeType;

        /// <summary>
        /// 初期化処理
        /// </summary>
        public void SetVersePresenter(VersePresenter versePresenter, double bpm, double speed)
        {
            _versePresenter = versePresenter;
            // 1小節あたりにかける時間
            _secPerBar = StaticConst.MIN_AS_SEC * StaticConst.BEAT_NUM / bpm;
            _speed = speed;
        }

        public override void OnBehaviourPlay(Playable playable, FrameData info)
        {
            base.OnBehaviourPlay(playable, info);
            // クリップ開始時にセット
            _currentIndex = 0;
            _currentRhymeType = RhymeType.RED;
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
            // div: 0.0 ~ 16.0
            var div = time * _speed / _secPerBar;
            // t: 0.0 ~ 1.0
            var t = div - (int)div;
            // ビートをランダムでセット
            if (_currentIndex < (int)div)
            {
                _currentIndex = (int)div % StaticConst.BEAT_NUM;
                _currentRhymeType = _rhymeTypes[_currentIndex];
            }
            // ビート位置をセット
            _versePresenter.SetBeatParam(_currentRhymeType, t);
            base.ProcessFrame(playable, info, playerData);
        }
    }
}