using UnityEngine;

public class KeySpawner : MonoBehaviour
{
    [SerializeField] Transform[] spawnLocations;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gameObject.transform.position = SetSpawnLocation().position;
    }


    private Transform SetSpawnLocation()
    {
        var random = new System.Random();
        var loc = random.Next(0, spawnLocations.Length);
        return spawnLocations[loc];
    }
}
