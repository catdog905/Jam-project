using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bot : MonoBehaviour
{
    public float distanceToEnemy = 10;
    public GameObject mapGeneration;
    public float spead = 0.00005f;

    private Vector2 currentMoveTarget;
    private static BreadthFirstSearch searchPath; 
    private Character character;
    private List<Vector2> currentPath;
    private int curNextPath = 1;
    private BotDecision botDecision;

    void Awake()
    {
    }

    void Start()
    { 
        character = GetComponent<Character>();
        CircleMap circleMap = mapGeneration.GetComponent<CircleMap>();
        if (searchPath == null)
            searchPath = new BreadthFirstSearch(circleMap);
        botDecision = GetComponent<BotDecision>();
    }

    void Update()
    {
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
            ptr = 1;
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
        character.SetMovement(dir);
        character.SetPlaceToLookAt(target);
    }
}
