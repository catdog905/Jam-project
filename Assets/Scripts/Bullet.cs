using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private float speed=5f;
    [SerializeField]
    private int damage = 1;
    private Rigidbody2D rb;

    void Awake(){
        rb = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        
    }
    
    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.gameObject.tag != "Wall"){
            Character character = collision.collider.gameObject.GetComponent<Character>();
            if (character != null){
                character.TakeDamage(damage);
                Destroy(gameObject);
            }
            return;
        }
        int repeatCoefficient = 1;
        if (alreadyHadCollisionThisFrame){

            repeatCoefficient = -1;
        }
        alreadyHadCollisionThisFrame = true;
        if (collision.otherCollider.name == "collRight"){
            transform.rotation = Quaternion.Euler(0,0,transform.rotation.eulerAngles.z+90*repeatCoefficient);
        }else{
            transform.rotation = Quaternion.Euler(0,0,transform.rotation.eulerAngles.z-90*repeatCoefficient);
        }
    }
    private bool alreadyHadCollisionThisFrame = false;

    void FixedUpdate(){
        alreadyHadCollisionThisFrame = false;
    }

    // Update is called once per frame
    void Update()
    {
        rb.velocity = transform.up*speed;
    }
}
