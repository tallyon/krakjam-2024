using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace UI
{
    public class SplitScreenUIManager : MonoBehaviour
    {
        [SerializeField] private List<PlayerActionsView> playerActions;

        //TODO: here subscribe to GameManager, and initialize and inform different playerAction set based on ID callback
        public void SetupSplitScreen(int id)
        {
            playerActions[id].Setup();
        }
    }
}