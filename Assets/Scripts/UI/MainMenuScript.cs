using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuScript : MonoBehaviour
{
    [SerializeField] string sceneToLoad;        //attach scene's name to vairable and game should run said scene
    public GameObject settingsPanel;
    private bool settingsPanelOpen = false;

    public void StartGame()
    {
        SceneManager.LoadScene(sceneToLoad);
    }

    public void SettingsTab()
    {
        if(settingsPanelOpen)
        {
            settingsPanel.SetActive(false);
            settingsPanelOpen = false;
        }
        else
        {
            settingsPanel.SetActive(true);
            settingsPanelOpen = true;
        }
    }

    public void ExitGame()
    {
        Application.Quit();
        Debug.Log("Game quit");
    }
}
