using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace UI
{
    public class BaseScreen : MonoBehaviour
    {   
        [HideInInspector]
        public CanvasGroup canvasGroup;
        private void Awake()
        {
            canvasGroup = GetComponent<CanvasGroup>();
        }
        /// <summary>
        /// ������� �������� ����
        /// </summary>
        public void ShowWindow()
        {
            canvasGroup.alpha = 1;
            canvasGroup.interactable = true;
            canvasGroup.blocksRaycasts = true;
        }
        /// <summary>
        /// ������� ������� ����
        /// </summary>
        public void HideWindow()
        {
            canvasGroup.alpha = 0;
            canvasGroup.interactable = false;
            canvasGroup.blocksRaycasts = false;
        }
    }
}