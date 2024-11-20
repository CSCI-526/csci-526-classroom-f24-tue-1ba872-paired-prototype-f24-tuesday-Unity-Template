using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

/// Simple coordinate type conversion
public static class V3IntExpand
{
    public static Vector3Int ToInt(this Vector3 vector3)
    {
        return new Vector3Int((int)vector3.x, (int)vector3.y, (int)vector3.z);
    }
}

/// AI control for enemies
public class EnemyAI : MonoBehaviour
{

    private NavMeshAgent nav;

    public float MaxSpeed = 3;
    public float MinSpeed = 2;

    public bool IsStar;
    public bool IsSeedPlayer;

    public float DetectionRange = 10f;  // Detection range
    public float FieldOfView = 60f;     // Field of view angle

    public Color MaxColor = Color.red;

    public PlayerMove PlayerTaget;

    public Transform StarTarget; // Target point for stars (new addition)

    // Record all stars that have already been locked by enemies (static global)
    private static HashSet<Transform> lockedStars = new HashSet<Transform>();

    public Vector3Int CurrentPoint => transform.position.ToInt();
    public Vector3Int MovePoint;
    public Vector3Int LastPoint;

    void Start()
    {
        MovePoint = CurrentPoint;
        nav = GetComponent<NavMeshAgent>();
        GameObject star = GameObject.FindWithTag("Star");
        if (star != null)
        {
            StarTarget = star.transform;
        }
    }

    // Movement logic
    void FixedUpdate()
    {
        Debug.Log("Seeplayer:" + IsSeedPlayer);
        nav.speed = IsStar ? MaxSpeed : MinSpeed;

        if (Vector3.Distance(transform.position, PlayerTaget.transform.position) < DetectionRange)
        {
            IsSeedPlayer = true;

            if (!IsStar)
            {

                MovePoint = MoveNextPoint(transform.position.ToInt()).OrderByDescending(v => Vector3.Distance(v, PlayerTaget.transform.position)).First();

            }

        }
        else
        {
            IsSeedPlayer = false;
        }
        // Detected
        if (IsSeedPlayer)
        {
            if (IsStar)
            {
                nav.SetDestination(PlayerTaget.transform.position);
            }
            else
            {
                nav.SetDestination(MovePoint + Vector3.up * 0.5f);
            }
        }
        else
        {
            if (Vector3.Distance(transform.position, MovePoint + Vector3.up * 0.5f) < 0.1f)
            {

                MovePoint = MoveNextPoint(MovePoint).GetRandom();

            }

            nav.SetDestination(MovePoint + Vector3.up * 0.5f);
        }
    }

    private void UpdateNearestStar()
    {
        GameObject[] stars = GameObject.FindGameObjectsWithTag("Star");
        if (stars.Length > 0)
        {
            // Find the nearest star that is not locked and update as the current target
            StarTarget = stars
                .Select(star => star.transform)
                .Where(star => !lockedStars.Contains(star))  // Filter out stars that are already locked
                .OrderBy(star => Vector3.Distance(transform.position, star.position))
                .FirstOrDefault();
        }
    }

    // Check if the player is within the sight range
    private bool IsPlayerInSight()
    {
        // Calculate the x-axis offset between the player and the enemy
        float xOffset = Mathf.Abs(PlayerTaget.transform.position.x - transform.position.x);

        // Calculate the z-axis offset between the player and the enemy
        float zOffset = Mathf.Abs(PlayerTaget.transform.position.z - transform.position.z);

        // Only consider the player in sight when the x-axis offset is greater than the z-axis offset (i.e., the enemy can only detect players in the x direction)
        if (xOffset > zOffset)
        {
            Vector3 directionToPlayer = new Vector3(PlayerTaget.transform.position.x - transform.position.x, 0, PlayerTaget.transform.position.z - transform.position.z).normalized;

            // Calculate the horizontal angle between the enemy's front and the player's direction
            float horizontalAngle = Vector3.Angle(transform.forward, directionToPlayer);

            if (horizontalAngle < FieldOfView / 2f)
            {
                RaycastHit hit;
                // Cast a horizontal ray from the enemy's position to check for sight obstacles (ignoring the y-axis)
                if (Physics.Raycast(transform.position + Vector3.up * 0.5f, directionToPlayer, out hit, DetectionRange))
                {
                    if (hit.collider.CompareTag("Player"))
                    {
                        return true;
                    }
                }
            }
        }

        return false;
    }

    public List<Vector3Int> MoveNextPoint(Vector3Int point)
    {
        return GameSys.Ins.RoadPointList.Where(v => Vector3.Distance(v, point) < 1.1f).ToList();
    }

    // Trigger event handler
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Star")
        {
            IsStar = true;
            this.GetComponentInChildren<Renderer>().material.color = Color.red;
            PlayerTaget.GetComponent<Renderer>().material.color = Color.green;
            PlayerTaget.speed = PlayerTaget.MinSpeed;
            other.SetActiveF();
        }
        if (other.tag == "Trap")
        {
            this.SetActiveF();
            //PlayerTaget.GetComponent<Renderer>().material.color = Color.red;
            //PlayerTaget.speed = PlayerTaget.MaxSpeed;
        }
    }
}
