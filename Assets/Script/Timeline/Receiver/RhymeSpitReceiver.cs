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
            var rhymeSpitWithAudioNotification = notification as RhymeSpitWithAudioNotification;
            if (rhymeSpitWithAudioNotification != null)
            {
                _opponentView.RhymeSpitWithAudio(rhymeSpitWithAudioNotification.RhymeData);
                return;
            }
            var rhymeSpitNotification = notification as RhymeSpitNotification;
            if (rhymeSpitNotification != null)
            {
                _opponentView.RhymeSpit(rhymeSpitNotification.RhymeStr);
                return;
            }
        }
    }
}