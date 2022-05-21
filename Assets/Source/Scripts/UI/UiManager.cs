using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Level;
namespace UI
{
    public class UiManager : MonoBehaviour
    {
        [SerializeField] private BaseScreen menuScreen;
        [SerializeField] private BaseScreen inGameScreen;
        [SerializeField] private BaseScreen winScreen;
        [SerializeField] private BaseScreen loseScreen;
        public Gameplay.PlayerController PlayerController;
        public static UiManager Instance;
        public LevelManager levelManager;
        
        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }

        }
        void Start()
        {
            ShowMenu();
            levelManager.NextLevel();
        }
        /// <summary>
        /// ������� ��������� ���� ����
        /// </summary>
        public void ShowMenu()
        {
            winScreen.HideWindow();
            loseScreen.HideWindow();
            inGameScreen.HideWindow();
            menuScreen.ShowWindow();
        }
        /// <summary>
        /// ������� ��������� ���� ����
        /// </summary>
        public void ShowLose()
        {
            winScreen.HideWindow();
            loseScreen.ShowWindow();
            inGameScreen.HideWindow();
            menuScreen.HideWindow();
        }
        /// <summary>
        /// ������� ��������� ���� ����
        /// </summary>
        public void ShowWin()
        {
            winScreen.ShowWindow();
            loseScreen.HideWindow();
            inGameScreen.HideWindow();
            menuScreen.HideWindow();
        }
        /// <summary>
        /// ������� ��������� ���� 
        /// </summary>
        public void SnowInGame()
        {
            winScreen.HideWindow();
            loseScreen.HideWindow();
            inGameScreen.ShowWindow();
            menuScreen.HideWindow();
        }
    }
}
