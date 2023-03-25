using System;
using Script.Data;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

namespace Script.Timeline.Marker
{
    [Serializable]
    public class RhymeSpitNotification : UnityEngine.Timeline.Marker, INotification, INotificationOptionProvider
    {
        [SerializeField] private RhymeData _rhymeData;
        public RhymeData RhymeData => _rhymeData;
        
        // 通知を識別するために一意な値を指定する
        // ref: https://speakerdeck.com/lycoris102/timeline-signals-tutorial?slide=22
        public PropertyName id => new PropertyName("RhymeData");
        // Editor実行時にも確認を行いたい場合はINotificationOptionProviderを継承している必要がある
        public NotificationFlags flags => NotificationFlags.TriggerInEditMode;
    }
}