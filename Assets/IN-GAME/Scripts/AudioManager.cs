using System;
using System.Collections;
using UnityEngine;

namespace safariSort
{
    public class AudioManager : MonoBehaviour
    {
        public static AudioManager instance;

        [Header("AUIDO CLIPS")]
        public AudioClip mainMenuMusic;
        public AudioClip gameMusic;
        public AudioClip correctGuessClip;
        public AudioClip wrongGuessClip;
        public AudioClip timeUpClip;
        public AudioClip winClip;
        public AudioClip errorClip;
        public AudioClip ClickClip;

        [Header("AUIDO SOURCE")]
        [SerializeField] AudioSource musicSource;
        [SerializeField] AudioSource soundEffectSource;

        [Header("PLAYER PREFS")]
        public const string AudioPrefKey = "AudioEnabled";

        private void Awake()
        {
            // Singleton pattern to ensure only one instance of AudioManager exists
            if (instance == null)
            {
                instance = this;
                DontDestroyOnLoad(gameObject); // Keep AudioManager across scenes
            }
            else
            {
                Destroy(gameObject);
            }

            if (musicSource ==null || soundEffectSource==null)
            {
                Debug.LogError("AudioSource Missing");
            }
            else
            {
              // Music source setup
              musicSource.loop = true;
            }
        }

        private void Start()
        {
            // Load audio preference on start
            bool audioEnabled = PlayerPrefs.GetInt(AudioPrefKey, 1) == 1; // Default to true if not set

            // Set initial audio state
            AudioListener.volume = audioEnabled ? 1 : 0;

            PlayMainMenuMusic();
        }

       
        public void PlayClicKSound()
        {
            PlaySoundEffect(ClickClip);
        }

       
        private void PlayMusic(AudioClip clip)
        {
            if (musicSource.clip != clip)
            {
                musicSource.clip = clip;
                musicSource.Play();
            }
        }

        public void PlayCorrectGuessSound()
        {
            PlaySoundEffect(correctGuessClip);
        }

        public void PlayWrongGuessSound()
        {
            PlaySoundEffect(wrongGuessClip);
        }

        public void PlayTimeUpSound()
        {
            PlaySoundEffect(timeUpClip);
        }

        public void PlayWinSound()
        {
            PlaySoundEffect(winClip);
        }

        public void PlayErrorSound()
        {
            PlaySoundEffect(errorClip);
        }

        private void PlaySoundEffect(AudioClip clip)
        {
            soundEffectSource.PlayOneShot(clip);
        }

        public void PauseMusic()
        {
            musicSource.Pause();
        }

        public void ResumeMusic()
        {
            musicSource.UnPause();
        }

        public void StopMusic()
        {
            musicSource.Stop();
        }

        public bool IsMainMenuMusicPlaying()
        {
            return musicSource.clip == mainMenuMusic && musicSource.isPlaying;
        }

        public bool IsGameMusicPlaying()
        {
            return musicSource.clip == gameMusic && musicSource.isPlaying;
        }

        #region BG MUSIC
        public void PlayMainMenuMusic()
        {
            if (musicSource.clip != mainMenuMusic)
            {
                musicSource.clip = mainMenuMusic;
                musicSource.loop = true;
                musicSource.Play();
            }
        }

        public void PlayGameMusic()
        {
            if (musicSource.clip != gameMusic)
            {
                musicSource.clip = gameMusic;
                musicSource.loop = true;
                musicSource.Play();
            }
        }

        public void ChangeToGameMusic()
        {
            if (musicSource.clip != gameMusic)
            {
                BGAudioChange(gameMusic);
            }
        }

        public void ChangeToMainMenuMusic()
        {
            if (musicSource.clip != mainMenuMusic)
            {
               BGAudioChange(mainMenuMusic);
            }
        }

        private void BGAudioChange(AudioClip newClip)
        {
            musicSource.Stop();
            musicSource.clip = newClip;
            musicSource.Play();
        }

        #endregion

        #region Audio Button

        public void ToggleAudio()
        {
            bool audioEnabled = !IsAudioEnabled();
            SetAudioEnabled(audioEnabled);
        }

        public void SetAudioEnabled(bool enabled)
        {
            PlayerPrefs.SetInt(AudioPrefKey, enabled ? 1 : 0);
            PlayerPrefs.Save();

            AudioListener.volume = enabled ? 1 : 0;
        }

        public bool IsAudioEnabled()
        {
            return PlayerPrefs.GetInt(AudioPrefKey, 1) == 1;
        }

        #endregion
    }
}