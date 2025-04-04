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
    public static bool[] weaponsPickedUp = new bool [9];
    public static bool[] itemPickedUp = new bool [13];
    public static int[] weaponAmts = new int [9];
    public static int[] itemAmts = new int [13];
    public static int[] ammoAmts = new int [2];
    public static int[] currentAmmo = new int [9];
    public static bool changeWeapon = false;
    public static bool changeItems = false;
    public static float stamina;
    public static float infection;
    public static int health;
    public static GameObject doorObject;
    public static bool gunUsed = false;
    public static Vector3 bottlePos = new Vector3(0, 0, 0);
    private bool hasSmashed = false;
    public static bool isHidden = false;
    public static int zombieInGameAmt = 0;
    public static bool generatorOn = false;
    public static GameObject generator;

    public static List<GameObject> zombiesChasing = new List<GameObject>();


    // Start is called before the first frame update
    void Start()
    {
        stamina = FirstPersonController.FPSstamina;
        health = 100;
        weaponsPickedUp[0] = true;
        weaponAmts[0] = 1;
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
        if(zombieInGameAmt < 0)
        {
            zombieInGameAmt = 0;
        }
        if (FirstPersonController.inventorySwitchOn == true)
        {
            inventoryOpen = false;
        }
        if (FirstPersonController.inventorySwitchOn == false)
        {
            inventoryOpen = true;
        }

        if(Input.GetAxis("Vertical") != 0 && Input.GetKey(KeyCode.LeftShift) && FirstPersonController.FPSstamina > 0.0f)
        {
            FirstPersonController.FPSstamina -= 10 * Time.deltaTime;
            stamina = FirstPersonController.FPSstamina;
        }
        if(stamina < 100)
        {
            FirstPersonController.FPSstamina += 3.35f * Time.deltaTime;
            stamina = FirstPersonController.FPSstamina;
        }
        if(stamina >= 100)
        {
            FirstPersonController.FPSstamina = stamina;
        }
        if(Input.GetMouseButtonDown(0) && stamina > 10 & weaponID < 4 && inventoryOpen == true)
        {
            FirstPersonController.FPSstamina -= 10;
            stamina = FirstPersonController.FPSstamina;
        }

        if (Input.GetKey(KeyCode.C))
        {
            FirstPersonController.FPSstamina -= 10f * Time.deltaTime;
            stamina = FirstPersonController.FPSstamina;
        }

        if(infection < 50)
        {
            infection += 0.1f * Time.deltaTime;
        }
        if (infection > 49 && infection < 100)
        {
            infection += 0.4f * Time.deltaTime;
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

        if (bottlePos != Vector3.zero )
        {
            if(hasSmashed == false)
            {
                StartCoroutine(ResetBottlePos());
                hasSmashed = true;
            }
        }

    }

    IEnumerator ResetBottlePos()
    {
        yield return new WaitForSeconds(30);
        bottlePos = Vector3.zero;
        hasSmashed = false;
    }
}
