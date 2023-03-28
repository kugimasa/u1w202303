using System;
using DG.Tweening;
using Script.Data;
using Script.Util;
using Script.View;
using UnityEngine;

namespace Script.Presenter
{
    public class GamePresenter : MonoBehaviour
    {
        [SerializeField] private TitleView _titleView;
        [SerializeField] private SoundManager _soundManager;
        [SerializeField] private TimelineManager _timelineManager;

        private bool _isFirstPlay;

        /// <summary>
        /// ゲーム実行時の処理
        /// </summary>
        public void Start()
        {
            _isFirstPlay = !PlayerPrefs.HasKey(StaticConst.GAME_KEY);
            if (_isFirstPlay)
            {
                PlayerPrefs.SetString(StaticConst.GAME_KEY, "HasKey");
            }
            // タイトル画面初期化
            _titleView.TitleInitialize(_isFirstPlay);
            // BGMの再生
            _soundManager.TitleBGMFadeIn(_isFirstPlay);
        }

        /// <summary>
        /// スタートボタンクリック時
        /// </summary>
        public void OnGameStart()
        {
            // タイトルをフェードアウト
            _titleView.TitleFadeOut();
            // BGMのボリュームを落とす
            _soundManager.TitleBGMFadeOut(0.1f, 1.0f);
            // 2秒後イントロスタート
            DOVirtual.DelayedCall(2.0f,() => _timelineManager.PlayIntro(_isFirstPlay));
        }

        /// <summary>
        /// ゲームリスタート処理
        /// </summary>
        public void OnGameRestart()
        {
            // TODO: もろもろ初期化
            // 暗転
            // 明転
        }
    }
}