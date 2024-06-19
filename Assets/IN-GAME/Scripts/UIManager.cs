using System.Diagnostics.Contracts;
using UnityEngine;

namespace safariSort
{

    public class UIManager : MonoBehaviour
    {
        [SerializeField] GameObject mainMenu;
        [SerializeField] GameObject GamePanel;

        public void PlayButton()
        {
            mainMenu.SetActive(false);
            GamePanel.SetActive(true);

            if (AudioManager.instance != null)
            {
                AudioManager.instance.PlayGameMusic();
            }
        }

        public void QuitButton()
        {
            Application.Quit();
        }
    }
}
