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
    public float period = 1.5f;
    int ptr = 1;
    List<Vector2> path;
    float timeToDoNext = 0;
    private List<GameObject> GetCurrentThreats(){
        Vector3 pos = transform.position;
        List<GameObject> threats = new List<GameObject>();
        foreach(Meteorite meteorite in Meteorite.meteorites){
            float xDif = meteorite.transform.position.x-pos.x;
            float yDif = meteorite.transform.position.y-pos.y;
            if (yDif*yDif + xDif*xDif <= meteorite.radius){
                threats.Add(meteorite.gameObject);
            }
        }
        foreach(Bomb bomb in Bomb.bombs){
            float xDif = bomb.transform.position.x-pos.x;
            float yDif = bomb.transform.position.y-pos.y;
            if (yDif*yDif + xDif*xDif <= bomb.radius){
                threats.Add(bomb.gameObject);
            }
        }
        return threats;
    }
    private void GoToTarget() {
        Transform target = CameraFollower.singleton.whoToFollow;
        Vector2 currentPosition = transform.position;
        if (Time.time > timeToDoNext){
            ptr = 1;
            timeToDoNext = Time.time+period+Random.Range(0f,1f);
            path = searchPath.GetShortestWay(currentPosition, target.position);
        }
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
        character.SetPlaceToLookAt(target.position);





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
