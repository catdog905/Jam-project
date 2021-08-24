using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollower : MonoBehaviour
{
    void Start()
    {
        
    }
    [SerializeField]
    private Transform whoToFollow;

    void Update()
    {
        if (whoToFollow != null)
            transform.position = new Vector3(whoToFollow.position.x,whoToFollow.position.y,transform.position.z);
    }
}
