using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchingDirections : MonoBehaviour
{
   public bool IsGrounded { get => _isGrounded;
      private set {
         _isGrounded = value;
         _animator.SetBool(AnimationStrings.isGrounded, value);
      } }
   public bool IsOnWall { get => _isOnWall;
      private set {
      _isOnWall = value;
      _animator.SetBool(AnimationStrings.isOnWall, value);
   } }
   public bool IsOnCeiling { get => _isOnCeiling;
      private set {
      _isOnCeiling = value;
      _animator.SetBool(AnimationStrings.isOnCeiling, value);
   } }
   
   [SerializeField] private bool _isGrounded;
   [SerializeField] private bool _isOnWall;
   [SerializeField] private bool _isOnCeiling;
   [SerializeField] private ContactFilter2D _castFilter2D;
   [SerializeField] private float _groundDistance = 0.05f;
   [SerializeField] private float _wallDistance = 0.2f;
   [SerializeField] private float _ceilingDistance = 0.05f;
   [SerializeField] private CapsuleCollider2D _touching;
   private Animator _animator;
   private Vector2 _wallCheckDirection => gameObject.transform.localScale.x > 0 ? Vector2.right : Vector2.left;
   private RaycastHit2D[] _groundHits = new RaycastHit2D[5];
   private RaycastHit2D[] _wallHits = new RaycastHit2D[5];
   private RaycastHit2D[] _ceilingHits = new RaycastHit2D[5];

   private void Awake()
   {
      _touching = GetComponent<CapsuleCollider2D>();
      _animator = GetComponent<Animator>();
   }
   private void FixedUpdate()
   {
      IsGrounded = _touching.Cast(Vector2.down, _castFilter2D, _groundHits, _groundDistance) > 0;
      IsOnWall = _touching.Cast(_wallCheckDirection, _castFilter2D, _wallHits, _wallDistance) > 0;
      IsOnCeiling = _touching.Cast(Vector2.up, _castFilter2D, _ceilingHits, _ceilingDistance) > 0;
   }
}
