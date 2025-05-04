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
        //flash = Color.Lerp(flash, flash2, fadeSpeed * Time.deltaTime);
        //flash += fadeSpeed * Time.deltaTime;
        red = flash.color;
        red.a += fadeSpeed * Time.deltaTime;
        flash.color = red;
        yield return new WaitForSeconds(2.0f);
        SceneManager.LoadScene("GameOver"); //Load into the gameover screen
    }
}
