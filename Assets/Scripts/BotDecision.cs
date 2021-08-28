using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotDecision : MonoBehaviour
{
    private static System.Random rnd = new System.Random();

    public GameObject standartTarget;
    public Vector2 attackDirection;
    public Vector2 goTarget;
    public Vector2 buildDirection;
    public bool isAttack = false;

    private Vector2 item;
    private bool isItemUp;
    private Vector2 botPositoin;
    private Character character;
    [SerializeField]
    private LayerMask layersToStopExplosion;

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
        goTarget = standartTarget.transform.position;
        List<GameObject> enemies = GetEnemiesNear();
        if (GetCurrentThreatsOnPos(transform.position).Count == 0){
            goTarget = FindSafePlace();
        } 
        List<GameObject> threats  = GetCurrentThreatsOnPos(transform.position);
        if (threats.Count != 0)
            goTarget = FindSafePlace();
        if (enemies.Count != 0){
            isAttack = true;
            attackDirection = GetDirectionVector(transform.position, enemies[rnd.Next(0, enemies.Count - 1)].transform.position);
        }else
            isAttack = false;
    }

    Vector2 GetDirectionVector(Vector2 start, Vector2 stop) {
        var heading = stop - start;
        var distance = heading.magnitude;
        return heading / distance;
    }
    
    private List<GameObject> GetEnemiesNear() {
        List<GameObject> nearEnemies = new List<GameObject>();
        GameObject[] players = CameraFollower.characters;
        Debug.Log(players.Length);
        foreach (GameObject player in players) {
            if (player != transform.gameObject && player != null){
                RaycastHit2D hit = Physics2D.Raycast(transform.position, GetDirectionVector(transform.position, player.transform.position), GetDistanceBetweenVectors(transform.position, player.transform.position), layersToStopExplosion);
                if (hit.collider == null){
                    nearEnemies.Add(player);
                }
            }
        }
        return nearEnemies;
    }

    private float GetDistanceBetweenVectors(Vector2 start, Vector2 stop){
        return (start - stop).magnitude;
    }

    private List<GameObject> GetCurrentThreatsOnPos(Vector2 pos){
        List<GameObject> threats = new List<GameObject>();
        foreach(Meteorite meteorite in Meteorite.meteorites){
            float xDif = meteorite.transform.position.x-pos.x;
            float yDif = meteorite.transform.position.y-pos.y;
            if (yDif*yDif + xDif*xDif <= meteorite.radius){
                threats.Add(meteorite.gameObject);
            }
        }
        foreach(Bomb bomb in Bomb.bombs){
            if (bomb.rbVelocity != Vector2.zero) {
                float xDif = bomb.transform.position.x-pos.x;
                float yDif = bomb.transform.position.y-pos.y;
                if (yDif*yDif + xDif*xDif <= bomb.radius){
                    threats.Add(bomb.gameObject);
                }
            }
        }
        return threats;
    }

    private Vector2 FindSafePlace() {
        Vector2 curPos = transform.position;
        for (int i = (int)curPos.x - 3; i < curPos.x + 3; i++) {
            for (int j = (int)curPos.y - 3; j < curPos.y + 3; j++) {
                if (CircleMap.singleton.map2D[i][j] == CircleMap.CellType.Floor) 
                    if (GetCurrentThreatsOnPos(new Vector2(i, j)).Count == 0)
                            return new Vector2(i, j);
            }
        }
        return curPos;
    }
}
