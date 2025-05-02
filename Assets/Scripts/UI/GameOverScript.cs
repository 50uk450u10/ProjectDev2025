using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverScript : MonoBehaviour
{
    public int sceneToLoad;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Cursor.lockState = CursorLockMode.Confined;
        sceneToLoad = GameManager.instance.sceneIndex;
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(sceneToLoad);
    }

    public void ExitToMainMenu()
    {
        SceneManager.LoadScene(0);
    }
}
