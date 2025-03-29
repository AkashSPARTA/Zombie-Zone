using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class LookMode : MonoBehaviour
{
    private PostProcessVolume vol;
    public PostProcessProfile Standard, Nightvision, inventory;
    public GameObject NightVisionOverlay;
    public GameObject FlashLightOverlay, FlashLight, inventoryMenu;
    private bool nightvisionOn = false;
    private bool flashLightOn = false;

    // Start is called before the first frame update
    void Start()
    {
        vol = GetComponent<PostProcessVolume>();
        NightVisionOverlay.SetActive(false);
        FlashLightOverlay.SetActive(false);
        FlashLight.SetActive(false);
        inventoryMenu.SetActive(false);
        vol.profile = Standard;
    }

    // Update is called once per frame
    void Update()
        // NightVision
    {
        if(Input.GetKeyUp(KeyCode.N))
        {
            if (SaveScript.inventoryOpen == false)
            {
                if (nightvisionOn == false)
                {
                    vol.profile = Nightvision;
                    NightVisionOverlay.SetActive(true);
                    nightvisionOn = true;
                    NightvisionOff();
                }
                else if (nightvisionOn == true)
                {
                    vol.profile = Standard;
                    NightVisionOverlay.SetActive(false);
                    this.gameObject.GetComponent<Camera>().fieldOfView = 60;
                    nightvisionOn = false;
                }
            }
        }

        // Inventory
        if (Input.GetKeyUp(KeyCode.I))
        {
            if (SaveScript.inventoryOpen == false)
            {
                vol.profile = inventory;
                inventoryMenu.SetActive(true);
                SaveScript.inventoryOpen = true;

                if (flashLightOn == true)
                {
                    FlashLight.SetActive(false);
                    FlashLightOverlay.SetActive(false);
                    flashLightOn = false;
                }
                if (nightvisionOn == true)
                {
                    NightVisionOverlay.SetActive(false);
                    this.gameObject.GetComponent<Camera>().fieldOfView = 60;
                    nightvisionOn = false;
                }
            }
            else 
            {
                vol.profile = Standard;
                inventoryMenu.SetActive(false);
                SaveScript.inventoryOpen = false;
            }
        }

        // Flashlight
        if (Input.GetKeyUp(KeyCode.F))
        {
            if (SaveScript.inventoryOpen == false)
            {
                if (flashLightOn == false)
                {
                    FlashLight.SetActive(true);
                    FlashLightOverlay.SetActive(true);
                    flashLightOn = true;
                }
                else if (flashLightOn == true)
                {
                    FlashLight.SetActive(false);
                    FlashLightOverlay.SetActive(false);
                    flashLightOn = false;
                }
            }
        }

        if (nightvisionOn == true)
        {
            NightvisionOff();
        }

        if (flashLightOn == true)
        {
            FlashLightOff();
        }
    }

    private void NightvisionOff()
    {
        if (NightVisionOverlay.GetComponent<NightVision>().BatteryPower.fillAmount <= 0)
        {
            vol.profile = Standard;
            NightVisionOverlay.SetActive(false);
            this.gameObject.GetComponent<Camera>().fieldOfView = 60;
            nightvisionOn = false;
        }
    }

    private void FlashLightOff()
    {
        if (FlashLightOverlay.GetComponent<FlashLight>().FLBatteryPower.fillAmount <= 0)
        {
            FlashLight.SetActive(false);
            FlashLightOverlay.SetActive(false);
            flashLightOn= false;
        }
    }
}
