using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "EnemyWave.asset", menuName = "EnemyWave")]
public class EnemyWave : ScriptableObject
{
    public int spawnCount;
    public BaseEnemy fireEnemies;
    public BaseEnemy windEnemies;
    public BaseEnemy earthEnemies;
    public BaseEnemy waterEnemies;
}
