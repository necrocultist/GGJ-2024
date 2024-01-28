using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(IdleEvent))]
[DisallowMultipleComponent]
public class EnemyMovement : MonoBehaviour
{
    private Transform playerLocation;
    private Enemy enemy;
    private EnemyDetailsSO enemyDetails;
    private Rigidbody2D rb;
    private Vector3 initialPosition;
    private bool isPatrolling = false;
    private float patrolTimer = 0f;
    private float randomPatrolDuration;
    private Vector2 randomPatrolDirection;
    public bool notInRoom = true;
    private void Start()
    {
        playerLocation = GameObject.FindGameObjectWithTag("Player").transform;
        enemy = GetComponent<Enemy>();
        enemyDetails = enemy.enemyDetails;
        rb = enemy.rb;
        initialPosition = transform.position;

        SetRandomPatrolValues();
    }

    private void Update()
    {
        if (!notInRoom)
        {
            MoveEnemy();
        }
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
                ReturnToInitialPosition();
                if (Vector3.Distance(transform.position, initialPosition) < 0.01f)
                {
                    // Set new random patrol values when returning to initial position
                    SetRandomPatrolValues();
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
        return Vector3.Distance(transform.position, playerLocation.position) < enemyDetails.chaseDistance;
    }

    private void Patrol()
    {
        Debug.Log("Patrolling");
        rb.velocity = randomPatrolDirection * enemyDetails.patrolSpeed;

        patrolTimer += Time.deltaTime;
        if (patrolTimer > enemyDetails.patrolTime)
        {
            // Set new random patrol values after the current patrol duration
            SetRandomPatrolValues();
        }
    }

    private void ReturnToInitialPosition()
    {
        Debug.Log("Returning to initial position");

        Vector2 direction = ((Vector2)initialPosition - (Vector2)transform.position).normalized;
        rb.velocity = direction * enemyDetails.patrolSpeed;

        if (Vector2.Distance(transform.position, initialPosition) < 0.1f)
        {
            // Set new random patrol values when returning to initial position
            SetRandomPatrolValues();
            isPatrolling = true;
        }
    }

    private void ChasePlayer()
    {
        Debug.Log("Chasing player");
        Vector3 direction = (playerLocation.position - transform.position).normalized;
        rb.velocity = new Vector2(enemyDetails.chaseSpeed * direction.x, enemyDetails.chaseSpeed * direction.y);
    }

    private void SetRandomPatrolValues()
    {
        randomPatrolDirection = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;
    }
}
