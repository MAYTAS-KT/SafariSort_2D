using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace safariSort
{

    public class UIManager : MonoBehaviour
    {

        [Header("PANELS")]
        [SerializeField] GameObject mainMenu;
        [SerializeField] GameObject GamePanel;
        [SerializeField] GameObject PausePanel;
        [SerializeField] GameObject GameOverPOpUp;

        [Header("BUTTONS")]
        [SerializeField] Button playBtn;
        [SerializeField] Button optionsBtn;
        [SerializeField] Button quitBtn;
        [SerializeField] Button settingBtn;
        [SerializeField] Button resumeBtn;
        [SerializeField] Button mainMenuBtn;
        [SerializeField] Button audioBtn;
        [SerializeField] Button restartBtn;
        [SerializeField] Button restartBtn_GameOver;
        [SerializeField] Button MenuBtn_GameOver;

        [Header("AUDIO TOGGLE")]
        [SerializeField] Image audioIcon;
        [SerializeField] Sprite audioOn;
        [SerializeField] Sprite audioOff;

        [Header("TEXT")]
        [SerializeField] TextMeshProUGUI PausePanelText;
        [SerializeField] TextMeshProUGUI BestTimeText;
        [SerializeField] TextMeshProUGUI BestTimeText_GameOver;
        [SerializeField] TextMeshProUGUI YourTimeText_GameOver;

        [Header("SCRIPT REF")]
        [SerializeField] GameTimer gameTimer;

        
        private AudioManager audioManager;

        private void Start()
        {
            if (AudioManager.instance != null)
            {
                audioManager = AudioManager.instance;
                SetAudioIcon();
            }
            gameTimer.onTimeStop.AddListener(ShowWinPopUP);
            SetBestTime();

            playBtn.onClick.AddListener(Play);
            optionsBtn.onClick.AddListener(Options);
            quitBtn.onClick.AddListener(Quit);
            settingBtn.onClick.AddListener(Setting);
            mainMenuBtn.onClick.AddListener(MainMenu);
            MenuBtn_GameOver.onClick.AddListener(MainMenu);
            mainMenuBtn.onClick.AddListener(audioManager.ChangeToMainMenuMusic);
            MenuBtn_GameOver.onClick.AddListener(audioManager.ChangeToMainMenuMusic);
            resumeBtn.onClick.AddListener(Resume);
            audioBtn.onClick.AddListener(audioManager.ToggleAudio);
            audioBtn.onClick.AddListener(SetAudioIcon);
            restartBtn.onClick.AddListener(RestartGame);
            restartBtn_GameOver.onClick.AddListener(RestartGame);
        }

        public void ShowWinPopUP(float playerTime)
        {
            int minutes = Mathf.FloorToInt(playerTime / 60);
            int seconds = Mathf.FloorToInt(playerTime % 60);
            YourTimeText_GameOver.text = string.Format("Your Time - {0:00}:{1:00}", minutes, seconds);
            SetBestTime();
            GameOverPOpUp.SetActive(true);
            PausePanel.SetActive(false);

        }

        private void RestartGame()
        {
            Time.timeScale = 1;
            audioManager.PlayClicKSound();
            GameManager.instance.LoadGame();
            GameOverPOpUp.SetActive(false);
            GamePanel.SetActive(true);
            mainMenu.SetActive(false);
            PausePanel.SetActive(false);

        }

        private void SetAudioIcon()
        {
            if (audioManager.IsAudioEnabled())
            {
                audioIcon.sprite = audioOn;
            }
            else
            {
                audioIcon.sprite = audioOff;
            }
        }

        private void Options()
        {

            PausePanelText.text = "OPTIONS";
            resumeBtn.gameObject.SetActive(false);
            restartBtn.gameObject.SetActive(false);
            mainMenu.SetActive(false);
            PausePanel.gameObject.SetActive(true);
            audioManager.PlayClicKSound();
        }

        private void Play()
        {
            mainMenu.SetActive(false);
            GamePanel.SetActive(true);
            GameManager.instance.LoadGame();
            audioManager.PlayClicKSound();
            audioManager.ChangeToGameMusic();

        }

        private void Quit()
        {
            Application.Quit();
        }

        private void Setting()
        {
            PausePanelText.text = "PAUSED";
            Time.timeScale = 0;
            resumeBtn.gameObject.SetActive(true);
            restartBtn.gameObject.SetActive(true);
            PausePanel.SetActive(true);
            GamePanel.SetActive(false);
            audioManager.PlayClicKSound();
        }

        private void Resume()
        {
            Time.timeScale = 1;
            GamePanel.SetActive(true);
            PausePanel.SetActive(false);
            audioManager.PlayClicKSound();
        }

        private void MainMenu()
        {
            Time.timeScale = 1;
            mainMenu.SetActive(true);
            GamePanel.SetActive(false);
            PausePanel.SetActive(false);
            GameOverPOpUp.SetActive(false);
            audioManager.PlayClicKSound();
        }

        private void SetBestTime()
        {
            float time = PlayerPrefs.GetFloat(gameTimer.BestTimePrefKey, 0);
            int minutes = Mathf.FloorToInt(time / 60);
            int seconds = Mathf.FloorToInt(time % 60);
            BestTimeText.text = string.Format("Best Time - {0:00}:{1:00}", minutes, seconds);
            BestTimeText_GameOver.text = string.Format("Best Time - {0:00}:{1:00}", minutes, seconds);
        }

    }
}
