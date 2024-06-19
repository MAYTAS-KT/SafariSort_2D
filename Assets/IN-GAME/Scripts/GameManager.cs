using UnityEngine;
using UnityEngine.UI;


namespace safariSort
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager instance;
        private AudioManager audioManager;
        [SerializeField] LayoutGroup animalLayoutGroup;

        private void Awake()
        {
            // Ensure there is only one instance of the GameManager
            if (instance == null)
            {
                instance = this;
            }
            else
            {
                Destroy(gameObject);
            }
        }
        private void Start()
        {
            if (AudioManager.instance!=null)
            {
                audioManager = AudioManager.instance;
            }
        }

        public void CorrectGuess()
        {
            Debug.Log("Correct Guess");
            audioManager.PlayCorrectGuessSound();
        }

        public void WrongGuess()
        {
            Debug.Log("Wrong Guess");
            audioManager.PlayWrongGuessSound();
        }

        public void AllAnimalSorted()
        {
            Debug.Log("ALL ANIMAL SORTED");
            audioManager.PlayWinSound();
        }

        public void AnimalLayoutGroup(bool isEnabled)
        {
            animalLayoutGroup.enabled = isEnabled;
        }

    }

}
