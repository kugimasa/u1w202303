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
        [SerializeField] private float _fadeOutDuration;

        private void Start()
        {
            // 音量設定をロードする
            LoadVolumeSetting();
            // タイトルをフェードイン
            TitleBGMFadeIn();
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

            if (PlayerPrefs.HasKey(StaticConst.SE_KEY))
            {
                var volume = PlayerPrefs.GetFloat(StaticConst.BGM_KEY);
                _seSource.volume = volume;
                _sePlayerVolumeController.CurrentVolume.Value = volume;
                _seSlider.value = volume;
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
        public void TitleBGMFadeIn()
        {
            var currentVolume = _bgmSource.volume;
            _bgmSource.clip = _titleClip;
            _bgmSource.loop = true;
            _bgmSource.volume = 0.0f;
            _bgmSource.Play();
            DOVirtual.Float(0.0f, currentVolume, _fadeInDuration, value => _bgmSource.volume = value).SetLink(gameObject);
        }

        /// <summary>
        /// タイトルBGMのフェードアウト処理
        /// </summary>
        public void TitleBGMFadeOut()
        {
            var currentVolume = _bgmSource.volume;
            DOVirtual.Float(currentVolume, 0.0f, _fadeOutDuration, value => _bgmSource.volume = value).SetLink(gameObject);
            _bgmSource.Stop();
            _bgmSource.loop = false;
            _bgmSource.volume = currentVolume;
        }
    }
}