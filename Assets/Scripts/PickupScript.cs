using System.Collections;
using System.Collections.Generic;
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
    public Text doorMessage;
    public AudioClip[] pickupSounds;

    // Start is called before the first frame update
    void Start()
    {
        PickupPanel.SetActive(false);
        audioPlayer = GetComponent<AudioSource>();
        doorMessageObj.SetActive(false);
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
                        SaveScript.ammoAmts[objID]++;
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
                        hit.transform.gameObject.GetComponent<DoorType>().message = "Locked. You need to use the " + hit.transform.gameObject.GetComponent<DoorType>().ChooseDoor + " key";
                    }
                    if (hit.transform.gameObject.GetComponent<DoorType>().locked == false)
                    {
                        hit.transform.gameObject.GetComponent<DoorType>().message = "Press E to open the door";
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

            }
            else
            {
                PickupPanel.SetActive(false);
                doorMessageObj.SetActive(false);
                SaveScript.doorObject = null;
            }
        }
    }
}
