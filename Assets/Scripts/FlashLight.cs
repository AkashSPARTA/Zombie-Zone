using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FlashLight : MonoBehaviour
{
    public Image FLBatteryPower;
    public float PowerDrain = 0.05f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        FLBatteryPower.fillAmount -= PowerDrain * Time.deltaTime;
    }
}
