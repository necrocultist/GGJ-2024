using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerDetails_", menuName = "Scriptable Objects/Player/Player Details")]
public class PlayerDetailsSO : ScriptableObject
{
    public string playerCharacterName;
    public float playerSpeed;
    public GameObject playerPrefab;
    public int playerHealthAmount;
    public int playerDamageAmount;
    public bool isImmuneAfterHit = false;
    public float hitImmunityTime = 0.5f;
}
