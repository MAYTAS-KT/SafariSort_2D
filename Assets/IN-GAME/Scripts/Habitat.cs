using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static safariSort.GameData;

namespace safariSort
{
    public class Habitat : MonoBehaviour
    {
         [SerializeField] HabitatType habitatType;
         [SerializeField] Image habitatImage;
         [SerializeField] TextMeshProUGUI HabitatName;
         [SerializeField] Image habitatFrame;


        public HabitatType getHabitatType()
        {
            return habitatType;
        }

        public void SetUpHabitat(HabitatData habitatData)
        {
            // Assign properties to the DragAndDrop component
            HabitatName.text = habitatData.habitatName;
            habitatType = habitatData.habitatType;
            habitatImage.sprite = habitatData.habitatSprite;
        }

        #region DoTween Animations

        public void PerformVibrationAnimation()
        {
            Color originalColor = habitatFrame.color;
            habitatFrame.color = Color.red;
            transform.DOShakePosition(0.5f, 20f, 10, 90f).OnComplete(() => habitatFrame.color = originalColor);
        }

        public void PerformCorrectAnimation()
        {
            Color originalColor = habitatFrame.color;
            habitatFrame.color = Color.green;

            float duration = 0.25f;
            Vector3 originalScale = transform.localScale;
            Vector3 targetScale = originalScale * 1.2f;

            Sequence sequence = DOTween.Sequence();
            sequence.Append(transform.DOScale(targetScale, duration / 2).SetEase(Ease.OutQuad));
            sequence.Append(transform.DOScale(originalScale, duration / 2).SetEase(Ease.InQuad));
            sequence.OnComplete(() => habitatFrame.color = originalColor);
            sequence.Play();
        }

        #endregion
    }

}

