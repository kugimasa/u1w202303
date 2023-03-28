using System;
using DG.Tweening;
using naichilab.EasySoundPlayer.Scripts;
using Script.Data;
using UnityEngine;
using UnityEngine.UI;

namespace Script.Util
{
    public class SoundManager : MonoBehaviour
    {
        [SerializeField] private AudioSource _bgmSource;
        [SerializeField] private AudioSource _seSource;
        [SerializeField] private SePlayerVolumeController _sePlayerVolumeController;
        [SerializeField] private AudioClip _titleClip;
        [SerializeField] private Slider _bgmSlider;
        [SerializeField] private Slider _seSlider;
        [SerializeField] private float _fadeInDuration;

        private void Awake()
        {            
            // 音量設定をロードする
            LoadVolumeSetting();
        }

        /// <summary>
        /// 音量設定のロード
        /// </summary>
        private void LoadVolumeSetting()
        {
            if (PlayerPrefs.HasKey(StaticConst.BGM_KEY))
            {
                var volume = PlayerPrefs.GetFloat(StaticConst.BGM_KEY);
                _bgmSource.volume = volume;
                _bgmSlider.value = volume;
            }
            else
            {
                SetBgmVal(StaticConst.INIT_VOLUME);
            }

            if (PlayerPrefs.HasKey(StaticConst.SE_KEY))
            {
                var volume = PlayerPrefs.GetFloat(StaticConst.BGM_KEY);
                _seSource.volume = volume;
                _sePlayerVolumeController.CurrentVolume.Value = volume;
                _seSlider.value = volume;
            }
            else
            {
                SetSeVal(StaticConst.INIT_VOLUME);
            }
        }
        
        public void SetBgmVal(float sliderVal)
        {
            PlayerPrefs.SetFloat(StaticConst.BGM_KEY, sliderVal);
            PlayerPrefs.Save();
        }

        public void SetSeVal(float sliderVal)
        {
            PlayerPrefs.SetFloat(StaticConst.SE_KEY, sliderVal);
            PlayerPrefs.Save();
        }

        /// <summary>
        /// タイトルBGMのフェードイン処理
        /// </summary>
        public void TitleBGMFadeIn(bool isFirstPlay)
        {
            var currentVolume = _bgmSource.volume;
            _bgmSource.clip = _titleClip;
            _bgmSource.loop = true;
            _bgmSource.volume = 0.0f;
            if (!isFirstPlay)
            {
                var titleBgmBpm = 110.0f;
                // 1小節あたりにかける時間
                var secPerBar = StaticConst.MIN_AS_SEC * StaticConst.BEAT_NUM / titleBgmBpm;
                _bgmSource.time = secPerBar * 4.0f;
            }
            _bgmSource.Play();
            DOVirtual.Float(0.0f, currentVolume, _fadeInDuration, value => _bgmSource.volume = value).SetLink(gameObject);
        }

        /// <summary>
        /// タイトルBGMのフェードアウト処理
        /// </summary>
        public void TitleBGMFadeOut(float endVolume, float duration)
        {
            DOVirtual.Float(_bgmSource.volume, endVolume, duration, value => _bgmSource.volume = value)
                .SetLink(gameObject);
        }

        /// <summary>
        /// BGMソースのボリュームをフェードインさせる
        /// </summary>
        public void BGMFadeInVolume(float duration)
        {
            DOVirtual.Float(0.0f, _bgmSource.volume, duration, value => _bgmSource.volume = value)
                .SetLink(gameObject);
        }
        
        /// <summary>
        /// BGMをフェードアウトし停止する
        /// </summary>
        public void BGMFadeOutStop(float duration)
        {
            DOVirtual.Float(_bgmSource.volume, 0.0f, duration, value => _bgmSource.volume = value)
                .OnComplete(() =>
                {
                    _bgmSource.Stop();
                    _bgmSource.loop = false;
                    // キャッシュの値で保存 
                    _bgmSource.volume = PlayerPrefs.GetFloat(StaticConst.BGM_KEY);;
                })
                .SetLink(gameObject);
        }
    }
}