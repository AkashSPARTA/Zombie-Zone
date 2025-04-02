using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class MainGUI : MonoBehaviour
{
    public Text healthAmt, staminaAmt, infectionAmt;

    // Update is called once per frame
    void Update()
    {
        healthAmt.text = SaveScript.health + "%";
        staminaAmt.text = SaveScript.stamina.ToString("F0") + "%";
        infectionAmt.text = SaveScript.infection.ToString("F0") + "%";
    }
}
