using System;
using UnityEngine;
using UnityEngine.Playables;

[Serializable]
public class VerseClip : PlayableAsset
{
    public ExposedReference<VersePresenter> _versePresenter;
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
        behaviour.SetVersePresenter(resolvedVersePresenter, _bpm, _speed);
        var playable = ScriptPlayable<VerseBehaviour>.Create(graph, behaviour);
        return playable;
    }
}
