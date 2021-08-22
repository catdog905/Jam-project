using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    private Character character;
    void Awake(){
        character = GetComponent<Character>();
    }
    // Start is called before the first frame update

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        character.SetMovement(new Vector2(Input.GetAxis("Horizontal"),Input.GetAxis("Vertical")));
        
        Vector3 worldPosition;
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = Camera.main.nearClipPlane;
        worldPosition = Camera.main.ScreenToWorldPoint(mousePos);
        character.SetPlaceToLookAt(worldPosition);
        
    }
}
