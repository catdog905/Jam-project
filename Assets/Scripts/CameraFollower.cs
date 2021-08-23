using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollower : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }
    [SerializeField]
    private Transform whoToFollow;

    // Update is called once per frame
    void Update()
    {
        if (whoToFollow != null)
            transform.position = new Vector3(whoToFollow.position.x,whoToFollow.position.y,transform.position.z);
    }
}
