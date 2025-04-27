using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicDoorScript : MonoBehaviour
{
    public GameObject Notif;
    public Animator Door;

    void Update() 
    {
        if(Input.GetKeyDown(KeyCode.E) && Notif.activeInHierarchy == true)
        {
            Door.SetTrigger("Open");
        }
        
    }

    void OnTriggerEnter(Collider obj) 
    {
        if(obj.tag == "Door")
        {
            Notif.SetActive(true);
        }
    }

    void OnTriggerExit(Collider obj) 
    {
        if(obj.tag == "Door")
        {
            Notif.SetActive(false);
        }
    }
    
}