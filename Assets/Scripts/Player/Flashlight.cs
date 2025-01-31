using UnityEngine;
using UnityEngine.Events;

public class Flashlight : MonoBehaviour
{
    [SerializeField] Light flashLight;
    bool FlashlightOn = true;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F) && FlashlightOn == true)
        {
            Debug.Log("F key was pressed.");
            flashLight.intensity = 0.0f;
            FlashlightOn = false;
        }

        else if (Input.GetKeyDown(KeyCode.F) && FlashlightOn == false)
        {

            Debug.Log("F key was pressed.");
            flashLight.intensity = 1.0f;
            FlashlightOn = true;
        }

    }

}
