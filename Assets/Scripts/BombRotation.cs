using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombRotation : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public float rotationSpeed = 5f;

    // Update is called once per frame
    void Update()
    {
        Vector3 rot = transform.rotation.eulerAngles;

        rot.z+=Time.deltaTime*rotationSpeed;

        transform.rotation = Quaternion.Euler(rot);


    }
}
