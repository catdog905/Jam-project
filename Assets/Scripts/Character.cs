using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{

    private Rigidbody2D rb;
    
    [SerializeField]
    private float speed=10f,anglularSpeed=10f;
    [SerializeField]
    private int hp = 1;

    Vector2 movementDirection = Vector2.zero;
    Vector2 placeToLookAt = Vector2.zero;

    void Start()
    {
        
    }

    void Awake(){
        rb = GetComponent<Rigidbody2D>();
    }

    public void TakeDamage(int damage){
        hp -= damage;
        if (hp <= 0){
            Destroy(gameObject);
        }
    }

    public void SetMovement(Vector2 dir){
        movementDirection=dir;
    }

    public void SetPlaceToLookAt(Vector2 PTLA){
        placeToLookAt=PTLA;
    }

    void Update()
    {
        // Transforming Vector2 into Vector3
        Vector3 worldPosition = placeToLookAt;
        worldPosition.z = transform.position.z;

        Quaternion dir = transform.rotation;
        Vector3 change = worldPosition-transform.position;

        // Angle math
        int dop = 0;
        if (change.y>0)
            dop = 180;
        Quaternion finish = Quaternion.Euler(0,0,dop-Mathf.Atan(change.x/change.y)*Mathf.Rad2Deg-90);

        // Slow rotation
        dir = Quaternion.Lerp(dir,finish,Time.deltaTime*anglularSpeed);
        transform.rotation=dir;
        
        // Movement
        rb.velocity =movementDirection*speed;
    }
}
