using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
namespace Gameplay
{
    public class DragInput : MonoBehaviour, IDragHandler
    {
        public static Action<Vector2, Vector2> OnDragPointer;

        public void OnDrag(PointerEventData eventData)
        {

            OnDragPointer?.Invoke(
                new Vector2(eventData.delta.x / Screen.width, eventData.delta.y / Screen.height),
                new Vector2(eventData.position.x / Screen.width, eventData.position.y / Screen.height)
            );
        }

        private void OnDisable()
        {
            OnDragPointer = null;

        }
    }
}