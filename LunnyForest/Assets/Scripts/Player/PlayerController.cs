using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D), typeof(TouchingDirections), typeof(Damaged))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] private bool _isMoving;
    [SerializeField] private FillingInventory _fillingInventory;
    [SerializeField] private LocalDataProvider _localDataProvider;
    private bool _isFacingRight = true;
    private bool _canMove;
    private Damaged _damaged;

    public bool IsFacingRight
    {
        get { return _isFacingRight; }
        private set
        {
            if (_isFacingRight != value)
            {
                transform.localScale *= new Vector2(-1, 1);
            }

            _isFacingRight = value;
        }
    }

    private bool CanMove
    {
        get { return _animator.GetBool(AnimationStrings.canMove); }
    }

    private float CurrentMoveSpeed
    {
        get
        {
            if (!CanMove) return 0;
            if (IsMoving && !_touchingDirections.IsOnWall)
            {
                return _touchingDirections.IsGrounded ? _walkSpeed : _airWalkSpeed;
            }

            return 0;
        }
    }

    private bool IsMoving
    {
        get => _isMoving;
        set
        {
            _isMoving = value;
            _animator.SetBool(AnimationStrings.isMoving, value);
        }
    }

    public bool IsAlive => _animator.GetBool(AnimationStrings.isAlive);

    [SerializeField] private float _walkSpeed = 5f;
    [SerializeField] private float _airWalkSpeed = 3f;
    [SerializeField] private float _jumpImpulse = 10f;
    private Vector2 _moveInput;
    private Rigidbody2D _rigidbody2D;
    private Animator _animator;

    private TouchingDirections _touchingDirections;
    // private Quaternion TurnRight => new(0, 0, 0, 0);
    // private Quaternion TurnLeft => Quaternion.Euler(0, 180, 0);

    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _touchingDirections = GetComponent<TouchingDirections>();
        _damaged = GetComponent<Damaged>();
    }

    private void FixedUpdate()
    {
        if (!_damaged.LockVelocity)
            _rigidbody2D.velocity = new Vector2(_moveInput.x * CurrentMoveSpeed, _rigidbody2D.velocity.y);
        _animator.SetFloat(AnimationStrings.yVelocity, _rigidbody2D.velocity.y);
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        _moveInput = context.ReadValue<Vector2>();
        if (IsAlive)
        {
            IsMoving = _moveInput != Vector2.zero;
            SetFacingDirection(_moveInput);
        }
        else
        {
            IsMoving = false;
        }
        // transform.rotation = GetRotationFrom(_moveInput);
    }

    private void SetFacingDirection(Vector2 moveInput)
    {
        if (moveInput.x > 0 && !IsFacingRight)
        {
            IsFacingRight = true;
        }
        else if (moveInput.x < 0 && IsFacingRight)
        {
            IsFacingRight = false;
        }
    }

    // private Quaternion GetRotationFrom(Vector3 velocity)
    // {
    //     return velocity.x switch
    //     {
    //         > 0 => TurnRight,
    //         < 0 => TurnLeft,
    //         _ => transform.rotation
    //     };
    // }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.started && _touchingDirections.IsGrounded && CanMove)
        {
            _animator.SetTrigger(AnimationStrings.jumpTrigger);
            _rigidbody2D.velocity = new Vector2(_rigidbody2D.velocity.x, _jumpImpulse);
        }
    }

    public void OnAttack(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            _animator.SetTrigger(AnimationStrings.groundAttackTrigger);
        }
    }

    public void OnRangerAttack(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            _animator.SetTrigger(AnimationStrings.rangedAttack);
        }
    }

    public void OnUltimateAttack(InputAction.CallbackContext context)
    {
        if (context.started && Storage.PlayerInventory.PlayerItems.Any(x => x.Id == "SpAttack"))
        {
            if (!_fillingInventory.IsBlock)
            {
                StartCoroutine(_fillingInventory.BlockUlt());
                _animator.SetTrigger(AnimationStrings.ultimate);
                _animator.SetTrigger(AnimationStrings.groundAttackTrigger);
            }
        }
    }

    public void OnRoll(InputAction.CallbackContext context)
    {
        if (context.started && Storage.PlayerInventory.PlayerItems.Any(x => x.Id == "Roll"))
        {
            _animator.SetTrigger(AnimationStrings.roll);
        }
    }

    public void OnHit(int damage, Vector2 knockback)
    {
        _rigidbody2D.velocity = new Vector2(knockback.x, _rigidbody2D.velocity.y + knockback.y);
    }

    public void OnSmallHealth(InputAction.CallbackContext context)
    {
        var hp10 = Storage.PlayerInventory.PlayerItems.FirstOrDefault(x => x.Id == "HP10");
        if (context.started && hp10.Amount > 0)
        {
            _damaged.Heal(10);
            hp10.Amount--;
            _localDataProvider.SavePlayerInventory();
            _fillingInventory.ReductionPotions();
        }
        
    }

    public void OnHighHealth(InputAction.CallbackContext context)
    {
        var hp50 = Storage.PlayerInventory.PlayerItems.FirstOrDefault(x => x.Id == "HP50");
        if (context.started && hp50.Amount > 0)
        {
            _damaged.Heal(50);
            hp50.Amount--;
            _localDataProvider.SavePlayerInventory();
            _fillingInventory.ReductionPotions();
        }
        
    }
}