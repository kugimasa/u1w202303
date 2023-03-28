using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

namespace Script.Util
{
    public class TimelineManager : MonoBehaviour
    {
        [SerializeField] private PlayableDirector _playableDirector;
        [SerializeField] private TimelineAsset _intro;
        [SerializeField] private TimelineAsset _hamu1;
        [SerializeField] private TimelineAsset _hamu2;
        [SerializeField] private TimelineAsset _hamu3;
        [SerializeField] private TimelineAsset _star1;
        [SerializeField] private TimelineAsset _star2;
        [SerializeField] private TimelineAsset _star3;

        public void PlayIntro(bool isFirstPlay)
        {
            _playableDirector.playableAsset = _intro;
            var startTime = 0.0;
            if (!isFirstPlay) startTime = 26.0;
            _playableDirector.time = startTime;
            _playableDirector.Play();
        }
    }
}