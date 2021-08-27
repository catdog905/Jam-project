using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bot : MonoBehaviour
{
    public GameObject target;
    public float distanceToEnemy = 10;
    public GameObject mapGeneration;
    public float spead = 0.00005f;

    private Vector2 currentMoveTarget;
    private BreadthFirstSearch searchPath; 
    private Character character;
    private List<Vector2> currentPath;
    private int curNextPath = 1;

    void Awake()
    {
    }

    void Start()
    { 
        character = GetComponent<Character>();
        CircleMap circleMap = mapGeneration.GetComponent<CircleMap>();
        searchPath = new BreadthFirstSearch(circleMap);
    }

    void Update()
    {
        
        GoToTarget();
    }
    
    private float nextActionTime = 0.0f;
    public float period = 0.5f;
    private void GoToTarget() {
        Vector2 currentPosition = transform.position;
        List<Vector2> path = searchPath.GetShortestWay(currentPosition, target.transform.position);
        Vector2 goal;
        Debug.Log(path[0]);
        Debug.Log(path[1]);
        //if (Mathf.Abs(path[0].x-path[1].x)<1f&&Mathf.Abs(path[0].y-path[1].y)<0.9f){
        Debug.Log(true);
            goal = path[2];

        //}
        //else{
        Debug.Log(false);
         //   goal = path[1];
        //}

        Vector2 dir = (goal - currentPosition);
        character.SetMovement(dir);
        character.SetPlaceToLookAt(target.transform.position);





        return;
         currentPosition = transform.position;
        if (Time.time > nextActionTime) {
            nextActionTime =  Time.time + period;
            currentPath = searchPath.GetShortestWay(currentPosition, target.transform.position);
            curNextPath = 1;
        }
        if (currentPath != null) {
            Debug.Log(curNextPath + " " + currentPath.Count);
            if (curNextPath >= currentPath.Count)
                return;
            if (Mathf.Abs(currentPath[curNextPath].x - currentPosition.x) <= 0.5 && Mathf.Abs(currentPath[curNextPath].y - currentPosition.y) <= 0.5)
                curNextPath++;
            var heading = currentPath[curNextPath] - currentPosition;
            var distance = heading.magnitude;
            var direction = heading / distance;
            character.SetMovement(direction);
        //character.SetPlaceToLookAt(new Vector2(path[0].x - currentPosition.x, path[1].y - currentPosition.y));
        //
        }
    }
}
