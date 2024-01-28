using System;

using UnityEngine;

public class AttackEvent : MonoBehaviour
{
    public event Action<AttackEvent, AttackEventArgs> OnAttack;

    public void CallAttackEvent(int damageAmount)
    {
        OnAttack?.Invoke(this, new AttackEventArgs() { damageAmount = damageAmount });
    }
}

public class AttackEventArgs : EventArgs
{
    public int damageAmount;
}
