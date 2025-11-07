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
    void Start()
    {
        GetAllPath();
    }

    void Update()
    {
        SetNextDestination();
        MoveToDestination();
    }

    // getter and setter

    public void SetSpeed(float newSpeed)
    {
        speed = newSpeed;
    }

    // main Enemy Movement methods

    void GetAllPath()
    {
        Transform[] pathObjects = GameObject.Find("Path").GetComponentsInChildren<Transform>();
        paths = new Transform[pathObjects.Length - 1];
        for (int i = 1; i < pathObjects.Length; i++)
        {
            paths[i - 1] = pathObjects[i];
        }
    }
    
    void SetNextDestination()
    {
        if (target != null) return;
        if (currentPathIndex < paths.Length - 1)
        {
            target = paths[currentPathIndex + 1];
        }
    }

    void MoveToDestination()
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

    // IDamageable implementation
    
    public void TakeDamage(float damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }
}
