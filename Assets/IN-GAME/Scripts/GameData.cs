using System.Collections.Generic;
using UnityEngine;

namespace safariSort
{
    [CreateAssetMenu(fileName = "NewGameData", menuName = "GameData")]
    public class GameData : ScriptableObject
    {
        [System.Serializable]
        public class HabitatData
        {
            public string habitatName;
            public HabitatType habitatType;
            public Sprite habitatSprite; 
        }

        [System.Serializable]
        public class AnimalData
        {
            public string animalName;
            public Sprite animalSprite;
            public HabitatType[] possibleHabitat;
        }

        public GameObject habitatPrefab;
        public GameObject animalPrefab;
        public List<HabitatData> habitats;
        public List<AnimalData> animals;

    }
}