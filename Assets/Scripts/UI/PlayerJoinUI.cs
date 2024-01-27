using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

namespace UI
{
    public class PlayerJoinUI : MonoBehaviour
    {
        [SerializeField] private GameObject joinTextParent;
        [SerializeField] private int expectedId;
        [SerializeField] private Image iconPad;
        [SerializeField] private Image iconKb;
        
        private void Start()
        {
            if (GameStateController.Instance.IsGameInitialized)
            {
                GameStateController.Instance.onPlayerJoined += OnJoin;
            }
            else
            {
                GameStateController.Instance.OnGameInit += () => GameStateController.Instance.onPlayerJoined += OnJoin;
            }
        }

        private void OnJoin(PlayerInput input, PlayerCharacter playerCharacter)
        {
            if (input.playerIndex == expectedId)
            {
                if (input.devices[0].GetType().Name == "FastKeyboard")
                {
                    joinTextParent.SetActive(false);
                    iconKb.gameObject.SetActive(true);
                    iconPad.gameObject.SetActive(false);
                }
                else
                {
                    joinTextParent.SetActive(false);
                    iconKb.gameObject.SetActive(false);
                    iconPad.gameObject.SetActive(true);
                }
            }
        }
    }
}