using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using DG.Tweening;
public class LevelManager : MonoBehaviour
{
    public static Action<Enemy> onEnemyDeath; 
    public Transform[] enemySpawnPoints;
    public Transform playerSpawnPoint;
    public Enemy[] enemiesPrefabs;
    public List<Enemy> spawnedEnemies;
    public Player player;
    public Enemy currentEnemy;
    public Transform rewardTransform;
    public Transform finalDestination;
    private int killedEnemy;
    public void SpawnEnemies()
    {
        Enemy[] enemies = GetEnemiesForLevel();
        for( int i=0; i<enemies.Length;i++)
        {
            SpawnEnemy(enemies[i],enemySpawnPoints[i]);
        }
        currentEnemy = spawnedEnemies[0];
        onEnemyDeath += DeleteEnemy;
    }
   
    public void OnDestroy()
    {
        onEnemyDeath -= DeleteEnemy;
    }
    public void SpawnEnemy(Enemy enemy,Transform transf)
    {
        Enemy newEnemy = Instantiate(enemy);
        newEnemy.transform.position = transf.position;
        spawnedEnemies.Add(newEnemy);
    }
    private Enemy[] GetEnemiesForLevel()
    {
        Enemy[] enemiesForSpawn = new Enemy[enemySpawnPoints.Length];
        for (int i = 0; i < enemySpawnPoints.Length; i++)
        {
            enemiesForSpawn[i] = enemiesPrefabs[UnityEngine.Random.Range(0, enemiesPrefabs.Length)];
        }
        return enemiesForSpawn;
    }
    public void DeleteEnemy(Enemy enemyForDelete)
    {
        spawnedEnemies.Remove(enemyForDelete);
        try
        {
            currentEnemy = spawnedEnemies[0];
        }
        catch
        {
            //GameManager.onEndGame?.Invoke();
        }
        if (spawnedEnemies.Count==0)
        {
            BattleManager.onEnemyChanged?.Invoke(null);
            //GameManager.onEndGame?.Invoke();
            BattleManager.onAllEnemiesDied?.Invoke();
        }
        else
        {
            Debug.Log("SPAWNED ENE<IES: " + spawnedEnemies.Count);
            BattleManager.onEnemyChanged?.Invoke(currentEnemy);

        }
    }
    public void OpenReward()
    {
        rewardTransform.DORotate(new Vector3(-55, -180, 0), 1).OnComplete(OnRewardOpened);
    }
    private void OnRewardOpened()
    {
        GameManager.onEndGame?.Invoke();
        UIManager.instance.ShowWinPanel();
    }
    public Enemy GetCurrentEnemy()
    {
        return currentEnemy;
    }
}
