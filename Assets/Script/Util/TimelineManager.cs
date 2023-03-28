using DG.Tweening;
using Script.Data;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

namespace Script.Util
{
    public class TimelineManager : MonoBehaviour
    {
        [SerializeField] private PlayableDirector _playableDirector;
        [SerializeField] private TimelineAsset _intro;
        [SerializeField] private TimelineAsset _battle1;
        [SerializeField] private TimelineAsset _battle2;
        [SerializeField] private TimelineAsset _battle3;
        [SerializeField] private TimelineAsset _ham1;
        [SerializeField] private TimelineAsset _ham2;
        [SerializeField] private TimelineAsset _ham3;
        [SerializeField] private TimelineAsset _star1;
        [SerializeField] private TimelineAsset _star2;
        [SerializeField] private TimelineAsset _star3;

        public void PlayIntro(bool isFirstPlay)
        {
            _playableDirector.playableAsset = _intro;
            if (isFirstPlay)
            {
                DOVirtual.DelayedCall(2.0f, () =>
                {
                    _playableDirector.time = 0.0;
                    _playableDirector.Play();
                }).SetLink(gameObject);
            }
            else
            {
                _playableDirector.time = 26.0;
                _playableDirector.Play();
            }
        }

        /// <summary>
        /// バトルを再生
        /// </summary>
        public void PlayBattle(int id)
        {
            DOVirtual.DelayedCall(2.0f, () =>
            {
                _playableDirector.time = 0.0f;
                switch (id)
                {
                    case 1:
                        _playableDirector.playableAsset = _battle1;
                        // ２回目以降の場合
                        if (PlayerPrefs.HasKey(StaticConst.GAME_KEY))
                        {
                            _playableDirector.time = 12.6;
                        }
                        break;
                    case 2:
                        _playableDirector.playableAsset = _battle2;
                        break;
                    case 3:
                        _playableDirector.playableAsset = _battle3;
                        break;
                    default:
                        _playableDirector.playableAsset = _battle1;
                        break;
                }
                // TODO: 余裕があればスキップする仕組み
                _playableDirector.Play();
            }).SetLink(gameObject);
        }

        /// <summary>
        /// HAMビートでバトル開始
        /// </summary>
        public void PlayBattleWithHamBeat(int id)
        {
            _playableDirector.time = 0.0;
            switch (id)
            {
                case 0:
                    _playableDirector.playableAsset = _ham1;
                    break;
                case 1:
                    _playableDirector.playableAsset = _ham2;
                    break;
                case 2:
                    _playableDirector.playableAsset = _ham3;
                    break;
                default:
                    _playableDirector.playableAsset = _star1;
                    break;
            }
            _playableDirector.Play();
        }

        /// <summary>
        /// STARビートでバトル開始
        /// </summary>
        public void PlayBattleWithStarBeat(int id)
        {
            _playableDirector.time = 0.0;
            switch (id)
            {
                case 0:
                    _playableDirector.playableAsset = _star1;
                    break;
                case 1:
                    _playableDirector.playableAsset = _star2;
                    break;
                case 2:
                    _playableDirector.playableAsset = _star3;
                    break;
                default:
                    _playableDirector.playableAsset = _star1;
                    break;
            }
            _playableDirector.Play();
        }
    }
}