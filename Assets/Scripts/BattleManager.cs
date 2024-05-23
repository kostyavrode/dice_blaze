using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public enum Turn
{
    PLAYER = 0,
    ENEMY = 1
}

public class BattleManager : MonoBehaviour, IGameStartListener, IGameFinishedListener
{
    public static Action onPlayerTurnMaked;
    public static Action<Enemy> onEnemyChanged;
    [SerializeField] private LevelManager levelManager;
    private Turn turn;
    private bool isBattleStarted;
    private Enemy enemyInBattle;
    private void Awake()
    {
        if (!levelManager)
        {
            levelManager = GetComponent<LevelManager>();
        }
    }
    void IGameStartListener.OnGameStarted()
    {
        CreateLevel();
        isBattleStarted = true;
        turn = Turn.ENEMY;
        SwitchTurn();
        onPlayerTurnMaked += SwitchTurn;
        onEnemyChanged += ChangeEnemy;
        enemyInBattle = levelManager.currentEnemy;
        Debug.Log("Battle_Started");
    }
    void IGameFinishedListener.OnGameFinished()
    {
        onPlayerTurnMaked -= SwitchTurn;
        onEnemyChanged -= ChangeEnemy;
    }
    public void SwitchTurn()
    {
        if (turn==Turn.PLAYER)
        {
            turn = Turn.ENEMY;
            enemyInBattle.ReceiveDamage(levelManager.player.GetAttackParameters() * Dice.instance.GetRoll());
            CheckEnemy();
        }
        else
        {
            turn = Turn.PLAYER;
            levelManager.player.StartTurn();
            levelManager.player.ReceiveDamage(levelManager.currentEnemy.GetDamage() * Dice.instance.GetRoll());
            
        }
    }
    private void CreateLevel()
    {
        levelManager.SpawnEnemies();
    }
    private void CheckEnemy()
    {
        levelManager.GetCurrentEnemy();
    }
    private void ChangeEnemy(Enemy en)
    {
        enemyInBattle = en;
        Debug.Log("Enemy changed");
    }

}
