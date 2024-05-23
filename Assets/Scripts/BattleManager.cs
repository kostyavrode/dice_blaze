using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public enum Turn
{
    PLAYER = 0,
    ENEMY=1
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
        
        onPlayerTurnMaked += SwitchTurn;
        onEnemyChanged += ChangeEnemy;
        enemyInBattle = levelManager.currentEnemy;
        Debug.Log("Battle_Started");
        SwitchTurn();
    }
    void IGameFinishedListener.OnGameFinished()
    {
        onPlayerTurnMaked -= SwitchTurn;
        onEnemyChanged -= ChangeEnemy;
    }
    public void SwitchTurn()
    {
        Debug.Log("SWITCH TURN");
        if (turn==Turn.PLAYER)
        {
            Debug.Log("START PLAYER TURN");
            levelManager.player.isCanMakeTurn = true;
            enemyInBattle.ReceiveDamage(levelManager.player.GetAttackParameters() * Dice.instance.GetRoll());
            CheckEnemy();
            if (enemyInBattle)
            {
                turn = Turn.ENEMY;
                Debug.Log("END PLAYER TURN");
                SwitchTurn();
            }
        }
        else
        {
            Debug.Log("START ENEMY TURN");
            
            levelManager.player.ReceiveDamage(levelManager.currentEnemy.GetDamage() * Dice.instance.GetRoll());
            Debug.Log("END ENEMY TURN");
            turn = Turn.PLAYER;
            levelManager.player.StartTurn();
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
