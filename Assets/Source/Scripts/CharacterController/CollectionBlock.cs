using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay
{
    public class CollectionBlock : MonoBehaviour
    {
        public List<Transform> CollectBlocks;
         private List<Transform> disativeBlocks;
        public Vector3 BeginPosition;
        [SerializeField] private Transform firstBlock;
        [SerializeField] private float sizeBlock;
        [SerializeField] private LayerMask mask;
        private PlayerController playerController;
        private void Awake()
        {
              CollectBlocks = new List<Transform>();
            disativeBlocks = new List<Transform>();
        }
        void Start()
        {
          
            sizeBlock = firstBlock.GetComponent<MeshFilter>().mesh.bounds.size.y;
            playerController = GetComponent<PlayerController>();
            playerController.OnReset += ResetPosition;
        }
        private void OnDisable()
        {
            playerController.OnReset -= ResetPosition;
        }
        /// <summary>
        ///Функция для установки первого блока в начальную позицию 
        /// </summary>
        public void ResetPosition()
        {
            transform.position = firstBlock.position;
            BeginPosition = firstBlock.localPosition;

        }
        /// <summary>
        /// Функция удаления всех собранных блоков
        /// </summary>
        public void ClearBlocks()
        {
            if (CollectBlocks.Count > 0)
            {
                for (int i = 1; i < CollectBlocks.Count; i++)
                {
                    Destroy(CollectBlocks[i].gameObject);
                }
            }
            foreach (var item in disativeBlocks)
            {
                Destroy(item.gameObject);
            }
            disativeBlocks.Clear();
            firstBlock.parent = transform;
            firstBlock.localPosition = Vector3.zero;
          
            CollectBlocks.Clear();
            CollectBlocks.Add(firstBlock);
        }
        //Функция удаления последнего блока
        public void DestroyLastBlock()
        {
   
            if (CollectBlocks.Count > 0)
            {
                if (CollectBlocks.Count != 1)
                {
                    CollectBlocks[CollectBlocks.Count - 1].tag = "Untagged";
                    CollectBlocks[CollectBlocks.Count - 1].parent = null;
                    CollectBlocks[CollectBlocks.Count - 1].gameObject.AddComponent<Rigidbody>();
                    Destroy(CollectBlocks[CollectBlocks.Count - 1].gameObject, 2f);
                }
                CollectBlocks.RemoveAt(CollectBlocks.Count - 1);
                CheckLose();
            }

        }
       
        private void OnCollisionEnter(Collision collision)
        {
            if (collision.collider.CompareTag("Block"))
            {  
                if (transform.position != CollectBlocks[CollectBlocks.Count - 1].position)
                {
                    transform.position = CollectBlocks[CollectBlocks.Count - 1].position;
                    for (int i = 0; i < CollectBlocks.Count; i++)
                    {
                        CollectBlocks[i].localPosition = new Vector3(CollectBlocks[i].localPosition.x,
                            BeginPosition.y + (CollectBlocks.Count - 1 - i) * sizeBlock, CollectBlocks[i].localPosition.z);
                    }
                }
                CollectBlocks.Add(collision.transform);
                collision.transform.position = transform.position;
                collision.transform.parent = transform;
               
                collision.collider.tag = "Player";
             
                for(int i = 0; i< CollectBlocks.Count; i++) 
                {
                    CollectBlocks[i].localPosition = new Vector3(CollectBlocks[i].localPosition.x,
                        BeginPosition.y +(CollectBlocks.Count-1-i) * sizeBlock, CollectBlocks[i].localPosition.z);
                    CollectBlocks[i].localRotation = Quaternion.identity;

                }
            }
            if (collision.collider.CompareTag("Obstacle"))
            {

                Collider[] collidersCenter = Physics.OverlapBox(collision.contacts[0].thisCollider.transform.position,
                   new  Vector3(0.2f,0.2f,1.5f),transform.rotation,mask);
                if (collidersCenter.Length > 0)
                {
                    CollectBlocks.Remove(collision.contacts[0].thisCollider.transform);

                    if (!CheckLose())
                    {
                        collision.contacts[0].thisCollider.transform.parent = null;
                        disativeBlocks.Add(collision.contacts[0].thisCollider.transform);
                    }
                }

            }
        }
     /// <summary>
     /// функция проверки поражения
     /// </summary>
     /// <returns>Возращает true если все блоки были уничтожены</returns>
        private bool CheckLose()
        {
            if (CollectBlocks.Count == 0)
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