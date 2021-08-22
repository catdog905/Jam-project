using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }
    void Awake(){
        rb = GetComponent<Rigidbody2D>();
    }    

    private Rigidbody2D rb;
    [SerializeField]
    private float speed=10f,anglularSpeed=10f;

    Vector2 movementDirection = Vector2.zero;
    Vector2 placeToLookAt = Vector2.zero;
    public void SetMovement(Vector2 dir){
        movementDirection=dir;
    }
    public void SetPlaceToLookAt(Vector2 PTLA){
        placeToLookAt=PTLA;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 worldPosition;
        worldPosition = placeToLookAt;
        worldPosition.z = transform.position.z;


        Quaternion dir = transform.rotation;
        Vector3 change = worldPosition-transform.position;

        int dop = 0;
        if (change.y>0)
            dop = 180;
        Quaternion finish = Quaternion.Euler(0,0,dop-Mathf.Atan(change.x/change.y)*Mathf.Rad2Deg-90);

        
        dir = Quaternion.Lerp(dir,finish,Time.deltaTime*anglularSpeed);
        transform.rotation=dir;
        
        rb.velocity =movementDirection*speed;
    }
}
