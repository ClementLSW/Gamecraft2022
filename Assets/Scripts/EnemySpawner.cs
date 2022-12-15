using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public float spawnRate;
    public float spawnRadius;
    public int spawnCount;
    float spawnTimer;
    public BaseEnemy slime;
    public GrowingPool<BaseEnemy> slimePool;
    private void Start()
    {
        slimePool = new GrowingPool<BaseEnemy>(slime, 100);
    }
    private void Update()
    {
        spawnTimer += Time.deltaTime;
        if (spawnTimer > spawnRate)
        {
            spawnTimer = 0;
            for (int i = 0; i < spawnCount; i++)
            {
                Vector2 randDir = new Vector2(Random.value * 2 - 1, Random.value * 2 - 1);
                Vector2 randPos = (Vector2)GameManager.Player.transform.position + randDir.normalized * spawnRadius;
                var slime = slimePool.Instantiate(randPos, Quaternion.identity);
                slime.Init();
            }
        }
    }
}
