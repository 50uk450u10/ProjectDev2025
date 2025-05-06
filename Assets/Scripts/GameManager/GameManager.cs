using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class GameManager : MonoBehaviour
{
    [SerializeField] AudioMixer mixer;
    public static GameManager instance;
    public float mouseSens;
    public int currentSceneIndex;
    public int sceneIndex;

    public const string VOLUME_KEY = "masterVolume";

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);

        LoadVolume();
    }

    void Start()
    {
        mouseSens = 10f;
    }

    private void Update()
    {
        currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        switch (currentSceneIndex)
        {
            case 0:
                sceneIndex = currentSceneIndex;
                break;
            case 1:
                sceneIndex = currentSceneIndex;
                break;
            case 2:
                sceneIndex = currentSceneIndex;
                break;
        }
    }

    void LoadVolume()
    {
        float masterVolume = PlayerPrefs.GetFloat(VOLUME_KEY, .5f);
        mixer.SetFloat(VolumeSettings.MIXER_VOLUME, Mathf.Log10(masterVolume) * 20);
    }
}
