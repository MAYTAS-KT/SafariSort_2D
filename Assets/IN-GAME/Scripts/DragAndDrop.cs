using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using static safariSort.GameData;

namespace safariSort
{
    public class DragAndDrop : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
    {
        [Header("Animal Data")]
        [SerializeField] HabitatType[] possibleHabitat;
        [SerializeField] Image animalImage;
        [SerializeField] TextMeshProUGUI animalText;

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

        
        public void OnEndDrag(PointerEventData eventData)
        {
            canvasGroup.alpha = 1f;
            if (eventData.pointerCurrentRaycast.gameObject != null && eventData.pointerCurrentRaycast.gameObject.TryGetComponent(out Habitat habitat))
            {
                bool hasGussedRight=false;
                foreach (var item in possibleHabitat)//Check if any habitat match
                {
                    if (item==habitat.getHabitatType())
                    {
                        hasGussedRight = true;
                    }
                }

                if (hasGussedRight)
                {
                    habitat.PerformCorrectAnimation();
                    PerformScaleIntoImageAnimation();
                    AudioManager.instance.PlayCorrectGuessSound();
                }
                else
                {
                    habitat.PerformVibrationAnimation();
                    PerformScaleOutImageAnimation();
                    AudioManager.instance.PlayWrongGuessSound();//selected wrong habitat
                }

                if (parentToReturnTo.transform.childCount==0)
                {
                    GameManager.instance.AllAnimalSorted();
                    AudioManager.instance.PlayWinSound();
                }

            }
            else//Return back to original pos
            {
                AudioManager.instance.PlayErrorSound();
                animalText.enabled = true;
                canvasGroup.blocksRaycasts = true;
                transform.SetParent(parentToReturnTo, false);
                transform.SetSiblingIndex(startChildIndex);
                transform.localScale=Vector3.one;
            }

            GameManager.instance.AnimalLayoutGroup(true);//Arrange Animal Layout group
        }

        #region Dotween Functions

       
        private void PerformScaleIntoImageAnimation()
        {
            transform.DOScale(Vector3.zero, 0.5f).SetEase(Ease.InOutQuad).OnComplete(() =>
            Destroy(gameObject)
            );
        }

        private void PerformScaleOutImageAnimation()
        {
            animalImage.DOFade(0, 0.25f);
            transform.DOScale(1.25f, 0.25f).OnComplete(() => Destroy(gameObject));

        }

        #endregion 
    }
}
