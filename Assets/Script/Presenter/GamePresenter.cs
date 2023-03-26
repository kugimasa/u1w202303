using System;
using Script.Util;
using Script.View;
using UnityEngine;

namespace Script.Presenter
{
    public class GamePresenter : MonoBehaviour
    {
        [SerializeField] private TitleView _titleView;
        [SerializeField] private SoundManager _soundManager;

        /// <summary>
        /// ゲーム実行時の処理
        /// </summary>
        public void Start()
        {
            // BGMのフェードイン処理
            _soundManager.TitleBGMFadeIn();
            // タイトル画面初期化
            _titleView.TitleInitialize();
        }

        /// <summary>
        /// スタートボタンクリック時
        /// </summary>
        public void OnGameStart()
        {
            // タイトルをフェードアウト
            _titleView.TitleFadeOut();
            // イントロスタート
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