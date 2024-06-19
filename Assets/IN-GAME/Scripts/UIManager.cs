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

        [Header("BUTTONS")]
        [SerializeField] Button playBtn;
        [SerializeField] Button optionsBtn;
        [SerializeField] Button quitBtn;
        [SerializeField] Button settingBtn;
        [SerializeField] Button resumeBtn;
        [SerializeField] Button mainMenuBtn;
        [SerializeField] Button audioBtn;
        [SerializeField] Button restartBtn;

        [Header("AUDIO TOGGLE")]
        [SerializeField] Image audioIcon;
        [SerializeField] Sprite audioOn;
        [SerializeField] Sprite audioOff;
        

        [SerializeField] TextMeshProUGUI PausePanelText;
        private AudioManager audioManager;

        private void Start()
        {
            if (AudioManager.instance != null)
            {
                audioManager = AudioManager.instance;
                SetAudioIcon();
            }

            playBtn.onClick.AddListener(Play);
            optionsBtn.onClick.AddListener(Options);
            quitBtn.onClick.AddListener(Quit);
            settingBtn.onClick.AddListener(Setting);
            mainMenuBtn.onClick.AddListener(MainMenu);
            mainMenuBtn.onClick.AddListener(audioManager.ChangeToMainMenuMusic);
            resumeBtn.onClick.AddListener(Resume);
            audioBtn.onClick.AddListener(audioManager.ToggleAudio);
            audioBtn.onClick.AddListener(SetAudioIcon);
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
            resumeBtn.gameObject.SetActive(true);
            restartBtn.gameObject.SetActive(true);
            PausePanel.SetActive(true);
            GamePanel.SetActive(false);
            audioManager.PlayClicKSound();
        }

        private void Resume()
        {
            GamePanel.SetActive(true);
            PausePanel.SetActive(false);
            audioManager.PlayClicKSound();
        }

        private void MainMenu()
        {
            mainMenu.SetActive(true);
            GamePanel.SetActive(false);
            PausePanel.SetActive(false);
            audioManager.PlayClicKSound();
        }
    }
}
