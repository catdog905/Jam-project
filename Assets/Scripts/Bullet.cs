using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private float speed = 5f;
    [SerializeField]
    private int damage = 1;
    private Rigidbody2D rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Start()
    {

    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
    }
    public void OnCollisionStay2D(Collision2D collision){
        if (collision.collider.gameObject.tag != "Wall")
        {
            // Not wall
            Character character = collision.collider.gameObject.GetComponent<Character>();
            if (character != null)
            {
                // Character
                character.TakeDamage(damage);
                Destroy(gameObject);
            }
            return;
        }
        if (collision.otherCollider.name == "collRight"){
            colRight=true;
        }else{
            colLeft=true;
        }
     
    }
    private bool colRight = false,colLeft=false;

    void FixedUpdate()
    {   
        if (colRight && colLeft){
            transform.rotation = Quaternion.Euler(0, 0, transform.rotation.eulerAngles.z + 180 );
        }
        else if (colRight)
        {
            transform.rotation = Quaternion.Euler(0, 0, transform.rotation.eulerAngles.z + 90 );
        }
        else if (colLeft)
        {
            transform.rotation = Quaternion.Euler(0, 0, transform.rotation.eulerAngles.z - 90);
        }
        colRight=colLeft=false;
    }

    void Update()
    {
        rb.velocity = transform.up * speed;
    }
}
