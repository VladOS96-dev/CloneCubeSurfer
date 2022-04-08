using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace UI
{
    public class MenuScreen : BaseScreen
    {

        public void StarGame()
        {
            UiManager.Instance.PlayerController.SetIsCanMove(true);
            UiManager.Instance.SnowInGame();
        }
    }
}
