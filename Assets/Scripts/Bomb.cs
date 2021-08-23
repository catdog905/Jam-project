using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
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
        StartCoroutine(blowUp());
    }
    [SerializeField]
    private float secondsToBlowUp = 3f, secondsOfExplosion = 0.1f;

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



    IEnumerator blowUp()
    {
        AreaOfEffectAppears();
        yield return new WaitForSeconds(secondsToBlowUp);

        ExplosionAnimationStarts();
        yield return new WaitForSeconds(secondsOfExplosion);

        AreaOfEffectGoesOff();
        Destroy(gameObject);
    }

    void Update()
    {

    }
}
