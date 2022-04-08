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
                other.tag = "Untagged";
                other.transform.parent = null;
                //other.gameObject.AddComponent<Rigidbody>();
                //Destroy(other.gameObject,2f);
            }
           
        }
  
    }
}
