using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicDoorScript : MonoBehaviour

// Main door script that makes the door open when pressing E
{
    public GameObject Notif;
    public LayerMask mask;

    void Update() 
    {
        if(Input.GetKeyDown(KeyCode.E) && Notif.activeInHierarchy == true)
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position, transform.forward, out hit, Mathf.Infinity, mask))
            {
                hit.collider.gameObject.transform.parent.gameObject.GetComponent<Animator>().SetTrigger("Open");
            }
        }
    }

// Turns on the UI that tells the player to Press E to open door
    void OnTriggerEnter(Collider obj) 
    {
        if(obj.tag == "Door")
        {
            Notif.SetActive(true);
        }
    }

// Turns off the UI that tells player to Press E to open door 
    void OnTriggerExit(Collider obj) 
    {
        if(obj.tag == "Door")
        {
            Notif.SetActive(false);
        }
    }
    
}