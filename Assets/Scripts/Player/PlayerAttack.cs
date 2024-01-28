using UnityEngine;

[DisallowMultipleComponent]
public class PlayerAttack : MonoBehaviour
{
    private AttackEvent playerAttackEvent;
    private Player player;
    private int playerDamage;
    private float attackRange;
    private LayerMask enemyLayer;
    private Camera _camera;

    private void Start()
    {
        _camera = Camera.main;
    }

    private void Awake()
    {
        player = GetComponent<Player>();
        playerAttackEvent = GetComponent<AttackEvent>();
        attackRange = player.playerDetails.playerAttackRange;
        enemyLayer = player.playerDetails.enemyLayer;
        playerDamage = player.playerDetails.playerDamageAmount;
    }

    private void OnEnable()
    {
        playerAttackEvent.OnAttack += AttackEvent_OnAttack;
    }

    private void OnDisable()
    {
        playerAttackEvent.OnAttack -= AttackEvent_OnAttack;
    }
    
    private void AttackEvent_OnAttack(AttackEvent attackEvent, AttackEventArgs attackEventArgs)
    {
        Attack();
    }

    private void Attack()
    {
        Vector2 attackDirection = Vector2.zero;

        // Get the mouse position in world coordinates
        if (_camera != null)
        {
            Vector2 mousePosition = _camera.ScreenToWorldPoint(Input.mousePosition);

            Vector2 directionToMouse = (mousePosition - (Vector2)transform.position).normalized;

            // Determine the primary axis of the attack based on the direction
            float horizontalMagnitude = Mathf.Abs(directionToMouse.x);
            float verticalMagnitude = Mathf.Abs(directionToMouse.y);

            if (horizontalMagnitude > verticalMagnitude)
            {
                attackDirection = new Vector2(Mathf.Sign(directionToMouse.x), 0f);
            }
            else
            {
                attackDirection = new Vector2(0f, Mathf.Sign(directionToMouse.y));
            }
        }
        
        // Calculate the attack position based on the attack direction
        Vector2 attackPosition = (Vector2)transform.position + attackDirection * attackRange;
        
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPosition, attackRange, enemyLayer);
        foreach (Collider2D enemy in hitEnemies)
        {
            if (enemy.GetComponent<Health>())
            {
                enemy.GetComponent<Health>().TakeDamage(playerDamage);
                // if (enemy.GetComponent<EnemyManager>().enemyAlive) knockbackManager.Knock(transform, enemy.transform, 0.01f, 55f);
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, attackRange);
        
    }
}
