using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ItemInventory : MonoBehaviour
{
    public Sprite[] bigIcons;
    public Image bigIcon;
    public string[] titles;
    public Text title;
    public string[] descriptions;
    public Text description;
    public Button[] itemButtons;
    public GameObject useButton;
    public Text amtsText;

    private AudioSource audioPlayer;
    public AudioClip click, select;
    private int chosenItemNumber;

    // Start is called before the first frame update
    void Start()
    {
        audioPlayer = GetComponent<AudioSource>();

        bigIcon.sprite = bigIcons[0];
        title.text = titles[0];
        description.text = descriptions[0];
        useButton.SetActive(false);


    }

    private void OnEnable()
    {
        
        for (int i = 0; i < itemButtons.Length; i++)
        {
            
            if (SaveScript.itemPickedUp[i] == true)
            {
                itemButtons[i].image.color = new Color(1, 1, 1, 1);
                itemButtons[i].image.raycastTarget = true;
                
            }
            else
            {
                itemButtons[i].image.color = new Color(1, 1, 1, 0.06f);
                itemButtons[i].image.raycastTarget = false;
                
            }
        }
    }

    public void ChooseItem(int itemNumber)
    {
        bigIcon.sprite = bigIcons[itemNumber];
        title.text = titles[itemNumber];
        description.text = descriptions[itemNumber];
        audioPlayer.clip = click;
        audioPlayer.Play();
        chosenItemNumber = itemNumber;
        amtsText.text = "Amt: " + SaveScript.itemAmts[itemNumber];

        if (itemNumber < 4)
        {
            useButton.SetActive(false);
        }
        else
        {
            useButton.SetActive(true);
        }
    }

    public void AssignItems()
    {
        SaveScript.itemID = chosenItemNumber;
        audioPlayer.clip = select;
        audioPlayer.Play();
    }
}
