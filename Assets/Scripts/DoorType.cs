using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorType : MonoBehaviour
{
    public enum typeofDoors
    {
        cabinet,
        house,
        cabin
    }

    public typeofDoors ChooseDoor;
    public bool opened = false;
    public bool locked = false;
    [HideInInspector]
    public string message = "Press E to open the door ";
    private Animator anim;
    public bool electricDoor = false;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        if (opened)
        {
            anim.SetTrigger("Open");
            message = "Press E to close the door";
        }
/*        else if (opened == true)
        {
            anim.SetTrigger("Close");
            message = "Press E to open the door";
        }*/
    }

}
