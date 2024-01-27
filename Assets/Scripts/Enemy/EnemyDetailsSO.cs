using UnityEngine;

[CreateAssetMenu(fileName = "EnemyDetails_", menuName = "Scriptable Objects/Enemy/EnemyDetails")]
public class EnemyDetailsSO : ScriptableObject
{
    public string enemyCharacterName = "Enemy";
    public GameObject enemyPrefab;
    public int enemyHealthAmount = 10;
    
    public int enemyDamageAmount = 10;
    public float chaseDistance = 5f;
    public float patrolSpeed = 2f;
    public float chaseSpeed = 5f;
    public float patrolTime = 2f;
    
    public bool isImmuneAfterHit = false;
    public float hitImmunityTime = 0.5f;
    public bool isHealthBarDisplayed = false;
}
