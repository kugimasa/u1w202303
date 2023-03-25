using Script.Presenter;
using Script.Timeline.Marker;
using Script.View;
using UnityEngine;
using UnityEngine.Playables;

namespace Script.Timeline.Receiver
{
    public class RhymeSpitReceiver : MonoBehaviour, INotificationReceiver
    {
        [SerializeField] private OpponentView _opponentView;
        
        public void OnNotify(Playable origin, INotification notification, object context)
        {
            var marker = notification as RhymeSpitNotification;
            if (marker == null)
            {
                return;
            }
            _opponentView.RhymeSpit(marker.RhymeData);
        }
    }
}