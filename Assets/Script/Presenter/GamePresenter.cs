using System;
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

        /// <summary>
        /// ゲーム実行時の処理
        /// </summary>
        public void Start()
        {
            var isFirstPlay = !PlayerPrefs.HasKey(StaticConst.GAME_KEY);
            if (isFirstPlay)
            {
                PlayerPrefs.SetString(StaticConst.GAME_KEY, "HasKey");
            }
            // タイトル画面初期化
            _titleView.TitleInitialize(isFirstPlay);
            // BGMの再生
            _soundManager.TitleBGMFadeIn(isFirstPlay);
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