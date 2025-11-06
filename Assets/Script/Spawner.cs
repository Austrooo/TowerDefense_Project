using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Wave
{
    public string name;
    public List<EnemyPack> enemyPacks;
    public float waveInterval;
}

public class Spawner : MonoBehaviour
{
    [SerializeField] private GameObject enemy;
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private List<Wave> waves;

    private void Start()
    {
        spawnPoint = GameObject.Find("Path").transform.GetChild(0);
        StartCoroutine(BeginWave());
    }

    public IEnumerator SpawnEnemy(EnemyPack pack)
    {
        for (int i = 0; i < pack.quantity; i++)
        {
            GameObject spawnedEnemy = Instantiate(pack.enemyPrefab, spawnPoint.position, Quaternion.identity);
            spawnedEnemy.GetComponent<Enemy>().SetSpeed(pack.speed);
            yield return new WaitForSeconds(pack.spawnRate);
        }
    }

    public IEnumerator LoadCurrentWave(Wave wave)
    {
        foreach (EnemyPack packs in wave.enemyPacks)
        {
            yield return StartCoroutine(SpawnEnemy(packs));
        }
    }

    public IEnumerator BeginWave()
    {
        foreach(Wave wave in waves)
        {
            Debug.Log("Spawning wave : " + wave.name);
            yield return StartCoroutine(LoadCurrentWave(wave));
            yield return new WaitForSeconds(wave.waveInterval);
        }
    }
}
