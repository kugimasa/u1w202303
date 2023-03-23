using Script.Presenter;
using UnityEngine.Playables;

namespace Script.Timeline.Behaviour
{
    public class VerseBehaviour : PlayableBehaviour
    {
        private VersePresenter _versePresenter;
        private double _speed;
        private double _secPerBar;
        private const int MIN_AS_SEC = 60;
        private const int BEAT_NUM = 4;
    
        /// <summary>
        /// 初期化処理
        /// </summary>
        public void SetVersePresenter(VersePresenter versePresenter, double bpm, double speed)
        {
            _versePresenter = versePresenter;
            // 1小節あたりにかける時間
            _secPerBar = MIN_AS_SEC * BEAT_NUM / bpm;
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
            var t = (float)(div - (int)div);
            // ビート位置をセット
            _versePresenter.SetBeatT(t);
            base.ProcessFrame(playable, info, playerData);
        }
    }
}