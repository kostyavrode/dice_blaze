using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using DG.Tweening;
using UnityEngine.Serialization;

public class NachalnikYrovnei : MonoBehaviour
{
    public static Action<Protivnik> onEnemyDeath; 
    public Transform[] enemySpawnPoints;
    public Transform playerSpawnPoint;
    public Protivnik[] enemiesPrefabs;
    public List<Protivnik> spawnedEnemies;
    [FormerlySerializedAs("player")] public Igrok igrok;
    [FormerlySerializedAs("currentEnemy")] public Protivnik currentProtivnik;
    public Transform rewardTransform;
    public Transform finalDestination;
    private int killedEnemy;
    public void SpawnEnemies()
    {
        Protivnik[] enemies = GetEnemiesForLevel();
        for( int i=0; i<enemies.Length;i++)
        {
            SpawnEnemy(enemies[i],enemySpawnPoints[i]);
        }
        currentProtivnik = spawnedEnemies[0];
        onEnemyDeath += DeleteEnemy;
    }
   
    public void OnDestroy()
    {
        onEnemyDeath -= DeleteEnemy;
    }
    public void SpawnEnemy(Protivnik protivnik,Transform transf)
    {
        Protivnik newProtivnik = Instantiate(protivnik);
        newProtivnik.transform.position = transf.position;
        spawnedEnemies.Add(newProtivnik);
    }
    private Protivnik[] GetEnemiesForLevel()
    {
        Protivnik[] enemiesForSpawn = new Protivnik[enemySpawnPoints.Length];
        for (int i = 0; i < enemySpawnPoints.Length; i++)
        {
            enemiesForSpawn[i] = enemiesPrefabs[UnityEngine.Random.Range(0, enemiesPrefabs.Length)];
        }
        return enemiesForSpawn;
    }
    public void DeleteEnemy(Protivnik protivnikForDelete)
    {
        spawnedEnemies.Remove(protivnikForDelete);
        try
        {
            currentProtivnik = spawnedEnemies[0];
        }
        catch
        {
            //GameManager.onEndGame?.Invoke();
        }
        if (spawnedEnemies.Count==0)
        {
            NachalnikBitvi.ZelebobaPomenyalsya?.Invoke(null);
            //GameManager.onEndGame?.Invoke();
            NachalnikBitvi.VseZelebobiYmerli?.Invoke();
        }
        else
        {
            Debug.Log("SPAWNED ENE<IES: " + spawnedEnemies.Count);
            NachalnikBitvi.ZelebobaPomenyalsya?.Invoke(currentProtivnik);

        }
    }
    public void OpenReward()
    {
        rewardTransform.DORotate(new Vector3(-55, -180, 0), 1).OnComplete(OnRewardOpened);
    }
    private void OnRewardOpened()
    {
        NachalnikIGRI.onEndGame?.Invoke();
        NachalnikUI.instance.ShowWinPanel();
    }
    public Protivnik GetCurrentEnemy()
    {
        return currentProtivnik;
    }
}
