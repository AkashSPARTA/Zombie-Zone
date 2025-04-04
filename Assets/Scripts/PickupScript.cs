using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PickupScript : MonoBehaviour
{
    private RaycastHit hit;
    public LayerMask excludeLayer;
    public GameObject PickupPanel;
    public float pickupDisplayDistance = 3;

    public Image mainImage;
    public Sprite[] weaponIcons;
    public Sprite[] itemsIcons;
    public Sprite[] AmmoIcons;
    public Text mainTitle;
    public string[] weaponTitles;
    public string[] itemsTitles;
    public string[] AmmoTitles;


    private int objID = 0;
    private AudioSource audioPlayer;
    public GameObject doorMessageObj;
    public GameObject generatorMessageObj;
    public Text doorMessage;
    public AudioClip[] pickupSounds;

    private RaycastHit gunHit;
    private RaycastHit[] shotGunHit;

    // Start is called before the first frame update
    void Start()
    {
        PickupPanel.SetActive(false);
        audioPlayer = GetComponent<AudioSource>();
        doorMessageObj.SetActive(false);
        generatorMessageObj.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Physics.SphereCast(transform.position, 0.3f, transform.forward, out hit, 30, ~excludeLayer))
        {
            if (Vector3.Distance(transform.position, hit.transform.position) < pickupDisplayDistance)
            {
                if (hit.transform.gameObject.CompareTag("weapon"))
                {
                    PickupPanel.SetActive(true);
                    int chooseWeapon1 = (int)hit.transform.gameObject.GetComponent<WeaponType>().chooseWeapon;
                    objID = chooseWeapon1;
                    mainImage.sprite = weaponIcons[objID];
                    mainTitle.text = weaponTitles[objID];

                    if (Input.GetKeyDown(KeyCode.E))
                    {
                        SaveScript.weaponAmts[objID]++;
                        audioPlayer.clip = pickupSounds[3];
                        audioPlayer.Play();
                        SaveScript.changeWeapon = true;
                        Destroy(hit.transform.gameObject, 0.2f);
                    }
                }
                else if (hit.transform.gameObject.CompareTag("item"))
                {
                    PickupPanel.SetActive(true);
                    objID = (int)hit.transform.gameObject.GetComponent<ItemType>().chooseItem;
                    mainImage.sprite = itemsIcons[objID];
                    mainTitle.text = itemsTitles[objID];

                    if (Input.GetKeyDown(KeyCode.E))
                    {
                        SaveScript.itemAmts[objID]++;
                        audioPlayer.clip = pickupSounds[3];
                        audioPlayer.Play();
                        SaveScript.changeItems = true;
                        Destroy(hit.transform.gameObject, 0.2f);
                    }
                }
                else if (hit.transform.gameObject.CompareTag("ammo"))
                {
                    PickupPanel.SetActive(true);
                    objID = (int)hit.transform.gameObject.GetComponent<AmmoType>().chooseAmmo;
                    mainImage.sprite = AmmoIcons[objID];
                    mainTitle.text = AmmoTitles[objID];

                    if (Input.GetKeyDown(KeyCode.E))
                    {
                        if (objID == 0)
                        {
                            SaveScript.ammoAmts[0] += 12;
                        }
                        if (objID == 1)
                        {
                            SaveScript.ammoAmts[1] += 8;
                        }
                        audioPlayer.clip = pickupSounds[3];
                        audioPlayer.Play();
                        SaveScript.changeItems = true;
                        Destroy(hit.transform.gameObject, 0.2f);
                    }
                }
                else if (hit.transform.gameObject.CompareTag("door"))
                {
                    SaveScript.doorObject = hit.transform.gameObject;
                    objID = (int)hit.transform.gameObject.GetComponent<DoorType>().ChooseDoor;
                    if (hit.transform.gameObject.GetComponent<DoorType>().locked == true)
                    {
                        if (hit.transform.gameObject.GetComponent<DoorType>().electricDoor == false)
                        {
                            hit.transform.gameObject.GetComponent<DoorType>().message = "Locked. You need to use the " + hit.transform.gameObject.GetComponent<DoorType>().ChooseDoor + " key";
                        }
                        if (hit.transform.gameObject.GetComponent<DoorType>().electricDoor == true && SaveScript.generatorOn == false)
                        {
                            hit.transform.gameObject.GetComponent<DoorType>().message = "This Door Need Power Supply to Open " + hit.transform.gameObject.GetComponent<DoorType>().ChooseDoor + " key";
                        }
                    }
                    if (hit.transform.gameObject.GetComponent<DoorType>().electricDoor == true && SaveScript.generatorOn == true)
                    {
                        if (hit.transform.gameObject.GetComponent<DoorType>().opened == false)
                        {
                            hit.transform.gameObject.GetComponent<DoorType>().message = "Press E to open the door";
                        }
                    }
                    doorMessageObj.SetActive(true);
                    doorMessage.text = hit.transform.gameObject.GetComponent<DoorType>().message;
                    if (Input.GetKeyDown(KeyCode.E) && hit.transform.gameObject.GetComponent<DoorType>().locked == false)
                    {
                        audioPlayer.clip = pickupSounds[objID];
                        audioPlayer.Play();
                        if (hit.transform.gameObject.GetComponent<DoorType>().opened == false)
                        {
                            hit.transform.gameObject.GetComponent<DoorType>().message = "Press E to close the door";
                            hit.transform.gameObject.GetComponent<DoorType>().opened = true;
                            hit.transform.gameObject.GetComponent<Animator>().SetTrigger("Open");
                        }
                        else if (hit.transform.gameObject.GetComponent<DoorType>().opened == true)
                        {
                            hit.transform.gameObject.GetComponent<DoorType>().message = "Press E to open the door";
                            hit.transform.gameObject.GetComponent<DoorType>().opened = false;
                            hit.transform.gameObject.GetComponent<Animator>().SetTrigger("Close");
                        }
                        
                    }
                }
                else if (hit.transform.gameObject.CompareTag("Generator"))
                {
                    SaveScript.generator = hit.transform.gameObject;

                    if(SaveScript.generatorOn == false)
                    {
                        generatorMessageObj.SetActive(true);
                    }
                    if (SaveScript.generatorOn == true)
                    {
                        generatorMessageObj.SetActive(false);
                    }
                }

            }
            else
            {
                PickupPanel.SetActive(false);
                doorMessageObj.SetActive(false);
                SaveScript.doorObject = null;
                generatorMessageObj.SetActive(false);
                SaveScript.generator = null;
            }
        }

        if (Physics.SphereCast(transform.position, 0.01f, transform.forward, out gunHit, 500))
        {
            if (gunHit.transform.gameObject.name == "Body" && SaveScript.weaponID == 4)
            {
                if (Input.GetMouseButtonDown(0) && SaveScript.currentAmmo[4] > 0)
                {
                    gunHit.transform.gameObject.GetComponent<ZombieGunDamage>().SendGunDamage(gunHit.point);
                }
            }
        }
        if (SaveScript.weaponID == 5 && SaveScript.currentAmmo[5] > 0)
        {
            shotGunHit = Physics.SphereCastAll(transform.position, 0.3f, transform.forward, 50);

            for(int i = 0; i < shotGunHit.Length; i++)
            {
                if (shotGunHit[i].transform.gameObject.name == "Body")
                {
                    if(Input.GetMouseButtonDown(0))
                    {
                        shotGunHit[i].transform.gameObject.GetComponent<ZombieGunDamage>().SendGunDamage(shotGunHit[i].point);
                    }
                }
            }
        }
    }
}
