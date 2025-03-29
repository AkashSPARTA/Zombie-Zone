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
        if (Physics.SphereCast(transform.position, 0.5f, transform.forward, out hit, 30, ~excludeLayer))
        {
            if (Vector3.Distance(transform.position, hit.transform.position) < pickupDisplayDistance)
            {
                if (hit.transform.gameObject.CompareTag("weapon"))
                {
                    PickupPanel.SetActive(true);
                    objID = (int)hit.transform.gameObject.GetComponent<WeaponType>().chooseWeapon;
                    mainImage.sprite = weaponIcons[objID];
                    mainTitle.text = weaponTitles[objID];

                    if (Input.GetKeyDown(KeyCode.E))
                    {
                        SaveScript.weaponAmts[objID]++;
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
                        audioPlayer.Play();
                        SaveScript.changeItems = true;
                        Destroy(hit.transform.gameObject, 0.2f);
                    }
                }
                else if (hit.transform.gameObject.CompareTag("door"))
                {
                    objID = (int)hit.transform.gameObject.GetComponent<DoorType>().ChooseDoor;

                    doorMessageObj.SetActive(true);
                    doorMessage.text = hit.transform.gameObject.GetComponent<DoorType>().message;
                    if (Input.GetKeyDown(KeyCode.E))
                    {
                        audioPlayer.Play();
                    }
                }

            }
            else
            {
                PickupPanel.SetActive(false);
                doorMessageObj.SetActive(false);
            }
        }
    }
}
