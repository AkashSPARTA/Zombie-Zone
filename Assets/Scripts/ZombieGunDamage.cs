using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieGunDamage : MonoBehaviour
{
    public GameObject zombieDamageObj;
    public GameObject flames;
    public Material skinBurn;
    public GameObject[] LODs;
    private Animator bodyAnim;

    private void Start()
    {
        bodyAnim = GetComponent<Animator>();
    }

    public void SendGunDamage(Vector3 hitpoint)
    {
        zombieDamageObj.GetComponent<ZombieDamage>().gunDamage(hitpoint);
    }

    private void OnParticleCollision(GameObject other)
    {
        zombieDamageObj.GetComponent<ZombieDamage>().FlameDeath();
        flames.SetActive(true);
        foreach (GameObject skin in LODs)
        {
            skin.GetComponent<Renderer>().material = skinBurn;
        }
        
        bodyAnim.SetTrigger("burn");
    }
}
