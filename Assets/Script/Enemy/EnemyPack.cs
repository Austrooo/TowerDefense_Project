using UnityEngine;

[CreateAssetMenu(fileName = "EnemyPack", menuName = "Scriptable Objects/EnemyPack")]
public class EnemyPack : ScriptableObject
{
    public GameObject enemyPrefab;
    public int quantity;
    public float spawnRate;
    public float speed;
}
