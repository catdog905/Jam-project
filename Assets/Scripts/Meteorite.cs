using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meteorite : MonoBehaviour
{
    private static System.Random rnd = new System.Random();
    // Start is called before the first frame update
    void Awake(){
        meteorites.Add(this);
        colliderOfAOE.enabled=false;
    }
    void Start()
    {
        
    }
    public static List<Meteorite> meteorites = new List<Meteorite>();
    [SerializeField] CircleCollider2D colliderOfAOE;
    [SerializeField] GameObject bulletPrefab;
    public void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.name == "WinField" || collider.gameObject.name.StartsWith("Ultra")){
            Physics2D.IgnoreCollision(collider,colliderOfAOE);
            return;
          //  GetComponent<Rigidbody2D>().
        }
        OnTriggerStay2D(collider);
    }
    public void OnTriggerStay2D(Collider2D collider)
    {
        Destroy(collider.gameObject);
    }
    [SerializeField] SpriteRenderer spriteRenderer;
    IEnumerator timelyExplosion(){
        float timePassed = 0;
        while (true){
        if (timePassed >= timeForExplosion){
            // Explode
            colliderOfAOE.enabled=true;
            transform.position+=new Vector3(0.01f,0.01f);
            CameraFollower.singleton.explosionSound1.Play();
            Vector3 bulPos = transform.position;
            bulPos.z=0;
            Vector3 rotEuler = new Vector3();
            rotEuler.z=45 + 90 * rnd.Next(0, 3);
            Instantiate(bulletPrefab,bulPos,Quaternion.Euler(rotEuler));
            Destroy(spriteRenderer);
            break;
        }
        float currentState = 0.7f-(timePassed/timeForExplosion)/2;
        spriteRenderer.color = new Color(currentState,currentState,currentState);


        timePassed+=Time.deltaTime;
        yield return null;
        }
    }
    public float radius,timeForExplosion;
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
