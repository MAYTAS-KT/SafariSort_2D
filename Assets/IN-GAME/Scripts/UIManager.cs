using System.Diagnostics.Contracts;
using UnityEngine;

namespace safariSort
{

    public class UIManager : MonoBehaviour
    {
        [SerializeField] GameObject mainMenu;
        [SerializeField] GameObject GamePanel;
        private AudioManager audioManager;

        private void Start()
        {
            if (AudioManager.instance != null)
            {
                audioManager = AudioManager.instance;
            }
        }
        public void PlayButton()
        {
            mainMenu.SetActive(false);
            GamePanel.SetActive(true);

            audioManager.PlayClicKSound();
            audioManager.CrossfadeToGameMusic();

        }

        public void QuitButton()
        {
            audioManager.PlayClicKSound();
            Application.Quit();
        }
    }
}
