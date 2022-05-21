using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
namespace Gameplay
{
    public class DragInput : MonoBehaviour, IDragHandler
    {
        /// <summary>
        /// Событие отвечающие за отслеживания направления движения touch
        /// </summary>
        public static Action<Vector2> OnDragPointer;
     
        public void OnDrag(PointerEventData eventData)
        {
            OnDragPointer?.Invoke(
                new Vector2(eventData.delta.x / Screen.width, eventData.delta.y / Screen.height)    
            );
        }

        private void OnDisable()
        {
            OnDragPointer = null;

        }
    }
}