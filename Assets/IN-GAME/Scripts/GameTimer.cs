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

        public UnityEvent<float> onTimeStop; // Event to trigger when time is up
        private IEnumerator timerRef;
        private float playerTime;

        [Header("PLAYER PREFS")]
        public  string BestTimePrefKey = "BestTime";

        public void ResetAndStartTimer()
        {
            if (timerRef != null)
            {
                StopCoroutine(timerRef);
            }

            playerTime =0;
            timerRef = TimerCoroutine();
            StartCoroutine(timerRef);
        }

        private IEnumerator TimerCoroutine()
        {
            while (true)
            {
                playerTime += Time.deltaTime;
                UpdateTimerUI();
                yield return null;
            }
        }

        private void UpdateTimerUI()
        {
            int minutes = Mathf.FloorToInt(playerTime / 60);
            int seconds = Mathf.FloorToInt(playerTime % 60);
            timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
        }

        public void StopTimer()
        {
           
            if (timerRef != null)
            {
                StopCoroutine(timerRef);
            }

            if (PlayerPrefs.GetFloat(BestTimePrefKey, 0)==0)
            {
                PlayerPrefs.SetFloat(BestTimePrefKey, playerTime);
                PlayerPrefs.Save();
                return;
            }

            if (playerTime < PlayerPrefs.GetFloat(BestTimePrefKey, 0))
            {
                PlayerPrefs.SetFloat(BestTimePrefKey, playerTime);
                PlayerPrefs.Save();
                print("TIME UPDATED");
            }

            onTimeStop.Invoke(playerTime);
        }

    }
}