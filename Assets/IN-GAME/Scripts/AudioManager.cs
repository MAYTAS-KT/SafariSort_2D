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

        public void PlayMainMenuMusic()
        {
            PlayMusic(mainMenuMusic);
        }

        public void PlayClicKSound()
        {
            PlaySoundEffect(ClickClip);
        }

        public void PlayGameMusic()
        {
            PlayMusic(gameMusic);
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
    }
}