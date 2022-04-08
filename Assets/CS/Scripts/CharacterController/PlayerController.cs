using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathCreation;
using System;

namespace Gameplay
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private float moveSpeed;
        [SerializeField] private float border;
        [SerializeField] private float slideSpeed;
        private float localLengthOfCentre;
        [HideInInspector]
        public PathCreator pathCreator;
        public delegate void ResetData();
        public event ResetData OnReset;
        [SerializeField] private EndOfPathInstruction endOfPathInstruction;
        private float distanceTravelled;
        private Rigidbody rb; 
        private Vector3 CenterPos;
        private bool isCanMove;
        private void Awake()
        {
            rb = GetComponent<Rigidbody>();
        }
        void Start()
        {           
            DragInput.OnDragPointer += SideMove;
            CenterPos = transform.position;
          
        }
         public void ResetBeginPosition()
        {
          
            distanceTravelled = 0; 
            transform.position = pathCreator.path.GetPointAtDistance(distanceTravelled, endOfPathInstruction);
            transform.rotation = pathCreator.path.GetRotationAtDistance(distanceTravelled, endOfPathInstruction);
            localLengthOfCentre = 0;
            rb.isKinematic = false;
            OnReset?.Invoke();
        }
        void Update()
        {         
            Movement();
        }
        public void SetIsCanMove(bool status)
        {
            isCanMove = status;          
        }
        public void StopMove()
        {
            rb.isKinematic = true;
        }

        private void Movement()
        {

            if (!isCanMove)
                return;
            if (distanceTravelled >= pathCreator.path.length)
            {
                SetIsCanMove(false);
               
                UI.UiManager.Instance.ShowWin();
            }

            if (pathCreator != null)
            {
                distanceTravelled += moveSpeed * Time.deltaTime;
                Vector3 dir = pathCreator.path.GetPointAtDistance(distanceTravelled, endOfPathInstruction);
                CenterPos = new Vector3(dir.x,transform.position.y,dir.z );
                transform.rotation = Quaternion.Euler(0,
                pathCreator.path.GetRotationAtDistance(distanceTravelled, endOfPathInstruction).eulerAngles.y,0);
                transform.Translate(Vector3.right * localLengthOfCentre, Space.Self);
                Vector3 newPos = transform.position;
                Vector3 offset = newPos - CenterPos;
                transform.position = CenterPos + Vector3.ClampMagnitude(offset, border);
            }
        }
    
        private void SideMove(Vector2 delta, Vector2 pos)
        {
            if (!isCanMove)
            {
                return;
            }
            transform.Translate(Vector3.right *delta.x* slideSpeed * Time.deltaTime, Space.Self);
            localLengthOfCentre += slideSpeed * delta.x * Time.deltaTime;
            localLengthOfCentre = Mathf.Clamp(localLengthOfCentre,-border,border);
            Vector3 newPos = transform.position;
            Vector3 offset = newPos - CenterPos;
            transform.position = CenterPos + Vector3.ClampMagnitude(offset, border);
        }


    }
}