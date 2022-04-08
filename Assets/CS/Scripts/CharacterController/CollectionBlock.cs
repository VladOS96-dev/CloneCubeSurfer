using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay
{
    public class CollectionBlock : MonoBehaviour
    {
        public List<Transform> ListCollectBlocks;
        public Vector3 BeginPosition { get; set; }
        [SerializeField] private Transform mainBlock;
        [SerializeField] private float sizeBlock;
        private PlayerController playerController;
        void Start()
        {
            sizeBlock = mainBlock.GetComponent<MeshFilter>().mesh.bounds.size.y;
            playerController = GetComponent<PlayerController>();
            playerController.OnReset += ResetPosition;
        }
        private void OnDisable()
        {
            playerController.OnReset -= ResetPosition;
        }
        public void ResetPosition()
        {
            BeginPosition = mainBlock.localPosition;

        }
        public void ClearBlocks()
        {
            if (ListCollectBlocks.Count > 0)
            {
                for (int i = 1; i < ListCollectBlocks.Count; i++)
                {
                    Destroy(ListCollectBlocks[i].gameObject);
                }
            }
            mainBlock.parent = transform;
            mainBlock.localPosition = Vector3.zero;
            ListCollectBlocks.Clear();
            ListCollectBlocks.Add(mainBlock);
        }
        public void DestroyLastBlock()
        {
   
            if (ListCollectBlocks.Count > 0)
            {   
                ListCollectBlocks.RemoveAt(ListCollectBlocks.Count - 1);
                CheckLose();
            }

        }
       
        private void OnCollisionEnter(Collision collision)
        {
            if (collision.collider.CompareTag("Block"))
            {

                ListCollectBlocks.Add(collision.transform);
                collision.transform.position = transform.position;
                collision.transform.parent = transform;
                collision.collider.tag = "Player";
                for(int i = 0; i< ListCollectBlocks.Count; i++) 
                {
                    ListCollectBlocks[i].localPosition = new Vector3(ListCollectBlocks[i].localPosition.x,
                        BeginPosition.y +(ListCollectBlocks.Count-1-i) * sizeBlock, ListCollectBlocks[i].localPosition.z);
                }
            }
            if (collision.collider.CompareTag("Obstacle"))
            {
              
                ListCollectBlocks.Remove(collision.contacts[0].thisCollider.transform);

               
                if(!CheckLose())
                {
                    collision.contacts[0].thisCollider.transform.parent = null;

                }
            }
        }
        private bool CheckLose()
        {
            if (ListCollectBlocks.Count == 0)
            {
                playerController.SetIsCanMove(false);
                UI.UiManager.Instance.ShowLose();
                playerController.StopMove();
                return true;
            }
            return false;
        }

    
    }
}