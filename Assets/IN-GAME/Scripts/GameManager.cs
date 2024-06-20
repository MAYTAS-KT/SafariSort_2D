using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static safariSort.GameData;


namespace safariSort
{
    public class GameManager : MonoBehaviour
    {

        [SerializeField] GameTimer gameTimer;
        [SerializeField] GameData gameData;
        [SerializeField] LayoutGroup animalLayoutGroup;
        [SerializeField] Transform habitatGroupLayout;
        [SerializeField] Image visualImageRef;

        [Header("COLOR REF")]
        [SerializeField] Color originalColor;
        [SerializeField] Color correctGuessColor;
        [SerializeField] Color WrongGuessColor;

        public static GameManager instance;

        private AudioManager audioManager;
        private UIManager uiManager;

        private List<AnimalData> shuffledAnimals;
        private List<HabitatData> shuffledHabitats;

        

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
            originalColor=visualImageRef.color;
            shuffledAnimals = new List<AnimalData>(gameData.animals);
            shuffledHabitats = new List<HabitatData>(gameData.habitats);

        }

        public void LoadGame()
        {
            ShuffleList(shuffledAnimals);
            ShuffleList(shuffledHabitats);

            SpawnAnimals();
            SpawnHabitats();


            gameTimer.ResetAndStartTimer();
        }

        public void SpawnAnimals()
        {
            DragAndDrop temp;

            if (animalLayoutGroup.transform.childCount >0)//USED FOR RESETTING 
            {
                foreach (Transform child in animalLayoutGroup.transform)
                {
                    Destroy(child.gameObject);
                }
            }

            foreach (var animalData in shuffledAnimals)
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

            if (habitatGroupLayout.childCount > 0)//USED FOR RESETTING 
            {
                foreach (Transform child in habitatGroupLayout)
                {
                    Destroy(child.gameObject);
                }
            }

            foreach (var habitatData in shuffledHabitats)
            {
                // Instantiate the animal prefab and get its DragAndDrop component
                GameObject newAnimal = Instantiate(gameData.habitatPrefab, habitatGroupLayout);
                temp = newAnimal.GetComponent<Habitat>();

                // Assign properties to the DragAndDrop component
                temp.HabitatName.text = habitatData.habitatName;
                temp.habitatType = habitatData.habitatType;
                temp.habitatImage.sprite = habitatData.habitatSprite;
            }
        }

        public void CorrectGuess()
        {
            Debug.Log("Correct Guess");
            audioManager.PlayCorrectGuessSound();
            ShowCorrectVisual();

        }

        public void WrongGuess()
        {
            Debug.Log("Wrong Guess");
            audioManager.PlayWrongGuessSound();
            ShowWrongVisual();
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

        #region Shuffle Logic

        private void ShuffleList<T>(List<T> list)
        {
            for (int i = list.Count - 1; i > 0; i--)
            {
                int randomIndex = UnityEngine.Random.Range(0, i + 1);
                T temp = list[i];
                list[i] = list[randomIndex];
                list[randomIndex] = temp;
            }
        }

        #endregion

        #region Visual
        public void ShowCorrectVisual()
        {
            visualImageRef.color=correctGuessColor;
            Invoke(nameof(setoriginalColor), 0.5f);
        }
        public void ShowWrongVisual()
        {
            visualImageRef.color = WrongGuessColor;
            Invoke(nameof(setoriginalColor), 0.5f);
        }
        private void setoriginalColor()
        {
           visualImageRef.color=originalColor;
        }
        #endregion

    }

}
