using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingEye : MonoBehaviour
{
    [SerializeField] private DetectionArea _detectionArea;
    [SerializeField] private List<Transform> _waypoints;
    [SerializeField] private Collider2D _deathCollider;
    [SerializeField] private float _flightSpeed = 2f;
    [SerializeField] private float waypointReachedDistance = 0.1f;
    private Damaged _damaged;
    private Rigidbody2D _rigidbody2D;
    private Animator _animator;
    private bool _hasTarget;
    private Transform _nextWaypoint;
    private int _waypointNum;

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


    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _damaged = GetComponent<Damaged>();
    }

    private void Start()
    {
        _nextWaypoint = _waypoints[_waypointNum];
    }

    private void Update()
    {
        HasTarget = _detectionArea.detectedColliders.Count > 0;
    }

    private void FixedUpdate()
    {
        if (_damaged.IsAlive)
        {
            if (CanMove)
            {
                Flight();
            }
            else
            {
                _rigidbody2D.velocity = Vector2.zero;
            }
        }
    }

    private void Flight()
    {
        Vector2 directionToWaypoint = (_nextWaypoint.position - transform.position).normalized;
        float distance = Vector2.Distance(_nextWaypoint.position, transform.position);
        _rigidbody2D.velocity = directionToWaypoint * _flightSpeed;
        UpdateDirection();


        if (distance <= waypointReachedDistance)
        {
            _waypointNum++;
            if (_waypointNum >= _waypoints.Count)
            {
                _waypointNum = 0;
            }

            _nextWaypoint = _waypoints[_waypointNum];
        }
    }

    private void UpdateDirection()
    {
        Vector3 locScale = transform.localScale;
        if (transform.localScale.x > 0)
        {
            if (_rigidbody2D.velocity.x < 0)
            {
                transform.localScale = new Vector3(-1 * locScale.x, locScale.y, locScale.z);
            }
        }
        else
        {
            if (_rigidbody2D.velocity.x > 0)
            {
                transform.localScale = new Vector3(-1 * locScale.x, locScale.y, locScale.z);
            }
        }
    }

    public void OnDeath()
    {
        _rigidbody2D.gravityScale = 2f;
        _rigidbody2D.velocity = new Vector2(0, _rigidbody2D.velocity.y);
        _deathCollider.enabled = true;
    }
}