using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;


public class PlayerDeath : MonoBehaviour
{
    [SerializeField] AudioClip manDying;
    [SerializeField] AudioClip monsterAttack;
    [SerializeField] AudioSource playerSource;
    [SerializeField] AudioSource monsterSource;
    [SerializeField] Image bloodImage;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GameOver()
    {
        playerSource.PlayOneShot(manDying);
        monsterSource.PlayOneShot(monsterAttack);

        //This will load the main menu
        StartCoroutine(PauseForSound());
    }

    private IEnumerator PauseForSound()
    {
        yield return new WaitForSeconds(2.0f);
        SceneManager.LoadScene("GameOver"); //Load into the gameover screen
    }
}
