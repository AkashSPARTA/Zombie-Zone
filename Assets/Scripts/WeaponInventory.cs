using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponInventory : MonoBehaviour
{
    public Sprite[] bigIcons;
    public Image bigIcon;
    public string[] titles;
    public Text title;
    public string[] descriptions;
    public Text description;
    public Button[] weaponButtons;
    public Text amtsText;

    private AudioSource audioPlayer;
    public AudioClip click, select;
    private int chosenWeaponNumber;

    public GameObject useButton, combineButton;
    public GameObject combinePanel, combineUseButton;
    public Image[] combineItems;
    // Start is called before the first frame update
    void Start()
    {
        audioPlayer = GetComponent<AudioSource>();
        bigIcon.sprite = bigIcons[0];
        title.text = titles[0];
        description.text = descriptions[0];

        combinePanel.SetActive(false);
        combineButton.SetActive(false);

    }

    private void OnEnable()
    {
        for (int i = 0; i < weaponButtons.Length; i++)
        {
            if (SaveScript.weaponsPickedUp[i] == true)
            {
                weaponButtons[i].image.color = new Color(1, 1, 1, 1);
                weaponButtons[i].image.raycastTarget = true;
            }
            else
            {
                weaponButtons[i].image.color = new Color(1, 1, 1, 0.06f);
                weaponButtons[i].image.raycastTarget = false;
            }
        }

        if (chosenWeaponNumber < 6)
        {
            combinePanel.SetActive(false);
            combineButton.SetActive(false);
        }

        if (SaveScript.itemPickedUp[2] == true)
        {
            combineItems[0].color = new Color(1, 1, 1, 1);
        }
        else
        {
            combineItems[0].color = new Color(1, 1, 1, 0.06f);
        }
        if (SaveScript.itemPickedUp[3] == true)
        {
            combineItems[1].color = new Color(1, 1, 1, 1);
        }
        else
        {
            combineItems[1].color = new Color(1, 1, 1, 0.06f);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChooseWeapon(int weaponNumber)
    {
        bigIcon.sprite = bigIcons[weaponNumber];
        title.text = titles[weaponNumber];
        description.text = descriptions[weaponNumber];
        audioPlayer.clip = click;
        audioPlayer.Play();
        chosenWeaponNumber = weaponNumber;
        amtsText.text = "Amt: " + SaveScript.weaponAmts[weaponNumber];

        if (chosenWeaponNumber > 5)
        {
            combineButton.SetActive(true);
            combinePanel.SetActive(false);
        }
        if (chosenWeaponNumber < 6)
        {
            combinePanel.SetActive(false);
            combineButton.SetActive(false);
        }
    }

    public void CombineAction()
    {
        combinePanel.SetActive(true);

        if (chosenWeaponNumber == 6)
        {
            combineItems[1].transform.gameObject.SetActive(false);
            if (SaveScript.itemPickedUp[2] == true)
            {
                combineUseButton.SetActive(true);
            }
            else
            {
                combineUseButton.SetActive(false);
            }
        }

        if (chosenWeaponNumber == 7)
        {
            combineItems[1].transform.gameObject.SetActive(true);
            if (SaveScript.itemPickedUp[2] == true && SaveScript.itemPickedUp[3] == true)
            {
                combineUseButton.SetActive(true);
            }
            else
            {
                combineUseButton.SetActive(false);
            }
        }
    }

    public void CombineAssignWeapon()
    {
        if (chosenWeaponNumber == 6)
        {
            SaveScript.weaponID = chosenWeaponNumber;
        }
        if (chosenWeaponNumber == 7)
        {
            SaveScript.weaponID = chosenWeaponNumber += 1;
        }

        audioPlayer.clip = select;
        audioPlayer.Play();
    }
    public void AssignWeapon()
    {
        SaveScript.weaponID = chosenWeaponNumber;
        audioPlayer.clip = select;
        audioPlayer.Play();
    }
}
