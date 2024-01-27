using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(Animator))]
// [RequireComponent(typeof(EnemyMovementAI))]
[RequireComponent(typeof(Health))]
[RequireComponent(typeof(HealthEvent))]
[RequireComponent(typeof(DestroyedEvent))]
[RequireComponent(typeof(Destroyed))]
[RequireComponent(typeof(Idle))]
[RequireComponent(typeof(IdleEvent))]
[RequireComponent(typeof(DealContactDamage))]
[RequireComponent(typeof(ReceiveContactDamage))]
[DisallowMultipleComponent]
public class Enemy : MonoBehaviour
{
    [SerializeField] private int enemyDifficulty = 1;
    public EnemyDetailsSO enemyDetails;
    private Health health;
    private HealthEvent healthEvent;
    [HideInInspector] public IdleEvent idleEvent;
    private DealContactDamage dealContactDamage;
    private ReceiveContactDamage receiveContactDamage;
    private Destroyed destroyed;
    private DestroyedEvent destroyedEvent;
    private Rigidbody2D rb;
    [HideInInspector] public SpriteRenderer spriteRenderer;
    [HideInInspector] public Animator animator;
    // private EnemyMovementAI enemyMovementAI;

    private void Awake()
    {
        healthEvent = GetComponent<HealthEvent>();
        health = GetComponent<Health>();
        idleEvent = GetComponent<IdleEvent>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        dealContactDamage = GetComponent<DealContactDamage>();
        receiveContactDamage = GetComponent<ReceiveContactDamage>();
        destroyed = GetComponent<Destroyed>();
        // enemyMovementAI = GetComponent<EnemyMovementAI>();
        
        Initialize(enemyDetails, enemyDifficulty);
    }

    private void OnEnable()
    {
        healthEvent.OnHealthChanged += HealthEvent_OnHealthLost;
    }
    
    private void OnDisable()
    {
        healthEvent.OnHealthChanged -= HealthEvent_OnHealthLost;
    }
    
    private void HealthEvent_OnHealthLost(HealthEvent healthEvent, HealthEventArgs healthEventArgs)
    {
        if (healthEventArgs.healthAmount <= 0)
        {
            EnemyDestroyed();
        }
    }
    
    private void EnemyDestroyed()
    {
        DestroyedEvent destroyedEvent = GetComponent<DestroyedEvent>();
        destroyedEvent.CallDestroyedEvent(false, health.GetStartingHealth());
    }
    
    public void Initialize(EnemyDetailsSO enemyDetails, int difficulty)
    {
        this.enemyDetails = enemyDetails;
        
        SetEnemyStartingHealth(difficulty);
    }
    
    private void SetEnemyStartingHealth(int difficulty)
    {
        health.SetStartingHealth(enemyDetails.enemyHealthAmount * difficulty);
    }
}
