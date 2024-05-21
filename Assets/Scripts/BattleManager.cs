using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum Turn
{
    PLAYER = 0,
    ENEMY = 1
}

public class BattleManager : MonoBehaviour, IGameStartListener
{
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
    }
    public void SwitchTurn()
    {
        if (turn==Turn.PLAYER)
        {
            turn = Turn.ENEMY;
            int multi = Dice.instance.GetRoll();
            enemyInBattle.Attack(multi);
        }
        else
        {
            turn = Turn.PLAYER;
        }
    }
    private void CreateLevel()
    {
        levelManager.SpawnEnemies();
    }
}
