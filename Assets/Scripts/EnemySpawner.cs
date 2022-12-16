using ProcGen;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public static EnemySpawner _;
    public float spawnRate;
    public float spawnRadius;
    float spawnTimer;
    //public BaseEnemy slime;
    //public GrowingPool<BaseEnemy> slimePool;

    public int waveCounter = 0;
    public int pooledPerWave = 20;
    public List<EnemyWave> waves = new();
    public static EnemyWave CurrentWave => _.waves[_.waveCounter];
    private void Awake()
    {
        _ = this;
    }
    private void Start()
    {
        NextWave();
    }
    public void NextWave()
    {
        CurrentWave.firePool = new GrowingPool<BaseEnemy>(waves[waveCounter].fireEnemies, pooledPerWave);
        CurrentWave.windPool = new GrowingPool<BaseEnemy>(waves[waveCounter].windEnemies, pooledPerWave);
        CurrentWave.earthPool = new GrowingPool<BaseEnemy>(waves[waveCounter].earthEnemies, pooledPerWave);
        CurrentWave.waterPool = new GrowingPool<BaseEnemy>(waves[waveCounter].waterEnemies, pooledPerWave);
        waveCounter++;
    }
    private void Update()
    {
        spawnTimer += Time.deltaTime;
        if (spawnTimer > spawnRate)
        {
            spawnTimer = 0;
            for (int i = 0; i < waves[waveCounter - 1].spawnCount; i++)
            {
                Vector2 randDir = new Vector2(Random.value * 2 - 1, Random.value * 2 - 1);
                Vector2 randPos = (Vector2)GameManager.Player.transform.position + randDir.normalized * spawnRadius;
                var spawnRegion = MapManager.GetRegion(randPos);
                Color col = spawnRegion.biome.elementSpawnRate.Evaluate(Random.value);
                switch (AssetDB._.colToElement[col])
                {
                    case Element.Fire:
                        CurrentWave.firePool.Instantiate(randPos, Quaternion.identity).Init();
                        break;
                    case Element.Wind:
                        CurrentWave.windPool.Instantiate(randPos, Quaternion.identity).Init();
                        break;
                    case Element.Earth:
                        CurrentWave.earthPool.Instantiate(randPos, Quaternion.identity).Init();
                        break;
                    case Element.Water:
                        CurrentWave.waterPool.Instantiate(randPos, Quaternion.identity).Init();
                        break;
                }
            }
        }
    }
}
