using UnityEngine;
using UnityEngine.Playables;

namespace Script.Timeline.Marker
{
    public class RhymeSpitNotification : UnityEngine.Timeline.Marker, INotification
    {
        [SerializeField] private string _rhymeStr;
        public string RhymeStr => _rhymeStr;
        
        // 通知を識別するために一意な値を指定する
        // ref: https://speakerdeck.com/lycoris102/timeline-signals-tutorial?slide=22
        public PropertyName id => new PropertyName("RhymeString");
    }
}