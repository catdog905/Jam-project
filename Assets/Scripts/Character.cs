using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{

    private Rigidbody2D rb;
    [SerializeField]
    private GameObject bombPrefab;
    public GameObject wallFather;


    [SerializeField]
    private float speed = 10f, anglularSpeed = 10f, bombPlacingCooldown = 6, expolsionRadius = 5,cooldownBetweenWallCreation=0.5f,secondsToBlowUp = 3f, bombSpeed = 10f;
    [SerializeField]
    private GameObject wallPrefab,wallCreatorPrefab;
    [SerializeField]
    LayerMask layersToStopExplosion;
    [SerializeField]
    private int hp = 1,bombDamage=10,spawnedWallDistance=2;

    private bool isBombOnCooldown = false,isWallOnCooldown = false;

    Vector2 movementDirection = Vector2.zero;
    Vector2 placeToLookAt = Vector2.zero;

    void Start()
    {

    }

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void TakeDamage(int damage)
    {
        hp -= damage;
        if (hp <= 0)
        {
            Destroy(gameObject);
        }
    }

    IEnumerator ManageBombCooldown()
    {
        isBombOnCooldown = true;
        yield return new WaitForSeconds(bombPlacingCooldown);
        isBombOnCooldown = false;
    }
    IEnumerator ManageWallCooldown()
    {
        isWallOnCooldown = true;
        yield return new WaitForSeconds(cooldownBetweenWallCreation);
        isWallOnCooldown = false;
    }

    public void PlaceBomb()
    {
        if (isBombOnCooldown)
            return;
        StartCoroutine(ManageBombCooldown());
        Instantiate(bombPrefab, transform.position, transform.rotation).GetComponent<Bomb>().Initialize(expolsionRadius,secondsToBlowUp, bombSpeed,layersToStopExplosion,bombDamage);
    }

    public void SetMovement(Vector2 dir)
    {
        movementDirection = dir;
    }

    public void SetPlaceToLookAt(Vector2 PTLA)
    {
        placeToLookAt = PTLA;
    }
    Vector2[] primaryDirection = new Vector2[]{
        Vector2.right, // right
        Vector2.right+Vector2.up, // right-up
        Vector2.up, // up
        Vector2.left+Vector2.up, // left-up
        Vector2.left, // left
        Vector2.left+Vector2.down, // left-down
        Vector2.down, // down
        Vector2.right+Vector2.down, // right-down
        Vector2.right // right
    };
    Vector2[] secondDirection = new Vector2[]{
        Vector2.up, // right
        Vector2.left, // right-up
        Vector2.left, // up
        Vector2.down, // left-up
        Vector2.down, // left
        Vector2.right, // left-down
        Vector2.right, // down
        Vector2.up, // right-down
        Vector2.up // right
    };
    Vector2[] thirdDirection = new Vector2[]{
        Vector2.down, // right
        Vector2.down, // right-up
        Vector2.right, // up
        Vector2.right, // left-up
        Vector2.up, // left
        Vector2.up, // left-down
        Vector2.left, // down
        Vector2.left, // right-down
        Vector2.down // right
    };

    private bool WallIsPlacable(Vector3 pos){
        Vector2 change = pos - transform.position;
        RaycastHit2D hit = Physics2D.Raycast(transform.position, change, change.magnitude, layersToStopExplosion);
        return hit.collider == null;
            
    }

    public void PlaceWall(){
        if (isWallOnCooldown)
            return;
        StartCoroutine(ManageWallCooldown());
        int rotSerialized = Mathf.RoundToInt(transform.rotation.eulerAngles.z/45);
        Vector2 dir = primaryDirection[rotSerialized];
        Vector3 opos1 = secondDirection[rotSerialized],opos2=thirdDirection[rotSerialized];
        Vector3 coords = new Vector3(transform.position.x,transform.position.y,transform.position.z+99);
        coords += (Vector3)(transform.right*spawnedWallDistance);
        coords.x = Mathf.Round(coords.x);
        coords.y = Mathf.Round(coords.y);
        List<Vector3> coordsToBuildWallsAt = new List<Vector3>();
        if (WallIsPlacable(coords)){
            coordsToBuildWallsAt.Add(coords);
        }
        if (WallIsPlacable(coords+opos1)){
            coordsToBuildWallsAt.Add(coords+opos1);
        }
        if (WallIsPlacable(coords+opos2)){
            coordsToBuildWallsAt.Add(coords+opos2);
        }
        foreach(var vec in coordsToBuildWallsAt){
            StartCoroutine(spawnWall(vec));
        }
    }
    IEnumerator spawnWall(Vector3 vector3){
        float t = 0;
        const float timeToFinish = 0.15f;
        Vector3 start = transform.position;
        GameObject wallCreator = Instantiate(wallCreatorPrefab);
        while (true){
            t+=Time.deltaTime;
            if (t >= timeToFinish){
                Destroy(wallCreator);
                break;
            }
            wallCreator.transform.position = Vector3.Lerp(start,vector3,t/timeToFinish);
            yield return null;
        }
        Instantiate(wallPrefab,vector3,Quaternion.identity,wallFather.transform).transform.position+=new Vector3(0.001f,0,0);
       
    }

    void Update()
    {
        // Transforming Vector2 into Vector3
        Vector3 worldPosition = placeToLookAt;
        worldPosition.z = transform.position.z;

        Quaternion dir = transform.rotation;
        Vector3 change = worldPosition - transform.position;

        // Angle math
        int dop = 0;
        if (change.y > 0)
            dop = 180;
        Quaternion finish = Quaternion.Euler(0, 0, dop - Mathf.Atan(change.x / change.y) * Mathf.Rad2Deg - 90);

        // Slow rotation
        dir = Quaternion.Lerp(dir, finish, Time.deltaTime * anglularSpeed);
        transform.rotation = dir;

        // Movement
        rb.velocity = movementDirection * speed;
    }
}
