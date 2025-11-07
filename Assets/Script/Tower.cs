using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private LayerMask enemyLayer;
    [Header("Attributes")]
    private bool canAttack = true;
    [SerializeField] private float range;
    [SerializeField] private float damage;
    [SerializeField] private float fireRate;
    [SerializeField] private Transform enemy;
    
    void Update()
    {
        if (enemy == null)
        {
            Debug.Log("no current target, finding target");
            FindTarget();
            return;
        }

        if (!IsTargetInRange())
        {
            enemy = null;
        }
        else if (canAttack) StartCoroutine(Attack());        
    }

    private void FindTarget()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(
            point: transform.position,
            radius: range,
            layerMask: enemyLayer
            );

        if (hits.Length > 0)
        {
            enemy = hits[0].transform;
        }
    }

    IEnumerator Attack()
    {
        Debug.Log("Attacking Enemy");
        canAttack = false;
        enemy.GetComponent<Enemy>().TakeDamage(damage);
        yield return new WaitForSeconds(1 / fireRate);
        canAttack = true;
    }

    private bool IsTargetInRange()
    {
        return Vector2.Distance(enemy.position, transform.position) < range;
    }
}
