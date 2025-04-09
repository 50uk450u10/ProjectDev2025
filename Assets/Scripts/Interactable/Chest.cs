using UnityEngine;

public class Chest : MonoBehaviour
{
    [SerializeField] Item itemInside;

    private bool open = false;
    private Animator anim;

    public void OnInteract()
    {
        if (!open)
        {
            itemInside.obtainable = true;
            open = true;
            gameObject.GetComponent<MeshRenderer>().enabled = false;
        }
        else if (open)
        {
            //do nothing
            return;
        }
    }

}
