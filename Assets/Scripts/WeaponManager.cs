using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class WeaponManager : MonoBehaviour
{
    public enum weaponSelect
    {
        Knife,
        cleaver,
        bat,
        axe,
        pistol,
        shotgun,
        sprayCan,
        Bottle,
        BottleWithCloth
    }

    public weaponSelect chosenWeapon;
    public GameObject[] Weapons;
    //private int weaponID = 0;
    public Animator anim;
    private AudioSource audioPlayer;
    public AudioClip[] weaponSounds;
    private int currentWeaponID;
    private bool spraySoundOn = false;
    public GameObject sprayPanel;

    // Start is called before the first frame update
    void Start()
    {
        SaveScript.weaponID = (int)chosenWeapon;
        audioPlayer = GetComponent<AudioSource>();
        ChangeWeapons();
    }

    // Update is called once per frame
    void Update()
    {
        if (SaveScript.weaponID != currentWeaponID)
        {
            ChangeWeapons();
        }


        if (Input.GetMouseButtonDown(0))
        {
            if (SaveScript.inventoryOpen == true)
            {
                if (SaveScript.currentAmmo[SaveScript.weaponID] > 0)
                {
                    SaveScript.inventoryOpen = true;
                    anim.SetTrigger("Attack");
                    audioPlayer.clip = weaponSounds[SaveScript.weaponID];
                    audioPlayer.Play();

                    if (SaveScript.weaponID == 4 || SaveScript.weaponID == 5)
                    {
                        SaveScript.currentAmmo[SaveScript.weaponID]--;
                    }
                }
                else
                {
                    if (SaveScript.weaponID == 4 || SaveScript.weaponID == 5)
                    {
                        audioPlayer.clip = weaponSounds[9];
                        audioPlayer.Play();
                    }
                }
            }
            else
            {
                SaveScript.inventoryOpen = false;
            }
        }
        if(Input.GetMouseButton(0) && sprayPanel.GetComponent<SprayScript>().sprayAmount > 0.0f)
        {
            if (SaveScript.weaponID == 6 && SaveScript.inventoryOpen == true)
            {
                if (spraySoundOn == false)
                {
                    spraySoundOn = true;
                    anim.SetTrigger("Attack");
                    StartCoroutine(StartSpraySound());
                }
            }
        }
        if (Input.GetMouseButtonUp(0) || sprayPanel.GetComponent<SprayScript>().sprayAmount <= 0.0f)
        {
            if (SaveScript.weaponID == 6 && SaveScript.inventoryOpen == true)
            {
                anim.SetTrigger("Release");
                spraySoundOn = false;
                audioPlayer.Stop();
                audioPlayer.loop = false;
            }
        }
    }

    private void ChangeWeapons()
    {
        foreach (GameObject weapon in Weapons)
        {
            weapon.SetActive(false);
        }
        Weapons[SaveScript.weaponID].SetActive(true);
        chosenWeapon = (weaponSelect)SaveScript.weaponID;
        anim.SetInteger("WeaponID", SaveScript.weaponID);
        anim.SetBool("weaponChanged", true);
        currentWeaponID = SaveScript.weaponID;

        Move();
        StartCoroutine(WeaponReset());
    }

    private void Move()
    {
        switch (chosenWeapon)
        {
            case weaponSelect.Knife:
                transform.localPosition = new Vector3(0.02f, -0.193f, 0.66f);
                break;
            case weaponSelect.cleaver:
                transform.localPosition = new Vector3(0.02f, -0.193f, 0.66f);
                break;
            case weaponSelect.bat:
                transform.localPosition = new Vector3(0.02f, -0.193f, 0.66f);
                break;
            case weaponSelect.axe:
                transform.localPosition = new Vector3(0.02f, -0.193f, 0.66f);
                break;
            case weaponSelect.pistol:
                transform.localPosition = new Vector3(0.02f, -0.193f, 0.66f);
                break;
            case weaponSelect.shotgun:
                transform.localPosition = new Vector3(0.02f, -0.193f, 0.40f);
                break;
            case weaponSelect.sprayCan:
                transform.localPosition = new Vector3(0.02f, -0.193f, 0.66f);
                break;
            case weaponSelect.Bottle:
                transform.localPosition = new Vector3(0.02f, -0.193f, 0.66f);
                break;
        }
    }
    IEnumerator WeaponReset()
    {
        yield return new WaitForSeconds(0.5f);
        anim.SetBool("weaponChanged", false);
    }

    IEnumerator StartSpraySound()
    {
        yield return new WaitForSeconds(0.3f);
        audioPlayer.clip = weaponSounds[SaveScript.weaponID];
        audioPlayer.Play();
        audioPlayer.loop = true;
    }
}
