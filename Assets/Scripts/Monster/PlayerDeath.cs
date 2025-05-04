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
    [SerializeField] Image flash;
    [SerializeField] Color red;
    [SerializeField] float fadeSpeed;
    bool gameOverTriggered;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        red = flash.color;
    }

    // Update is called once per frame
    void Update()
    {
        if (gameOverTriggered)
        {

            red.a += fadeSpeed * Time.deltaTime;
            flash.color = red;
        }
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
        //flash = Color.Lerp(flash, flash2, fadeSpeed * Time.deltaTime);
        //flash += fadeSpeed * Time.deltaTime;
        gameOverTriggered = true;
        yield return new WaitForSeconds(2.0f);
        SceneManager.LoadScene("GameOver"); //Load into the gameover screen
    }
}
