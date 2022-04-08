using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace UI
{
    public class LoseScreen : BaseScreen
    {
 
        public void RestartGame()
        {
            UiManager.Instance.SnowInGame();
            UiManager.Instance.levelManager.RestartLevel();
        }
    }
}
