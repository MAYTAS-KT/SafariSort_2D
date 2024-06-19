using System;
using UnityEngine;
using UnityEngine.UI;


namespace safariSort
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] GameTimer gameTimer;
        [SerializeField] GameData gameData;
        public static GameManager instance;
        private AudioManager audioManager;
        [SerializeField] LayoutGroup animalLayoutGroup;
        [SerializeField] LayoutGroup habitatGroupLayout;

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
            if (AudioManager.instance != null)
            {
                audioManager = AudioManager.instance;
            }

            SpawnAnimals();
            SpawnHabitats();


            gameTimer.enabled = true;
            gameTimer.onTimeUp.AddListener(TimeUp);

        }

       

        public void SpawnAnimals()
        {
            DragAndDrop temp;
            foreach (var animalData in gameData.animals)
            {
                // Instantiate the animal prefab and get its DragAndDrop component
                GameObject newAnimal = Instantiate(gameData.animalPrefab, animalLayoutGroup.transform);
                temp = newAnimal.GetComponent<DragAndDrop>();

                // Assign properties to the DragAndDrop component
                temp.animalText.text = animalData.animalName;
                temp.possibleHabitat = animalData.possibleHabitat;
                temp.animalImage.sprite = animalData.animalSprite;
            }
        }

        public void SpawnHabitats()
        {
            Habitat temp;
            foreach (var habitatData in gameData.habitats)
            {
                // Instantiate the animal prefab and get its DragAndDrop component
                GameObject newAnimal = Instantiate(gameData.habitatPrefab, habitatGroupLayout.transform);
                temp = newAnimal.GetComponent<Habitat>();

                // Assign properties to the DragAndDrop component
                temp.HabitatName.text = habitatData.habitatName;
                temp.habitatType = habitatData.habitatType;
                temp.habitatImage.sprite = habitatData.habitatSprite;
            }
        }


        public void TimeUp()
        {
            audioManager.PlayTimeUpSound();
            Time.timeScale = 0.0f;
            //endPanel
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
            gameTimer.StopTimer();
            audioManager.PlayWinSound();

        }

        public void AnimalLayoutGroup(bool isEnabled)
        {
            animalLayoutGroup.enabled = isEnabled;
        }

    }

}
