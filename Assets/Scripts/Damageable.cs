using UnityEngine;
using UnityEngine.AI;

public class Damageable : MonoBehaviour
{   
    Animator animator;

    [SerializeField]
    private int _maxHealth = 100;
    public int MaxHealth { get { 
            return _maxHealth; 
        } private set { 
            _maxHealth = value; 
        }
    }

    [SerializeField]
    private float _health = 100;

    public float Health { get { 
            return _health; 
        } private set { 
            _health = value; 
            if (_health <= 0)
            {
                IsAlive = false;
            }
        }
    }

    [SerializeField]
    private bool isInvincible = false;
    private float timeSinceHit = 0;
    public float invincibleTime = 0.25f;

    [SerializeField]
    private bool _isAlive = true;
    public bool IsAlive { get {
            return _isAlive;
        } private set {
            _isAlive = value;
            animator.SetBool(AnimationStrings.isAlive, value);
            Debug.Log("IsAlive: " + value);
        } 
    }

    void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void TakeDamage(int damage)
    {
        if (IsAlive && !isInvincible)
        {
            Health -= damage;
            isInvincible = true;
        }
    }

    private void Update()
    {
        if (isInvincible)
        {
            if(timeSinceHit >= invincibleTime)
            {
                isInvincible = false;
                timeSinceHit = 0;
            }
            else
            {
                timeSinceHit += Time.deltaTime;
            }
        }
    }
}
