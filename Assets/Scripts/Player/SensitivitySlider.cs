using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using TMPro;
using UnityEngine.UI;
public class SensitivitySlider : MonoBehaviour
{

    public Slider sensSlider;
    public TextMeshProUGUI sensText;
    // Start is called before the first frame update
    void Start()
    {
        sensSlider.value = GameManager.instance.mouseSens;
        sensText.text = sensSlider.value.ToString("0.00");
    }

    // Update is called once per frame
    void Update()
    {
        sensText.text = sensSlider.value.ToString("0.00");
        GameManager.instance.mouseSens = sensSlider.value;
    }
}
