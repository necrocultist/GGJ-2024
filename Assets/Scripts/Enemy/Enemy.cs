using DunGen;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(EnemyMovement))]
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
    public EnemyDetailsSO enemyDetails;
    [SerializeField] private int enemyDifficulty = 1;
    [HideInInspector] public Health health;
    [HideInInspector] public HealthEvent healthEvent;
    [HideInInspector] public IdleEvent idleEvent;
    [HideInInspector] public DealContactDamage dealContactDamage;
    [HideInInspector]public ReceiveContactDamage receiveContactDamage;
    [HideInInspector] public Destroyed destroyed;
    [HideInInspector]public DestroyedEvent destroyedEvent;
    [HideInInspector] public Rigidbody2D rb;
    [HideInInspector] public SpriteRenderer spriteRenderer;
    [HideInInspector] public Animator animator;
    [HideInInspector] public EnemyMovement enemyMovement;


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
        enemyMovement = GetComponent<EnemyMovement>();
        
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
        RoomControl.instance.StartCoroutine(RoomControl.instance.RoomCoroutine());
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
