using UnityEngine;
using DG.Tweening;

public class TutorialUI : MonoBehaviour
{
    [SerializeField] private CanvasGroup[] sectionsToAppear;
    [SerializeField] private bool skipTutorial;

    private Sequence _seq;

    private void Awake()
    {
        // if (skipTutorial) gameObject.SetActive(false);
        // return;
        
        _seq = DOTween.Sequence();
        foreach (var section in sectionsToAppear)
        {
            section.alpha = 0;
        }

        var adoreHer = sectionsToAppear[0];
        var characters = sectionsToAppear[1];
        var textSection = sectionsToAppear[2];

        _seq.Append(adoreHer.DOFade(1, 2));
        _seq.AppendInterval(2);
        _seq.Append(characters.DOFade(1, 2));
        _seq.AppendInterval(3);
        _seq.Append(textSection.DOFade(1, 2));
        _seq.AppendInterval(2);
        _seq.Append(GetComponent<CanvasGroup>().DOFade(0, 1));
        _seq.onComplete += () => gameObject.SetActive(false);
        _seq.Pause();
    }

    public void StartFadeIn()
    {
        Debug.Log("START FADE IN");
        _seq.Play();
    }
}
