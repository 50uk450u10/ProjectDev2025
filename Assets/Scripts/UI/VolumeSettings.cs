using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class VolumeSettings : MonoBehaviour
{
    [SerializeField] AudioMixer mixer;
    [SerializeField] Slider volumeSlider;

    public const string MIXER_VOLUME = "MasterVolume";

    void Awake()
    {
        volumeSlider.onValueChanged.AddListener(SetVolume);
    }

    void Start()
    {
        volumeSlider.value = PlayerPrefs.GetFloat(GameManager.VOLUME_KEY, .5f);
    }

    void OnDisable()
    {
        PlayerPrefs.SetFloat(GameManager.VOLUME_KEY, volumeSlider.value);
    }

    void SetVolume(float value)
    {
        mixer.SetFloat(MIXER_VOLUME, Mathf.Log10(value) * 20);
    }
}
