using DG.Tweening;
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
        [SerializeField] private TimelineAsset _hamu1;
        [SerializeField] private TimelineAsset _hamu2;
        [SerializeField] private TimelineAsset _hamu3;
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
                switch (id)
                {
                    case 1:
                        _playableDirector.playableAsset = _battle1;
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
                _playableDirector.time = 0.0f;
                _playableDirector.Play();
            }).SetLink(gameObject);
        }
    }
}