using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProgectileAir : MonoBehaviour
{
    [SerializeField] private int _damage = 15;
    [SerializeField] private Vector2 _moveSpeed = new Vector2(4, 0);
    [SerializeField] private Vector2 _knockback = new Vector2(0, 0);
    private Rigidbody2D _rigidbody2D;

    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        _rigidbody2D.velocity = new Vector2(_moveSpeed.x * transform.localScale.x, _moveSpeed.y * transform.localScale.y);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Damaged damaged = collision.GetComponent<Damaged>();
        if (damaged != null)
        {
            Vector2 deliveredKnockback = transform.localScale.x > 0
                ? _knockback
                : new Vector2(-_knockback.x, _knockback.y);

            bool gotHit = damaged.TakeDamage(_damage, deliveredKnockback);
            if (gotHit)
                Destroy(gameObject);
        }
    }
}
