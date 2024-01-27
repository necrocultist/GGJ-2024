using UnityEngine;

[CreateAssetMenu(fileName = "EnemyDetails_", menuName = "Scriptable Objects/Enemy/EnemyDetails")]
public class EnemyDetailsSO : ScriptableObject
{
    public string enemyCharacterName = "Enemy";
    public GameObject enemyPrefab;
    public float minMoveSpeed = 2f;
    public float maxMoveSpeed = 4f;
    public int enemyHealthAmount = 10;
    public int enemyDamageAmount = 10;
    public float chaseDistance = 15f;
    public bool isImmuneAfterHit = false;
    public float hitImmunityTime = 0.5f;
    public bool isHealthBarDisplayed = false;
    
    public float GetMoveSpeed()
    {
        return minMoveSpeed == maxMoveSpeed ? minMoveSpeed : Random.Range(minMoveSpeed, maxMoveSpeed);
    }
}
