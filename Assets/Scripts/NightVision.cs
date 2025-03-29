using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NightVision : MonoBehaviour
{
    public Image zoomBar;
    public Image BatteryPower;
    public Camera cam;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnEnable()
    {
        if (zoomBar != null)
        zoomBar.fillAmount = 0.6f;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetAxis("Mouse ScrollWheel") >  0)
        {
            if (cam.fieldOfView > 10)
            {
                cam.fieldOfView -= 5;
                zoomBar.fillAmount = cam.fieldOfView / 100;
            }
        }
        if (Input.GetAxis("Mouse ScrollWheel") < 0)
        {
            if (cam.fieldOfView < 60)
            {
                cam.fieldOfView += 5;
                zoomBar.fillAmount = cam.fieldOfView / 100;
            }
        }
        BatteryPower.fillAmount -= 0.05f * Time.deltaTime;
    }
}
