using UnityEngine;

public class Item : MonoBehaviour
{
    //An item (for our inventory) consists of a name, sprite and ability to pick it up
    [SerializeField] public string itemName;
    [SerializeField] public Sprite sprite;
    [SerializeField] public bool obtainable = false;
    
}
