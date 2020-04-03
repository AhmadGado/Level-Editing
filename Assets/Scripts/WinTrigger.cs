using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinTrigger : MonoBehaviour
{
     
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (other.gameObject.GetComponent<Inventory>().HasKey)
            {
                UIManager.Instance.ActivateWinPanel();
            }
            else
            {
                Debug.Log("ll");
                UIManager.Instance.MessagePanel.SetActive(true);
                UIManager.Instance.SetMessage("Find the key to open the door");
            }
        }    
    }

}
