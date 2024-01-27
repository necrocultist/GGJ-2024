using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerDetails_", menuName = "Scriptable Objects/Player/Player Details")]
public class PlayerDetailsSO : ScriptableObject
{
    public string playerCharacterName = "Player";
    public float minMoveSpeed = 8f;
    public float maxMoveSpeed = 8f;
    public GameObject playerPrefab;
    
    public int playerDamageAmount;
    public float playerAttackRange;
    public LayerMask enemyLayer;
    
    public int playerHealthAmount;
    public bool isImmuneAfterHit = false;
    public float hitImmunityTime = 0.5f;
    
    public static float animationBaseSpeed = 8f;
    
    public float GetMoveSpeed()
    {
        return minMoveSpeed == maxMoveSpeed ? minMoveSpeed : Random.Range(minMoveSpeed, maxMoveSpeed);
    }
}