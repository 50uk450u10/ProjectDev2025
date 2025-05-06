using UnityEngine;

public class MouseMovement : MonoBehaviour
{
    [SerializeField] float sensitivity = 500f;
    private float rotX;
    private float rotY;

    [SerializeField] Transform lookPos;         //needed for smooth looking

    public float topClamp = -90f;
    public float bottomClamp = 90f;

    public bool unlocked = true;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;           //locks cursor
    }

    // Update is called once per frame
    void Update()
    {
        if (unlocked)
        {
            // rotation
            float mouseX = Input.GetAxis("Mouse X") * sensitivity * Time.deltaTime;
            float mouseY = Input.GetAxis("Mouse Y") * sensitivity * Time.deltaTime;

            rotY += mouseX;
            rotX -= mouseY;
            rotX = Mathf.Clamp(rotX, topClamp, bottomClamp);

            //transform.localRotation = Quaternion.Euler(rotX, rotY, 0);        //used to rotate entire parent, use if head looks up/down
            transform.eulerAngles = new Vector3(0, rotY, 0);
            lookPos.eulerAngles = new Vector3(rotX, rotY, 0);
        }
    }

    public void SetSensitivityFromSlider(float sliderValue)
    {
        sensitivity = sliderValue * 50f;
        Debug.Log("Slider value is: " + sliderValue.ToString());
    }

    public void ToggleLocked()
    {
        if (unlocked)
        {
            unlocked = false;
        }
        else
        {
            unlocked = true;
        }
    }
}
