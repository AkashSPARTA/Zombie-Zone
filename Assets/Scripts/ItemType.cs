using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemType : MonoBehaviour
{
    public enum typeOfItem
    {
        flashlight,
        nightvision,
        lighter,
        rags,
        healthpack,
        pills,
        waterBottle,
        apple,
        flashlightBattery,
        nightvisionBattery,
        housekey,
        cabinkey,
        jerrycan
    }

    public typeOfItem chooseItem;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
