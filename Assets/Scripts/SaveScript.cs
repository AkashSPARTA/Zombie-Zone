using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.Characters.FirstPerson;

public class SaveScript : MonoBehaviour
{
    public static bool inventoryOpen = false;
    public static int weaponID = 0;
    public static int itemID = 0;
    public static bool[] weaponsPickedUp = new bool [8];
    public static bool[] itemPickedUp = new bool [13];
    public static int[] weaponAmts = new int [8];
    public static int[] itemAmts = new int [13];
    public static int[] ammoAmts = new int [2];
    public static int[] currentAmmo = new int [9];
    public static bool changeWeapon = false;
    public static bool changeItems = false;


    // Start is called before the first frame update
    void Start()
    {
        weaponsPickedUp[0] = true;

        itemPickedUp[0] = true;
        itemPickedUp[1] = true;
        itemAmts[0] = 1;
        itemAmts[1] = 1;
        ammoAmts[0] = 12;
        ammoAmts[1] = 2;

        for (int i = 0; i < currentAmmo.Length; i++)
        {
            currentAmmo[i] = 2;
        }
        currentAmmo[4] = 12;
        currentAmmo[6] = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (FirstPersonController.inventorySwitchOn == true)
        {
            inventoryOpen = false;
        }
        if (FirstPersonController.inventorySwitchOn == false)
        {
            inventoryOpen = true;
        }

        if (changeWeapon == true)
        {
            changeWeapon = false;
            for(int i = 1; i < weaponAmts.Length; i++)
            {
                if (weaponAmts[i] > 0)
                {
                    weaponsPickedUp[i] = true;
                }
                else if (weaponAmts[i] == 0)
                {
                    weaponsPickedUp[i] = false;
                }
            }

        }

        if (changeItems == true)
        {
            changeItems = false;
            for (int i = 2; i < itemAmts.Length; i++)
            {
                if (itemAmts[i] > 0)
                {
                    itemPickedUp[i] = true;
                }
                else if (itemAmts[i] == 0)
                {
                    itemPickedUp[i] = false;
                }
            }

        }

    }
}
