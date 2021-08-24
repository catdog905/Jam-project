using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    private Rigidbody2D rb;
    List<GameObject> objectsToBlowUp = new List<GameObject>();
    public void OnTriggerEnter2D(Collider2D collider)
    {
        if (!colliderOfAOE.enabled)
            return;
        GameObject colliderGO = collider.gameObject;
        if (!objectsToBlowUp.Contains(colliderGO))
        {
            objectsToBlowUp.Add(colliderGO);
        }
    }
    void Awake(){
        rb = GetComponent<Rigidbody2D>();
    }
    Vector2 rbVelocity = Vector2.zero;
    public void Initialize(float radius, float secondsToBlowUp, float speed)
    {
        this.secondsToBlowUp = secondsToBlowUp;
        colliderOfAOE.radius = radius;
        spriteOfAOE.gameObject.transform.localScale = new Vector3(radius * 2, radius * 2, 1);
        rbVelocity = transform.right * speed;
    }
    public void OnTriggerStay2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Wall")
        {
            // We assume that wall is a 1x1 square
            const float radiusOfWall = 0.5f;
            Vector3 wallPosition = collider.gameObject.transform.position;
            if (transform.position.x >= wallPosition.x - radiusOfWall &&
            transform.position.y >= wallPosition.y - radiusOfWall &&
            transform.position.x <= wallPosition.x + radiusOfWall &&
            transform.position.y <= wallPosition.y + radiusOfWall)
            {
                // We touched the wall, we blow up
                blowUp();
            }
        }
    }
    public void OnTriggerExit2D(Collider2D collider)
    {
        if (!colliderOfAOE.enabled)
            return;
        GameObject colliderGO = collider.gameObject;
        if (objectsToBlowUp.Contains(colliderGO))
        {
            objectsToBlowUp.Remove(colliderGO);
        }
    }
    void Start()
    {
        AreaOfEffectAppears();
    }
    private float secondsOfExplosion = 0.1f;

    float secondsToBlowUp = 0;

    [SerializeField]
    private int damage = 10;
    [SerializeField]
    private CircleCollider2D colliderOfAOE;

    [SerializeField]
    private SpriteRenderer spriteOfAOE;

    private void AreaOfEffectAppears()
    {
        spriteOfAOE.enabled = true;
    }

    private void AreaOfEffectGoesOff()
    {
        // Disabling collider makes both trigger functions above know about the fact, that no more changes to list is allowed.
        colliderOfAOE.enabled = false;

        foreach (var obj in objectsToBlowUp)
        {
            if (obj != null)
            {
                if (obj.tag == "Wall")
                {
                    // Destroy wall
                    Destroy(obj);
                }
                else
                {
                    Character character = obj.GetComponent<Character>();
                    if (character != null)
                    {
                        // Damage characters
                        character.TakeDamage(damage);
                    }
                }
            }
        }
    }
    private void ExplosionAnimationStarts()
    {
        //TODO explosion animation
    }


    public void blowUp()
    {
        AreaOfEffectGoesOff();
        Destroy(gameObject);
    }

    void Update()
    {
        rb.velocity=rbVelocity;
    }
}
