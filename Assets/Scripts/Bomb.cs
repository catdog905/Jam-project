using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    // Start is called before the first frame update
    
    public void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log(collision.collider.name);
    }
    List<GameObject> objectsToBlowUp = new List<GameObject>();
    public void OnTriggerEnter2D(Collider2D collider)
    {
        if (!colliderOfAOE.enabled)
            return;
        GameObject colliderGO = collider.gameObject;
        if ( !objectsToBlowUp.Contains(colliderGO)){
            objectsToBlowUp.Add(colliderGO);
        }
    }
    public void OnTriggerExit2D(Collider2D collider)
    {
        if (!colliderOfAOE.enabled)
            return;
        GameObject colliderGO = collider.gameObject;
        if (objectsToBlowUp.Contains(colliderGO)){
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

    private void AreaOfEffectAppears(){
        spriteOfAOE.enabled=true;
    }

    private void AreaOfEffectGoesOff(){
        colliderOfAOE.enabled=false;
        foreach(var obj in objectsToBlowUp){
            if (obj != null){
                if (obj.tag == "Wall"){
                    // Destroy wall
                    Destroy(obj);
                }else{
                    Character character = obj.GetComponent<Character>();
                    if (character != null){
                        // Damage characters
                        character.TakeDamage(damage);
                    }
                }
            }
        }
    }
    private void ExplosionAnimationStarts(){

    }



    IEnumerator blowUp(){
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
