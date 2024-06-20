using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using static safariSort.GameData;

namespace safariSort
{
    public class DragAndDrop : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
    {
        [Header("Animal Data")]
        public HabitatType[] possibleHabitat;
        public Image animalImage;
        public TextMeshProUGUI animalText;

        private int startChildIndex;
        private Transform parentToReturnTo = null;
        private CanvasGroup canvasGroup;

        private void Start()
        {
            canvasGroup = GetComponent<CanvasGroup>();
        }

        public void SetUpPrefab(AnimalData animalData)
        {
            possibleHabitat = animalData.possibleHabitat;
            animalImage.sprite = animalData.animalSprite;
            animalText.text=animalData.animalName;
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            if (AudioManager.instance != null)
            {
                AudioManager.instance.PlayClicKSound();
            }
            GameManager.instance.AnimalLayoutGroup(false);//Stop Arranging Animal Layout group
            animalText.enabled=false;
            transform.localScale *= 1.25f;
            startChildIndex = transform.GetSiblingIndex();
            parentToReturnTo = transform.parent;
            transform.SetParent(transform.root);
        }

        public void OnDrag(PointerEventData eventData)
        {

            transform.position = eventData.position;
            canvasGroup.alpha = 0.75f;
            canvasGroup.blocksRaycasts = false;
        }

        [System.Obsolete]
        public void OnEndDrag(PointerEventData eventData)
        {
            canvasGroup.alpha = 1f;
            if (eventData.pointerCurrentRaycast.gameObject != null && eventData.pointerCurrentRaycast.gameObject.TryGetComponent(out Habitat habitat))
            {
               
                Debug.Log("Enetred");

                for (int i = 0; i < possibleHabitat.Length; i++)
                {
                    HabitatType item = possibleHabitat[i];
                    if (habitat.habitatType == item)
                    {

                        GameManager.instance.CorrectGuess();//selected correct habitat
                        Destroy(gameObject);
                    }
                    else if(i==possibleHabitat.Length-1)
                    {
                        GameManager.instance.WrongGuess();//selected wrong habitat
                        Destroy(gameObject);
                    }
                }

                if (parentToReturnTo.transform.GetChildCount()==0)
                {
                    GameManager.instance.AllAnimalSorted();
                }
            }
            else//Return back to original pos
            {
                if (AudioManager.instance!=null)
                {
                    AudioManager.instance.PlayErrorSound();
                }
                animalText.enabled = true;
                canvasGroup.blocksRaycasts = true;
                transform.SetParent(parentToReturnTo, false);
                transform.SetSiblingIndex(startChildIndex);
                transform.localScale=Vector3.one;
            }

            GameManager.instance.AnimalLayoutGroup(true);//Arrange Animal Layout group
        }

    }
}
