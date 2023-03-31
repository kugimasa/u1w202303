using System;
using Script.Util;
using Script.View;
using UnityEngine;

namespace Script.Presenter
{
    public class BeatCheckPresenter : MonoBehaviour
    {
        [SerializeField] private BeatCheckView _beatCheckView;
        [SerializeField] private TimelineManager _timelineManager;
        private int _battleId;
        private bool _isStar;

        public void SetBattleId(int id)
        {
            _battleId = id;
            _beatCheckView.ShowBeatCheckView(_battleId);
            _isStar = false;
        }

        public void PlayHamBeat()
        {
            if (_isStar)
            {
                _beatCheckView.PlayHamBeat(_battleId);
                _isStar = false;
            }
        }

        public void PlayStarBeat()
        {
            if (!_isStar)
            {
                _beatCheckView.PlayStarBeat(_battleId);
                _isStar = true;
            }
        }

        /// <summary>
        /// HAMのビートでバトル開始
        /// </summary>
        public void StartBattleWithHamBeat()
        {
            // ビートチェックパネルの非表示
            _beatCheckView.HideBeatCheckView();
            _timelineManager.PlayBattleWithHamBeat(_battleId);
        }

        /// <summary>
        /// STARのビートでバトル開始
        /// </summary>
        public void StartBattleWithStarBeat()
        {
            // ビートチェックパネルの非表示
            _beatCheckView.HideBeatCheckView();
            _timelineManager.PlayBattleWithStarBeat(_battleId);
        }
    }
}