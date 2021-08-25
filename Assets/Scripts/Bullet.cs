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
    public void OnCollisionStay2D(Collision2D collision)
    {

        // We assume that it's only possible to collide:
        // with walls,
        // with characters

        Character character = collision.collider.gameObject.GetComponent<Character>();
        if (character != null)
        {
            // Character
            character.TakeDamage(damage);
            Destroy(gameObject);
            return;
        }

        // Wall

        if (collision.otherCollider.name == "collRight")
        {
            colRight = true;
        }
        else
        {
            colLeft = true;
        }

    }


    IEnumerator manageDoubleCornerAllowance()
    {
        yield return new WaitForSeconds(0.05f);
        doubleCornerAllowed = true;
    }
    private bool colRight = false, colLeft = false;
    private bool doubleCornerAllowed = true;
    void FixedUpdate()
    {
        if (colRight && colLeft)
        {
            if (doubleCornerAllowed)
            {
                transform.rotation = Quaternion.Euler(0, 0, transform.rotation.eulerAngles.z + 180);
                doubleCornerAllowed = false;
            }
        }
        else if (colRight)
        {
            transform.rotation = Quaternion.Euler(0, 0, transform.rotation.eulerAngles.z + 90);
        }
        else if (colLeft)
        {
            transform.rotation = Quaternion.Euler(0, 0, transform.rotation.eulerAngles.z - 90);
        }
        colRight = colLeft = false;
    }

    void Update()
    {
        rb.velocity = transform.up * speed;
    }
}
