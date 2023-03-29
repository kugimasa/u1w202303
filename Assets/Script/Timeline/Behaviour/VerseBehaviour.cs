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
        private double _secPerBar;
        private int _currentIndex;

        private int GetCurrentIndex(int num)
        {
            var index = Mathf.Max(0, num);
            if (index >= _rhymeTypes.Count)
            {
                return 0;
            }
            return index;
        }

        /// <summary>
        /// 初期化処理
        /// </summary>
        public void SetVersePresenter(VersePresenter versePresenter, List<RhymeType> rhymeTypes, double bpm, double speed)
        {
            _versePresenter = versePresenter;
            _rhymeTypes = rhymeTypes;
            // 1小節あたりにかける時間
            _secPerBar = StaticConst.MIN_AS_SEC * StaticConst.BEAT_NUM / bpm;
            _speed = speed;
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
            _currentIndex = GetCurrentIndex((int)div);
            // TODO: 表示の更新は毎フレーム出なくて良い
            var isUpdateRhymeData = true;
            // TODO: ライムタイプの選出方法
            var rhymeType = _rhymeTypes[_currentIndex];
            // ビート位置をセット
            _versePresenter.SetBeatParam(isUpdateRhymeData, rhymeType, t);
            base.ProcessFrame(playable, info, playerData);
        }
    }
}