using UnityEngine;

public class Attack : MonoBehaviour
{   
    public int attackDamage = 10;
    void Awake()
    {

    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        Damageable damageable = collision.GetComponent<Damageable>();

        if (damageable != null)
        {
            damageable.TakeDamage(attackDamage);
        }
    }
}
