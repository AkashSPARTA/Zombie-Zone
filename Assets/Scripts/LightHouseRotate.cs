using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightHouseRotate : MonoBehaviour
{
    public float roatateSpeed = 3.5f;

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.forward * roatateSpeed * Time.deltaTime);
    }
}
