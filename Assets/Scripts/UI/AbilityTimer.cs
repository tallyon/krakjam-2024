using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class AbilityTimer : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI timerText;
        private float timeLeft;

        private void Awake()
        {
            HideTimer();
        }

        public void StartCooldown(float cooldownTime)
        {
            timeLeft = cooldownTime;
        }

        public void Update()
        {
            if (timeLeft < 0)
                return;
            timerText.gameObject.SetActive(true);
            timeLeft -= Time.deltaTime;
            timerText.text = string.Format("{0:0}", timeLeft);
            if (timeLeft <= 0)
            {
                HideTimer();
            }
        }

        private void HideTimer()
        {
            timerText.gameObject.SetActive(false);
        }
    }
}