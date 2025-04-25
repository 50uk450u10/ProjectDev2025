using UnityEngine;
using UnityEngine.Events;

public class Flashlight : MonoBehaviour
{
    [SerializeField] Light flashLight;
    [SerializeField] Light areaLight;
    [SerializeField] AudioClip flashlightClip;
    AudioSource AS;
    public bool FlashlightOn = true;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        AS = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            ToggleFlashlight();
        }

    }


    public void ToggleFlashlight()
    {
        AS.PlayOneShot(flashlightClip); //Plays flashlight noise

        //Handles turning on and off the light whenever the function is called
        if (FlashlightOn == true)
        {
            areaLight.intensity = 0.0f;
            flashLight.intensity = 0.0f;
            FlashlightOn = false;
        }

        else
        {
            areaLight.intensity = 1.75f;
            flashLight.intensity = 1.0f;
            FlashlightOn = true;
        }

}
}
