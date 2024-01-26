using UnityEngine;

[CreateAssetMenu(fileName = "EnemyDetails_", menuName = "Scriptable Objects/Enemy/EnemyDetails")]
public class EnemyDetailsSO : ScriptableObject
{
    public GameObject enemyPrefab;
    public float chaseDistance = 50f;
    public bool isImmuneAfterHit = false;
    public float hitImmunityTime = 0.5f;
    public bool isHealthBarDisplayed = false;
}
