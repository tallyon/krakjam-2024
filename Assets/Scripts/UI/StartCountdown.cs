using System;
using DG.Tweening;
using TMPro;
using UnityEngine;

namespace UI
{
    public class StartCountdown : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI countDownText;
        [SerializeField] private RectTransform countdownrect;
        private Tween currentScaleTween;
        Sequence tweenSeq;

        private void Start()
        {
            if (GameStateController.Instance.IsGameInitialized)
            {
                GameStateController.Instance.StartTimer += TimerCallback;
            }
            else
            {
                GameStateController.Instance.OnGameInit +=
                    () => GameStateController.Instance.StartTimer += TimerCallback;
            }
        }

        private bool isFirstCall;
        private void TimerCallback(float timer)
        {
            if (timer == 0)
            {
                gameObject.SetActive(false);
            }
            countDownText.text = timer.ToString();
                //currentScaleTween?.Kill();
                //tweenSeq?.Kill();
                currentScaleTween = countdownrect.DOScale(new Vector3(1.2f, 1.2f, 1.2f), 0.5f);
                currentScaleTween.SetLoops(2, LoopType.Yoyo);
                    

            currentScaleTween.Play();
        }

        private void SetupAdditonalSequence()
        {
            //tweenSeq.Append(droprect.DOMoveY(droprect.position.y - 560f, 0.6f));
            //tweenSeq.Append(droptext.DOFade(0, 0.6f));
            //tweenSeq.SetDelay(0.3f);
        }
    }
}