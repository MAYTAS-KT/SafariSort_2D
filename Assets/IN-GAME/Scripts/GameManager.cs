using UnityEngine;


namespace safariSort
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager instance;

       
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


        public void CorrectGuess()
        {
            Debug.Log("Correct Guess");
        }

        public void WrongGuess()
        {
            Debug.Log("Wrong Guess");
        }

        public void AllAnimalSorted()
        {
            Debug.Log("ALL ANIMAL SORTED");
        }

    }

}
