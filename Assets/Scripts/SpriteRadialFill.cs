using System.Collections;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class SpriteRadialFill : MonoBehaviour
{
    private SpriteRenderer _renderer;

    private void Awake()
    {
        _renderer = GetComponent<SpriteRenderer>();
    }

    public void StartFill(float durationSeconds)
    {
        StartCoroutine(PerformFill(durationSeconds));
    }

    private IEnumerator PerformFill(float durationSeconds)
    {
        var startArc1 = 0f;
        var endArc1 = 360f;
        var currentArc1 = startArc1;
        var arcDeltaMillisecond = (endArc1 - startArc1) / durationSeconds;
        
        while (currentArc1 < endArc1)
        {
            currentArc1 += arcDeltaMillisecond * Time.deltaTime;
            _renderer.material.SetFloat("_Arc1", currentArc1);
            yield return null;
        }
    }
}
