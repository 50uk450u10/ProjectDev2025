using UnityEngine;

[CreateAssetMenu(fileName = "GameManagement", menuName = "Scriptable Objects/GameManagement")]
public class GameManagement : ScriptableObject
{
    public int currentScene;
    public float mouseSens;
    public float contrast;
    public float volume;
}
