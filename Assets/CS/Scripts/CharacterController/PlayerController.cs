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
        [SerializeField] private float sensivity;
        [SerializeField] private float border;
        [SerializeField] private float slideSpeed;
        [SerializeField] private float localLengthOfCentre;
        [HideInInspector]
        public Transform FinalPoint;
        [HideInInspector]
        public PathCreator pathCreator;
        public delegate void ResetData();
        public event ResetData OnReset;
        [SerializeField] private EndOfPathInstruction endOfPathInstruction;
        [SerializeField]private float distanceTravelled;
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
            localLengthOfCentre = 0;
            CenterPos = transform.position;
          
        }
         public void ResetBeginPosition()
        {
            OnReset?.Invoke();
            distanceTravelled = 0; 
            transform.position = pathCreator.path.GetPointAtDistance(distanceTravelled, endOfPathInstruction);
            transform.rotation = pathCreator.path.GetRotationAtDistance(distanceTravelled, endOfPathInstruction);
            rb.isKinematic = false;
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
            //Vector3 movement = new Vector3(Input.GetAxis("Horizontal"), 0, 0);

            if (!isCanMove)
                return;
            if (distanceTravelled >= pathCreator.path.length)
            {
                SetIsCanMove(false);
               
                UI.UiManager.Instance.ShowWin();
            }
            //Vector3 newPos = transform.position;// +movement;
            //newPos.x -= moveSpeed * Time.deltaTime;
            //transform.position = newPos;
            if (pathCreator != null)
            {
                distanceTravelled += moveSpeed * Time.deltaTime;
                CenterPos = pathCreator.path.GetPointAtDistance(distanceTravelled, endOfPathInstruction);

                transform.rotation = Quaternion.Euler(0,
                    pathCreator.path.GetRotationAtDistance(distanceTravelled, endOfPathInstruction).eulerAngles.y,
                    0); 
                //if (Input.GetAxis("Horizontal") > 0)
                //{
                //    transform.Translate(Vector3.right * slideSpeed * Time.deltaTime, Space.Self);
                //}
                //else
                //{
                //    transform.Translate(Vector3.left * slideSpeed * Time.deltaTime, Space.Self);
                //}
                //transform.Translate(Vector3.left * localLengthOfCentre * Time.deltaTime, Space.Self);
                Vector3 newPos = transform.position;
                Vector3 offset = newPos - CenterPos;

                transform.position = CenterPos + Vector3.ClampMagnitude(offset, border);
            }
        }
    
        private void SideMove(Vector2 delta, Vector2 pos)
        {
            //if (!isCanMove || IsFinish || IsFight)
            //{
            //    return;
            //}
            bool IsRight = false;
            if (delta.x > 0)
            {
                transform.Translate(Vector3.right * slideSpeed * Time.deltaTime, Space.Self);
                IsRight = true;

                localLengthOfCentre += slideSpeed * Time.deltaTime;
            }
            else
            {
                IsRight = false;
                transform.Translate(Vector3.left * slideSpeed * Time.deltaTime, Space.Self);
                localLengthOfCentre -= slideSpeed * Time.deltaTime;
            }
            Vector3 newPos = transform.position; /*+movement;*/
            Vector3 offset = newPos - CenterPos;
            transform.position = CenterPos + Vector3.ClampMagnitude(offset, border);


        }


    }
}