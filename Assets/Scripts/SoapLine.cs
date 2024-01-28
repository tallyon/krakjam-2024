using UnityEngine;
using DG.Tweening;

public class SoapLine : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag(GameTags.PLAYER_1_TAG) || other.gameObject.CompareTag(GameTags.PLAYER_2_TAG))
        {
            other.GetComponent<PlayerCharacter>().ApplyStatus(PlayerCharacterStatus.Slipping, PlayerMovementController.STUNNING_SURFACE_STUN_DURATION_SECONDS);
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        const float duration = 1;
        const float length = 30;
        var seq = DOTween.Sequence();
        seq.Append(transform.DOScaleX(length, duration));
        seq.Join(transform.DOMoveX(transform.position.x + (length / 5), duration));
        seq.Play();
    }
}
