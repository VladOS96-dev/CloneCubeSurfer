using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace UI
{
    public class WinScreen : BaseScreen
    {
    
        public void ContinueGame()
        {
            UiManager.Instance.SnowInGame();
            UiManager.Instance.levelManager.IncreaseLevel();
            UiManager.Instance.levelManager.NextLevel();
            UiManager.Instance.PlayerController.SetIsCanMove(true);
        }
    }
}
