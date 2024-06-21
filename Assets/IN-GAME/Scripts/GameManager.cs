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

        public static GameManager instance;
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
            shuffledAnimals = new List<AnimalData>(gameData.animals);
            shuffledHabitats = new List<HabitatData>(gameData.habitats);
            {
                UIManager.loadGame += OnLoadGame;
            }

        }

        public void OnLoadGame()
        {
            ShuffleList(shuffledAnimals);
            ShuffleList(shuffledHabitats);

            SpawnAnimals();
            SpawnHabitats();

            gameTimer.ResetAndStartTimer();
        }

        public void SpawnAnimals()
        {
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
                newAnimal.GetComponent<DragAndDrop>().SetUpPrefab(animalData);
            }
            
        }

        public void SpawnHabitats()
        {
            if (habitatGroupLayout.childCount > 0)//USED FOR RESETTING 
            {
                foreach (Transform child in habitatGroupLayout)
                {
                    Destroy(child.gameObject);
                }
            }

            foreach (var habitatData in shuffledHabitats)
            {
                // Instantiate the animal prefab and get its Habitat component
                GameObject newHabitat = Instantiate(gameData.habitatPrefab, habitatGroupLayout);
                newHabitat.GetComponent<Habitat>().SetUpHabitat(habitatData);
            }
        }

        public void AllAnimalSorted()
        {
            Debug.Log("ALL ANIMAL SORTED");
            gameTimer.StopTimer();
        
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


        public void OnDestroy()
        {
            UIManager.loadGame -= OnLoadGame;
        }

    }

}
