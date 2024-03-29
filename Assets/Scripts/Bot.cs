using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bot : MonoBehaviour
{
    public float distanceToEnemy = 10;
    public GameObject mapGeneration;
    public float spead = 0.00005f;

    private Vector2 currentMoveTarget;
    private BreadthFirstSearch searchPath; 
    private Character character;
    private List<Vector2> currentPath;
    private int curNextPath = 1;
    private BotDecision botDecision;
    [SerializeField]
    private LayerMask layerToWall;

    void Awake()
    {
    }

    void Start()
    { 
        character = GetComponent<Character>();
        CircleMap circleMap = mapGeneration.GetComponent<CircleMap>();
        searchPath = new BreadthFirstSearch(circleMap);
        botDecision = GetComponent<BotDecision>();
    }

    void Update()
    {
//        Debug.Log(botDecision);
        if (botDecision.isAttack)
            character.PlaceBombInDirection(botDecision.attackDirection);
        GoToTarget(botDecision.goTarget);
    }
    
    private float nextActionTime = 0.0f;
    public float period = 1.5f;
    int ptr = 1;
    List<Vector2> path;
    float timeToDoNext = 0;
    private void GoToTarget(Vector2 target) {
        Vector2 currentPosition = transform.position;
        if (Time.time > timeToDoNext){
            timeToDoNext = Time.time+period+Random.Range(0f,1f);
            searchPath.OrderAWay(currentPosition,target);
           // path = searchPath.GetShortestWay(currentPosition, target.position);
        }
        List<Vector2> newPath = searchPath.takeAWay();
        if (newPath != null){
            path=newPath;
            float minLen = 9999;
            for (int i = 1 ; i < Mathf.Min(path.Count,10);++i){
                float mag = ((Vector2)transform.position - path[i]).magnitude;
                if (mag < minLen){
                    minLen=mag;
                    ptr = i+1;
                }
            }
        }
        if (path == null)
            return;
        Vector2 goal;
        if (Mathf.Abs(transform.position.x-path[ptr].x)<0.1f&&Mathf.Abs(transform.position.y-path[ptr].y)<0.1f){
            ptr++;
        }
            goal = path[ptr];

        //}
        //else{
         //   goal = path[1];
        //}
        
        Vector2 dir = (goal - currentPosition);
        RaycastHit2D hit = Physics2D.Raycast(transform.position, dir, 20, layerToWall);
        if (hit.collider != null)
            character.PlaceBombInDirection(dir);
        character.SetMovement(dir);
        character.SetPlaceToLookAt(target);
    }
}
