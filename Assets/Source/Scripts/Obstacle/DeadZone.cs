using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Gameplay
{
    public class DeadZone : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                other.GetComponentInParent<CollectionBlock>().DestroyLastBlock();
            }
           
        }
  
    }
}
