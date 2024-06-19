using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace safariSort
{
    public class DragAndDrop : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
    {
       
        [SerializeField] HabitatType habitatType;

        private int startChildIndex;
        private Transform parentToReturnTo = null;
        private CanvasGroup canvasGroup;

        private void Start()
        {
          canvasGroup=GetComponent<CanvasGroup>();
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
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
            Debug.Log(eventData.pointerCurrentRaycast.gameObject.name);
            if (eventData.pointerCurrentRaycast.gameObject != null && eventData.pointerCurrentRaycast.gameObject.TryGetComponent(out Habitat habitat))
            {
               
                Debug.Log("Enetred");
                if (habitat.habitatType==habitatType)
                {

                    GameManager.instance.CorrectGuess();//selected correct habitat
                    Destroy(gameObject);
                }
                else
                {
                    GameManager.instance.WrongGuess();//selected wrong habitat
                    Destroy(gameObject);
                }

                if (parentToReturnTo.transform.GetChildCount()==0)
                {
                    GameManager.instance.AllAnimalSorted();
                }
            }
            else
            {
                canvasGroup.blocksRaycasts = true;
                transform.SetParent(parentToReturnTo, false);
                transform.SetSiblingIndex(startChildIndex);
                transform.localScale=Vector3.one;
            }

        }

    }
}
