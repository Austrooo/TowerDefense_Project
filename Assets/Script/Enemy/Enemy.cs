using UnityEngine;
public interface IDamageable
{
    void TakeDamage(float damage);
}
public class Enemy : MonoBehaviour, IDamageable
{
    [SerializeField] private float health;
    [SerializeField] private float speed;
    private Transform[] paths;
    private int currentPathIndex = 0;
    private bool moving;
    private Transform target;
    public float GetSpeed() { return speed; }
    void Start()
    {
        GetAllPath();
    }

    void Update()
    {
        SetTarget();
        MoveToTarget();
    }

    void GetAllPath()
    {
        Transform[] pathObjects = GameObject.Find("Path").GetComponentsInChildren<Transform>();
        paths = new Transform[pathObjects.Length - 1];
        for (int i = 1; i < pathObjects.Length; i++)
        {
            paths[i - 1] = pathObjects[i];
        }
    }
    
    void SetTarget()
    {
        if (target != null) return;
        if (currentPathIndex < paths.Length - 1)
        {
            target = paths[currentPathIndex + 1];
        }
    }

    void MoveToTarget()
    {
        if (target == null) return;

        float step = speed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, target.position, step);

        if (Vector3.Distance(transform.position, target.position) == 0f)
        {
            currentPathIndex++;
            target = null;
            if (currentPathIndex == paths.Length - 1)
            {
                Destroy(gameObject);
            }
        }
    }
    
    public void TakeDamage(float damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }
}
