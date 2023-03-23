using System;
using Script.Presenter;
using Script.Timeline.Clip;
using UnityEngine.Timeline;

namespace Script.Timeline.Track
{
    [Serializable]
    [TrackClipType(typeof(VerseClip))]
    [TrackBindingType(typeof(VersePresenter))]
    public class VerseTrack : PlayableTrack
    {
    }
}