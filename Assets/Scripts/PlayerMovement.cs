using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;            
    public GameObject trailPrefab;          
    public float trailSpawnDistance = 0.9f;   

    private Vector2 keys;               
    private Vector3 lastTrailPosition;      
    private GameObject lastTrail;           

    void Start()
    {
        // Initialize last trail position with the player's starting position
        lastTrailPosition = transform.position;
    }

    void Update()
    {
        
        keys.x = Input.GetAxisRaw("Horizontal");
        keys.y = Input.GetAxisRaw("Vertical");

        // Spawn trail if the player has moved enough distance from the last trail
        if (Vector3.Distance(lastTrailPosition, transform.position) > trailSpawnDistance)
        {
            SpawnTrail();
        }
    }

    void FixedUpdate()
    {
        // Calculate new position 
        Vector3 newPosition = transform.position + (Vector3)keys * moveSpeed * Time.fixedDeltaTime;

        // collision check
        if (!IsCollidingWithTrail(newPosition))
        {
            // Move the player to the new position if there's no collision
            transform.position = newPosition;
        }
    }

    void SpawnTrail()
    {
        // Instantiate a trail prefab 
        lastTrail = Instantiate(trailPrefab, transform.position, Quaternion.identity);

        // Update the last trail 
        lastTrailPosition = transform.position;
    }

    private bool IsCollidingWithTrail(Vector3 newPosition)
    {
        // collision check
        Collider2D[] colliders = Physics2D.OverlapCircleAll(newPosition, 0.1f); 

        foreach (var collider in colliders)
        {
            if (collider.CompareTag("Trail") && collider.gameObject != lastTrail)
            {
                
                return true;
            }
        }

        
        return false;
    }
}



