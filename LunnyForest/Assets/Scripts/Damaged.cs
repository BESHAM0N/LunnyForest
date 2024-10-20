using UnityEngine;
using UnityEngine.Events;

public class Damaged : MonoBehaviour
{
    public UnityEvent<int, Vector2> damageableHit;
    public UnityEvent damageableDeath;
    public UnityEvent<int, int> healthChanged;

    public int MaxHealth
    {
        get { return _maxHealth; }
        set { _maxHealth = value; }
    }

    [SerializeField] private int _health = 100;

    public int Health
    {
        get { return _health; }
        set
        {
            _health = value;
            healthChanged?.Invoke(_health, MaxHealth);
            if (_health <= 0)
            {
                IsAlive = false;
            }
        }
    }

    [SerializeField] private bool _isAlive = true;

    public bool IsAlive
    {
        get { return _isAlive; }
        set
        {
            _isAlive = value;
            _animator.SetBool(AnimationStrings.isAlive, value);

            if (value == false)
            {
                damageableDeath.Invoke();
            }
          
        }
    }

    [SerializeField] private int _maxHealth = 100;
    [SerializeField] private bool _isInvincible;
    [SerializeField] private float _invincibilityTime = 0.25f;
    private float _timeSinceHit;
    private Animator _animator;

    public bool LockVelocity
    {
        get { return _animator.GetBool(AnimationStrings.lockVelocity); }
        set { _animator.SetBool(AnimationStrings.lockVelocity, value); }
    }

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (_isInvincible)
        {
            if (_timeSinceHit > _invincibilityTime)
            {
                _isInvincible = false;
                _timeSinceHit = 0;
            }

            _timeSinceHit += Time.deltaTime;
        }
    }

    public bool TakeDamage(int damage, Vector2 knockback)
    {
        if (IsAlive && !_isInvincible)
        {
            Health -= damage;
            _isInvincible = true;
            _animator.SetTrigger(AnimationStrings.hitTrigger);
            LockVelocity = true;
            damageableHit?.Invoke(damage, knockback);
            CharacterEvents.characterDamaged.Invoke(gameObject, damage);
            return true;
        }

        return false;
    }

    public bool Heal(int healthRestore)
    {
        if (IsAlive && Health < MaxHealth)
        {
            int maxHeal = Mathf.Max(MaxHealth - Health, 0);
            int actualHeal = Mathf.Min(maxHeal, healthRestore);
            Health += actualHeal;
            CharacterEvents.characterHealed(gameObject, actualHeal);
            return true;
        }

        return false;
    }
}