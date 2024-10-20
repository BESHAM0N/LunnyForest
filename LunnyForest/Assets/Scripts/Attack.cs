using UnityEngine;

public class Attack : MonoBehaviour
{
    [SerializeField] private int _attackDamage = 15;
    [SerializeField] private Vector2 _knockback = Vector2.zero;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Damaged damaged = collision.GetComponent<Damaged>();
        if (damaged != null)
        {
            Vector2 deliveredKnockback = transform.parent.localScale.x > 0
                ? _knockback
                : new Vector2(-_knockback.x, _knockback.y);
            damaged.TakeDamage(_attackDamage, deliveredKnockback);
            
        }
    }
}