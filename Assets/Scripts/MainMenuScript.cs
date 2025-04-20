using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuScript : MonoBehaviour
{
    [SerializeField] string sceneToLoad;        //attach scene's name to vairable and game should run said scene

    public void StartGame()
    {
        SceneManager.LoadScene(sceneToLoad);
    }

    public void ExitGame()
    {
        Application.Quit();
        Debug.Log("Game quit");
    }
}
