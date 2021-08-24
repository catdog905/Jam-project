using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    private Character character;
    void Awake()
    {
        character = GetComponent<Character>();
    }

    void Start()
    {

    }

    void Update()
    {
        // Movement
        character.SetMovement(new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")));

        // Look direction
        Vector3 worldPosition;
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = Camera.main.nearClipPlane;
        worldPosition = Camera.main.ScreenToWorldPoint(mousePos);
        character.SetPlaceToLookAt(worldPosition);

        // Bomb
        if (Input.GetKey(KeyCode.Space))
        {
            character.PlaceBomb();
        }

    }
}
