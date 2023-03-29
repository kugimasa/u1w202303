using System;
using System.Collections.Generic;
using Script.Data;
using Script.Model;
using Script.Presenter;
using Script.Timeline.Behaviour;
using UnityEngine;
using UnityEngine.Playables;

namespace Script.Timeline.Clip
{
    [Serializable]
    public class VerseClip : PlayableAsset
    {
        public ExposedReference<VersePresenter> _versePresenter;
        [SerializeField] private List<RhymeType> _rhymeTypes = new List<RhymeType>();
        [SerializeField] private double _bpm;
        [SerializeField] private double _speed;

        public override Playable CreatePlayable(PlayableGraph graph, GameObject owner)
        {
            var resolvedVersePresenter = _versePresenter.Resolve(graph.GetResolver());
            if (resolvedVersePresenter == null)
            {
                return default;
            }
            var behaviour = new VerseBehaviour();
            behaviour.SetVersePresenter(resolvedVersePresenter, _rhymeTypes, _bpm, _speed);
            var playable = ScriptPlayable<VerseBehaviour>.Create(graph, behaviour);
            return playable;
        }
    }
}
