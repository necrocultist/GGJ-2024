using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[DisallowMultipleComponent]
public class EnemyMovement : MonoBehaviour
{
    private Transform playerLocation;
    private Enemy enemy;
    private EnemyDetailsSO enemyDetails;
    private Rigidbody2D rb;
    private Vector3 initialPosition;
    private bool isPatrolling = true;
    private float patrolTimer = 0f;

    private void Start()
    {
        playerLocation = GameObject.FindGameObjectWithTag("Player").transform;
        enemy = GetComponent<Enemy>();
        enemyDetails = enemy.enemyDetails;
        rb = enemy.rb;
        initialPosition = transform.position;
    }

    private void Update()
    {
        Debug.Log(isPatrolling);
        MoveEnemy();
    }

    private void MoveEnemy()
    {
        if (IsPlayerInRange())
        {
            isPatrolling = false;
            ChasePlayer();
        }
        else
        {
            if (!isPatrolling)
            {
                Debug.Log("Not patrolling");
                ReturnToInitialPosition();
                if (Vector3.Distance(transform.position, initialPosition) < 0.1f)
                {
                    isPatrolling = true;
                }
            }
            else
            {
                Patrol();
            }
        }
    }

    private bool IsPlayerInRange()
    {
        Debug.Log("Checking if player is in range");
        return Vector3.Distance(transform.position, playerLocation.position) < enemyDetails.chaseDistance;
    }

    private void Patrol()
    {
        Debug.Log("Patrolling");
        rb.velocity = new Vector2(enemyDetails.patrolSpeed, enemyDetails.patrolSpeed);

        patrolTimer += Time.deltaTime;
        if (patrolTimer > enemyDetails.patrolTime)
        {
            rb.velocity = new Vector2(-enemyDetails.patrolSpeed, 0f);
            patrolTimer = 0f;
        }
    }

    private void ReturnToInitialPosition()
    {
        Debug.Log("Returning to initial position");
        float direction = (initialPosition.x - transform.position.x > 0) ? 1f : -1f;
        rb.velocity = new Vector2(enemyDetails.patrolSpeed * direction, 0f);

        // Check if the enemy has moved past the initial position
        if ((direction > 0 && transform.position.x > initialPosition.x) ||
            (direction < 0 && transform.position.x < initialPosition.x))
        {
            isPatrolling = true;
        }
    }

    private void ChasePlayer()
    {
        Debug.Log("Chasing player");
        Vector3 direction = (playerLocation.position - transform.position).normalized;
        rb.velocity = new Vector2(enemyDetails.chaseSpeed * direction.x, enemyDetails.chaseSpeed * direction.y);
    }
}