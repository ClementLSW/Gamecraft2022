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
    public static EnemyWave CurrentWave => _.waveCounter == 0 ? null : _.waves[_.waveCounter - 1];
    private void Awake()
    {
        _ = this;
    }
    public void NextWave()
    {
        waveCounter++;
        if (waveCounter >= waves.Count)
        {
            GameManager.instance.WinGame();
            return;
        }
        CurrentWave.firePool = new GrowingPool<BaseEnemy>(CurrentWave.fireEnemies, pooledPerWave);
        CurrentWave.windPool = new GrowingPool<BaseEnemy>(CurrentWave.windEnemies, pooledPerWave);
        CurrentWave.earthPool = new GrowingPool<BaseEnemy>(CurrentWave.earthEnemies, pooledPerWave);
        CurrentWave.waterPool = new GrowingPool<BaseEnemy>(CurrentWave.waterEnemies, pooledPerWave);
    }
    private void Update()
    {
        if (CurrentWave == null) return;
        spawnTimer += Time.deltaTime;
        if (spawnTimer > spawnRate)
        {
            spawnTimer = 0;
            for (int i = 0; i < CurrentWave.spawnCount; i++)
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
