using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CameraFollower : MonoBehaviour
{
    void Start()
    {
        characters = new GameObject[enemiesToSpawn+1];
        for (int i = 0; i<enemiesToSpawn;++i){
         //   StartCoroutine(confirmSpawn(enemyPrefab));
        }
        StartCoroutine(BotAutoRespawner());
    }
    void Awake(){
        singleton=this;
    }
    private int ptr = 1;
    public AudioSource explosionSound1,explosionSound2;
    public static GameObject[] characters;
    [SerializeField] GameObject enemyPrefab;
    [SerializeField] int enemiesToSpawn=5;
    public static CameraFollower singleton;
    [SerializeField]
    private GameObject wallFather,playerPrefab;
    [SerializeField]
    public Transform whoToFollow;

    IEnumerator BotAutoRespawner(){
        while (true){
            for (int i = 1; i < characters.Length;++i){
                if (characters[i] == null){
                    yield return confirmSpawn(enemyPrefab,false,i);
                }

                yield return null;
            }
        }
    }
    bool workingOnRespawn = false;
    IEnumerator confirmSpawn(GameObject who, bool isPlayer = false,int repsawnId = -1){
        int lptr;
        if (isPlayer){
            workingOnRespawn=true;
            lptr = 0;
        }   
        else
            lptr = ptr++;
        if (repsawnId != -1){
            lptr = repsawnId;
        }
        while (true){
            Vector3 coords = (Random.insideUnitCircle.normalized*1.1f+ new Vector2(1,1))/2;
            coords*=(CircleMap.singleton.mapSideLength-100);
            coords += new Vector3(100,100);
            Character character = Instantiate(who,coords,Quaternion.identity).GetComponent<Character>();
            characters[lptr]=character.gameObject;
            if (!isPlayer){
                character.gameObject.GetComponent<Bot>().mapGeneration=CircleMap.singleton.gameObject;
            }
            character.wallFather=wallFather;
            yield return new WaitForSeconds(0.5f);
            if (character!= null)
            {
                if (isPlayer)
                    whoToFollow=character.gameObject.transform;
                break;
            }
        }

        if (isPlayer)
        workingOnRespawn=false;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R)){
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex,LoadSceneMode.Single);
        }
        if (whoToFollow != null)
            transform.position = new Vector3(whoToFollow.position.x,whoToFollow.position.y,transform.position.z);
        else if (!workingOnRespawn){
            StartCoroutine(confirmSpawn(playerPrefab,true));
        }
    }
}
