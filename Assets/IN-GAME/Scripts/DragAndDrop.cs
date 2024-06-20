using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using static safariSort.GameData;
using static UnityEngine.GraphicsBuffer;

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
                bool hasGussedRight=false;
                Transform selectedHabitat = eventData.pointerCurrentRaycast.gameObject.transform;

                foreach (var item in possibleHabitat)//Check if any habitat match
                {
                    if (item==habitat.habitatType)
                    {
                        hasGussedRight = true;
                    }
                }

                if (hasGussedRight)
                {
                    PerformCorrectAnimation(selectedHabitat,habitat.habitatFrame);
                    PerformScaleIntoImageAnimation();
                    AudioManager.instance.PlayCorrectGuessSound();
                }
                else
                {
                    PerformVibrationAnimation(selectedHabitat,habitat.habitatFrame);
                    PerformScaleOutImageAnimation();
                    AudioManager.instance.PlayWrongGuessSound();//selected wrong habitat
                }

                if (parentToReturnTo.transform.GetChildCount()==0)
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

        private void PerformVibrationAnimation(Transform target, Image frame)
        {
            Color originalColor= frame.color;
            frame.color= Color.red;
            target.DOShakePosition(0.5f, 20f, 10, 90f).OnComplete(()=>frame.color=originalColor);
        }

        private void PerformCorrectAnimation(Transform target,Image frame)
        {
            Color originalColor = frame.color;
            frame.color = Color.green;

            float duration = 0.25f;
            Vector3 originalScale = target.localScale; 
            Vector3 targetScale = originalScale * 1.2f; 

            Sequence sequence = DOTween.Sequence();
            sequence.Append(target.DOScale(targetScale, duration / 2).SetEase(Ease.OutQuad));
            sequence.Append(target.DOScale(originalScale, duration / 2).SetEase(Ease.InQuad));
            sequence.OnComplete(() => frame.color=originalColor);
            sequence.Play();
        }

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
