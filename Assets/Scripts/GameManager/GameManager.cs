using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public float mouseSens;
    public int currentSceneIndex;
    public int sceneIndex;

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
}
