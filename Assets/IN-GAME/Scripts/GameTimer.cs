using UnityEngine.Events;
using UnityEngine;
using TMPro;
using System.Collections;
using Unity.VisualScripting;

namespace safariSort
{
    public class GameTimer : MonoBehaviour
    {
        [Header("TEXT")]
        public TextMeshProUGUI timerText;

        public UnityEvent onTimeUp; // Event to trigger when time is up
        private IEnumerator timerRef;
        private float currentTime;

        [Header("PLAYER PREFS")]
        public  string BestTimePrefKey = "BestTime";

        private void Start()
        {
            currentTime =0;
            timerRef = TimerCoroutine();
            StartCoroutine(timerRef);
        }

        private IEnumerator TimerCoroutine()
        {
            while (true)
            {
                currentTime += Time.deltaTime;
                UpdateTimerUI();
                yield return null;
            }
        }

        private void UpdateTimerUI()
        {
            int minutes = Mathf.FloorToInt(currentTime / 60);
            int seconds = Mathf.FloorToInt(currentTime % 60);
            timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
        }

        public void StopTimer()
        {
           
            if (timerRef != null)
            {
                StopCoroutine(timerRef);
            }
            print("TIME UPDATED"+currentTime);

            if (PlayerPrefs.GetFloat(BestTimePrefKey, 0)==0)
            {
                PlayerPrefs.SetFloat(BestTimePrefKey, currentTime);
                PlayerPrefs.Save();
                return;
            }

            if (currentTime < PlayerPrefs.GetFloat(BestTimePrefKey, 0))
            {
                PlayerPrefs.SetFloat(BestTimePrefKey, currentTime);
                PlayerPrefs.Save();
                print("TIME UPDATED");
            }
        }
    }
}