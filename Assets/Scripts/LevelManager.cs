using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public Transform[] enemySpawnPoints;
    public Transform playerSpawnPoint;
    public Enemy[] enemiesPrefabs;
    public List<Enemy> spawnedEnemies;
    
    public void SpawnEnemies()
    {
        Enemy[] enemies = GetEnemiesForLevel();
        for( int i=0; i<enemies.Length;i++)
        {
            SpawnEnemy(enemies[i],enemySpawnPoints[i]);
        }
    }
    public void SpawnEnemy(Enemy enemy,Transform transf)
    {
        Enemy newEnemy = Instantiate(enemy);
        newEnemy.transform.position = transf.position;
    }
    private Enemy[] GetEnemiesForLevel()
    {
        Enemy[] enemiesForSpawn = new Enemy[enemySpawnPoints.Length];
        for (int i = 0; i < enemySpawnPoints.Length; i++)
        {
            enemiesForSpawn[i] = enemiesPrefabs[Random.Range(0, enemiesPrefabs.Length)];
        }
        return enemiesForSpawn;
    }
}
