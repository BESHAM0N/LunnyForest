using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(TouchingDirections), typeof(Damaged))]
public class Mushroom : MonoBehaviour
{
    public bool LockVelocity
    {
        get { return _animator.GetBool(AnimationStrings.lockVelocity); }
        set { _animator.SetBool(AnimationStrings.lockVelocity, value); }
    }

    public bool HasTarget
    {
        get { return _hasTarget; }
        private set
        {
            _hasTarget = value;
            _animator.SetBool(AnimationStrings.hasTarget, value);
        }
    }

    public bool CanMove => _animator.GetBool(AnimationStrings.canMove);

    [SerializeField] private float _walkSpeed = 3f;
    [SerializeField] private DetectionArea _detectionArea;
    [SerializeField] private float _walkStopRate = 0.05f;
    [SerializeField] private DetectionArea _cliffDetection;
    private Rigidbody2D _rigidbody2D;
    private TouchingDirections _touchingDirections;
    private WalkableDirection _walkDirection;
    private Animator _animator;
    private Vector2 _walkDirectionVector = Vector2.right;
    private bool _hasTarget;
    private Damaged _damaged;

    private float _attackCooldown
    {
        get { return _animator.GetFloat(AnimationStrings.attackCooldown); }
        set { _animator.SetFloat(AnimationStrings.attackCooldown, Mathf.Max(value, 0)); }
    }

    public WalkableDirection WalkDirection
    {
        get { return _walkDirection; }
        set
        {
            if (_walkDirection != value)
            {
                gameObject.transform.localScale = new Vector2(gameObject.transform.localScale.x * -1,
                    gameObject.transform.localScale.y);
                if (value == WalkableDirection.Right)
                {
                    _walkDirectionVector = Vector2.right;
                }
                else if (value == WalkableDirection.Left)
                {
                    _walkDirectionVector = Vector2.left;
                }
            }

            _walkDirection = value;
        }
    }

    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _touchingDirections = GetComponent<TouchingDirections>();
        _animator = GetComponent<Animator>();
        _damaged = GetComponent<Damaged>();
    }

    private void Update()
    {
        HasTarget = _detectionArea.detectedColliders.Count > 0;
        if (_attackCooldown > 0)
        {
            _attackCooldown -= Time.deltaTime;
        }
    }

    private void FixedUpdate()
    {
        if (_touchingDirections.IsGrounded && _touchingDirections.IsOnWall)
        {
            FlipDirection();
        }

        if (!_damaged.LockVelocity)
        {
            _rigidbody2D.velocity = CanMove && _touchingDirections.IsGrounded
                ? new Vector2(_walkSpeed * _walkDirectionVector.x, _rigidbody2D.velocity.y)
                : new Vector2(Mathf.Lerp(_rigidbody2D.velocity.x, 0, _walkStopRate), _rigidbody2D.velocity.y);
        }
    }

    private void FlipDirection()
    {
        if (_walkDirection == WalkableDirection.Right)
        {
            WalkDirection = WalkableDirection.Left;
        }
        else if (WalkDirection == WalkableDirection.Left)
        {
            WalkDirection = WalkableDirection.Right;
        }
        else
        {
            Debug.Log("Нет значения для движения в стороны");
        }
    }

    public void OnCliffDetected()
    {
        if (_touchingDirections.IsGrounded)
        {
            FlipDirection();
        }
    }

    public enum WalkableDirection
    {
        Right,
        Left
    }
}