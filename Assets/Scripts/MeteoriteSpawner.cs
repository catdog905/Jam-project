using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteoriteSpawner : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private GameObject meteoritePrefab;

    [SerializeField]
    private CircleMap map;

    [SerializeField]
    private float minSecondsToWait=1f,maxSecondsToWait=10f,minMeteoriteSize=0.01f,maxMeteoriteSize=0.1f,minExplosionTime=2f,maxExplosionTime=5f;

    [SerializeField]
    private float radius;
    IEnumerator autoSpawnMeteorites(){
        while (true){
            float secondsToSpawnNext = Random.Range(minSecondsToWait,maxSecondsToWait);
            float secondsToExplode = Random.Range(minExplosionTime,maxExplosionTime);
            float metoriteRadius = Random.Range(minMeteoriteSize,maxMeteoriteSize);

            Vector2 meteoritePosition =  (Vector2)transform.position+(Random.insideUnitCircle)*radius;
            Instantiate(meteoritePrefab,meteoritePosition,Quaternion.identity).GetComponent<Meteorite>().Initialize(metoriteRadius,secondsToExplode);





            yield return new WaitForSeconds(secondsToSpawnNext);
        }        
    }
    void Awake(){
        StartCoroutine(autoSpawnMeteorites());
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
