using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

namespace UI
{
    public class Timer : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI timerText;
        [SerializeField] private float timeInSeconds;
        private Sequence tweenSequence;
        private float currentTime;
        private RectTransform textrect;
        private bool animStarted;

        private void Awake()
        {
            textrect = timerText.GetComponent<RectTransform>();
            Reset();
        }

        private void Update()
        {
            currentTime -= Time.deltaTime;
            if (currentTime > 0)
            {
                DrawTimer(currentTime);
            }

            if (currentTime <= 10 && tweenSequence == null && !animStarted)
            {
                PlayAnimation();
            }

            if (currentTime <= 0)
            {
                //tutaj callback na koniec czasu
            }
            
        }
        
        private void Reset()
        {
            currentTime = timeInSeconds;
            tweenSequence?.Kill();
            animStarted = false;
        }

        private void DrawTimer(float timeValue)
        {
            int minutes = Mathf.FloorToInt(timeValue / 60F);
            int seconds = Mathf.FloorToInt(timeValue - minutes * 60);

            string niceTime = string.Format("{0:0}:{1:00}", minutes, seconds);

            timerText.text = niceTime;
        }

        private void PlayAnimation()
        {
            animStarted = true;
            Tween scaleTween = textrect.DOScale(new Vector3(1.1f, 1.1f, 1.1f), 0.5f);
            Tween colorTween = timerText.DOColor(Color.red, 0.5f);
            scaleTween.Play();
            scaleTween.SetLoops(-1, LoopType.Yoyo);
            colorTween.Play();
            colorTween.SetLoops(-1, LoopType.Yoyo);
        }
    }
}