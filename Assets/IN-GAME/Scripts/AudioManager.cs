using System;
using System.Collections;
using UnityEngine;

namespace safariSort
{
    public class AudioManager : MonoBehaviour
    {
        public static AudioManager instance;

        public AudioClip mainMenuMusic;
        public AudioClip gameMusic;
        public AudioClip correctGuessClip;
        public AudioClip wrongGuessClip;
        public AudioClip timeUpClip;
        public AudioClip winClip;
        public AudioClip errorClip;
        public AudioClip ClickClip;

        [SerializeField] AudioSource musicSource;
        [SerializeField] AudioSource soundEffectSource;

        //USED FOR FADE EFFECT
        private float targetVolume = 1.0f;
        private float fadeSpeed = 0f;

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

        public void CrossfadeToGameMusic()
        {
            if (musicSource.clip != gameMusic)
            {
                CrossfadeCoroutine(gameMusic);
            }
        }

        public void CrossfadeToMainMenuMusic()
        {
            if (musicSource.clip != mainMenuMusic)
            {
               CrossfadeCoroutine(mainMenuMusic);
            }
        }

        private void CrossfadeCoroutine(AudioClip newClip)
        {
            Debug.Log("FADE");
           
            musicSource.Stop();
            musicSource.clip = newClip;
            musicSource.Play();

           
            Debug.Log("FADE end");
        }

        #endregion
    }
}