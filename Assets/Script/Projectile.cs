using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float damage;
    [SerializeField] private Rigidbody2D rb;
    private Transform target;

    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);

        float distance = Vector2.Distance(transform.position, target.position);
        float distanceLimit = 0.5f;

        if (distance < distanceLimit)
        {
            IDamageable damageable = target.GetComponent<IDamageable>();
            if (damageable != null)
            {
                damageable.TakeDamage(damage);
            }
            Destroy(gameObject);
        }
    }

    public void SetTarget(Transform _target)
    {
        target = _target;
    }

    public void SetDamage(float _damage)
    {
        damage = _damage;
    }
}
