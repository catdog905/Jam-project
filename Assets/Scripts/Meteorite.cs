using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meteorite : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }
    [SerializeField] CircleCollider2D colliderOfAOE;
    public void OnTriggerEnter2D(Collider2D collider)
    {
        OnTriggerStay2D(collider);
    }
    public void OnTriggerStay2D(Collider2D collider)
    {
        Destroy(collider.gameObject);
    }
    void Awake(){
        colliderOfAOE.enabled=false;
    }

    [SerializeField] SpriteRenderer spriteRenderer;
    IEnumerator timelyExplosion(){
        float timePassed = 0;
        while (true){
        if (timePassed >= timeForExplosion){
            // Explode
            colliderOfAOE.enabled=true;
            transform.position+=new Vector3(0.01f,0.01f);
            break;
        }
        float currentState = 0.7f-(timePassed/timeForExplosion)/2;
        spriteRenderer.color = new Color(currentState,currentState,currentState);


        timePassed+=Time.deltaTime;
        yield return null;
        }
    }
    float radius,timeForExplosion;
    public void Initialize(float radius, float timeForExplosion){
        this.radius=radius;
        this.timeForExplosion=timeForExplosion;
        transform.localScale*=(radius*2f);
        StartCoroutine(timelyExplosion());
    }

    // Update is called once per frame
    void Update()
    {
    }
}
