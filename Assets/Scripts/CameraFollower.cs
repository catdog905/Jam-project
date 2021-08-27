using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollower : MonoBehaviour
{
    void Start()
    {
        
    }
    void Awake(){
        singleton=this;
    }
    public AudioSource explosionSound1,explosionSound2;
    public static CameraFollower singleton;
    [SerializeField]
    private GameObject wallFather,playerPrefab;
    [SerializeField]
    public Transform whoToFollow;
    bool workingOnRespawn = false;
    IEnumerator confirmRespawn(){
        workingOnRespawn=true;

        while (true){
            Vector3 coords = (Random.insideUnitCircle.normalized*1.1f+ new Vector2(1,1))/2;
            coords*=CircleMap.singleton.mapSideLength;
            Character character = Instantiate(playerPrefab,coords,Quaternion.identity).GetComponent<Character>();
            character.wallFather=wallFather;
            yield return new WaitForSeconds(0.5f);
            if (character!= null)
            {
                whoToFollow=character.gameObject.transform;
                break;
            }
        }

        workingOnRespawn=false;
    }

    void Update()
    {
        if (whoToFollow != null)
            transform.position = new Vector3(whoToFollow.position.x,whoToFollow.position.y,transform.position.z);
        else if (!workingOnRespawn){
            StartCoroutine(confirmRespawn());
        }
    }
}
