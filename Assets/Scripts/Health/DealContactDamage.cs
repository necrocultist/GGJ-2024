using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
public class DealContactDamage : MonoBehaviour
{
    [SerializeField] private int contactDamageAmount;
    [SerializeField] private LayerMask layerMask;
    private bool isColliding = false;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (isColliding) return;

        ContactDamage(collision.collider);
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (isColliding) return;

        ContactDamage(collision.collider);
    }

    private void ContactDamage(Collider2D collision)
    {
        int collisionObjectLayerMask = (1 << collision.gameObject.layer);

        if ((layerMask.value & collisionObjectLayerMask) == 0)
            return;

        ReceiveContactDamage receiveContactDamage = collision.gameObject.GetComponent<ReceiveContactDamage>();

        if (receiveContactDamage != null)
        {
            isColliding = true;
            
            Invoke(nameof(ResetContactCollision), 0.5f);

            receiveContactDamage.TakeContactDamage(contactDamageAmount);
        }
    }
    
    private void ResetContactCollision()
    {
        isColliding = false;
    }
}
