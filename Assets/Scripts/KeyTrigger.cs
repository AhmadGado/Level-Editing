using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyTrigger : MonoBehaviour
{ 

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.GetComponent<Inventory>().HasKey = true;
            UIManager.Instance.MessagePanel.SetActive(true);
            UIManager.Instance.SetMessage("Now find the door.");

            Destroy(this.gameObject);
        }
    }

}
