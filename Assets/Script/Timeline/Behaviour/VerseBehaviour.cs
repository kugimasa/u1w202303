using Script.Data;
using Script.Presenter;
using UnityEngine.Playables;

namespace Script.Timeline.Behaviour
{
    public class VerseBehaviour : PlayableBehaviour
    {
        private VersePresenter _versePresenter;
        private double _speed;
        private double _secPerBar;
        private double _offset;

        private bool _isRhymeDataUpdate;
        private RhymeType[] _rhymeTypes = { RhymeType.RED, RhymeType.PURPLE, RhymeType.GREEN, RhymeType.BLUE};

        /// <summary>
        ///     初期化処理
        /// </summary>
        public void SetVersePresenter(VersePresenter versePresenter, double bpm, double speed, double offset)
        {
            _versePresenter = versePresenter;
            // 1小節あたりにかける時間
            _secPerBar = StaticConst.MIN_AS_SEC * StaticConst.BEAT_NUM / bpm;
            _speed = speed;
            _offset = offset;
        }

        public override void OnBehaviourPlay(Playable playable, FrameData info)
        {
            base.OnBehaviourPlay(playable, info);
            // クリップ開始時にセット
            _isRhymeDataUpdate = false;
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
            var time = playable.GetTime() + _offset;
            // div: 0.0 ~ 16.0
            var div = time * _speed / _secPerBar;
            // t: 0.0 ~ 1.0
            var t = div - (int)div;
            // ビートをランダムでセット
            var index = (int)div % StaticConst.BEAT_NUM;
            if (index == 0 && !_isRhymeDataUpdate)
            {
                // セットできるかチェック
                _isRhymeDataUpdate = _versePresenter.UpdateRhymeData();
                // ライムタイプリストを更新
                if (_isRhymeDataUpdate)
                {
                    _rhymeTypes = _versePresenter.GetRhymeTypeArray();
                }
            }
            if (index == 1 && _isRhymeDataUpdate)
            {
                _isRhymeDataUpdate = false;
            }

            var rhymeType = _rhymeTypes[index];
            // ビート位置をセット
            _versePresenter.SetBeatParam(rhymeType, t);
            base.ProcessFrame(playable, info, playerData);
        }
    }
}