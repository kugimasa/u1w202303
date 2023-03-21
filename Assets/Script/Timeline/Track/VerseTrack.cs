using System;
using UnityEngine.Timeline;

[Serializable]
[TrackClipType(typeof(VerseClip))]
[TrackBindingType(typeof(VersePresenter))]
public class VerseTrack : PlayableTrack
{
}