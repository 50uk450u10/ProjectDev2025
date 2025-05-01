using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    //public float mouseSens = 50f;

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
}
