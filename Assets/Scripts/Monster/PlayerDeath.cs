using UnityEngine;
using UnityEngine.SceneManagement;


public class PlayerDeath : MonoBehaviour
{
    int currentScene;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentScene = SceneManager.GetActiveScene().buildIndex; //Get the index of our current level and save it
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GameOver()
    {
        Debug.Log("You've been killed by The Monster!");

        SceneManager.LoadScene(currentScene); //Reload the current scene (from the beginning)
    }
}
