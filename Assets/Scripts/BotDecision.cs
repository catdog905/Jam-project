using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotDecision : MonoBehaviour
{
    private static System.Random rnd = new System.Random();

    public Vector2 attackDirection;
    public Vector2 goTarget;
    public Vector2 buildDirection;

    private Vector2 item;
    private bool isItemUp;
    private Vector2 botPositoin;
    private Character character;

    // Start is called before the first frame update
    void Start()
    {
        character = GetComponent<Character>();
    }

    // Update is called once per frame
    void Update()
    {
        //get isItemUp
        //get item vector2
        //goTarget = item;
        List<GameObject> enemies = GetEnemiesNear();
        //if (IsInBombRadius()){
            //InWhichBombRadius();
          //  goTarget = botPosition + GetDirectionVector(InWhichBombRadius(), botPosition());//todo
        //} 
        if (enemies.Count != 0)
            attackDirection = GetDirectionVector(transform.position, enemies[rnd.Next(0, enemies.Count - 1)].transform.position);
    }

    Vector2 GetDirectionVector(Vector2 start, Vector2 stop) {
        var heading = stop - start;
        var distance = heading.magnitude;
        return heading / distance;
    }
    
    private List<GameObject> GetEnemiesNear() {
        List<GameObject> nearEnemies = new List<GameObject>();
        GameObject[] players = CameraFollower.characters;
        foreach (GameObject player in players) {
            if (player != transform.gameObject){
                RaycastHit2D hit = Physics2D.Raycast(transform.position, GetDirectionVector(transform.position, player.transform.position));
                if (hit.collider != null){
                    if (hit.transform.gameObject == player)
                        nearEnemies.Add(player);
                }
            }
        }
        return nearEnemies;
    }

    
}
