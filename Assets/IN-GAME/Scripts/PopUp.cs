using DG.Tweening;
using UnityEngine;

namespace safariSort
{
    public class PopUp : MonoBehaviour
    {
        [SerializeField] float Time = 0.5f;
        [SerializeField] Transform target;

        private void OnEnable()
        {
            if (target == null)
            {
                target = transform;
            }

            target.localScale = Vector3.zero;
            target.DOScale(Vector3.one, Time).SetUpdate(true);

        }

    }

}
