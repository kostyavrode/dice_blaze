using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public enum Turn
{
    PLAYER = 0,
    ENEMY=1
}
public enum AttackType
{
    LEG=0,
    SWORD=1
}

public class BattleManager : MonoBehaviour, IGameStartListener, IGameFinishedListener
{
    public static Action onPlayerTurnMaked;
    public static Action<Enemy> onEnemyChanged;
    public static Action onTurnSwitch;
    public static Action onAllEnemiesDied;
    [SerializeField] private LevelManager levelManager;
    private Turn turn;
    private bool isBattleStarted;
    private Enemy enemyInBattle;
    private bool isWaitingForGiveDamage;
    private bool isEnemyChanged;
    private int diceRollResult;
    public Turn Turn
    {
        get { return turn; }
    }
    private void Awake()
    {
        if (!levelManager)
        {
            levelManager = GetComponent<LevelManager>();
        }
        Player.onDestinationArrived += ContinueFight;
        onAllEnemiesDied += EndBattle;
        Player.onPlayerDead += StopGamePlayerDead;
    }
    private void OnDisable()
    {
        onAllEnemiesDied -= EndBattle;
        Player.onDestinationArrived -= ContinueFight;
        Player.onPlayerDead -= StopGamePlayerDead;
    }
    private void Update()
    {
        if (isWaitingForGiveDamage)
        {
            if (IsAnimationPlaying("idle"))
            {
                if (turn == Turn.ENEMY)
                {
                    levelManager.player.ReceiveDamage(levelManager.currentEnemy.GetDamage() * diceRollResult);
                    Debug.Log("END ENEMY TURN");
                    turn = Turn.PLAYER;
                    levelManager.player.StartTurn();
                    isWaitingForGiveDamage = false;
                    onTurnSwitch?.Invoke();
                    SwitchTurn();
                }
                else
                {
                    enemyInBattle.ReceiveDamage(levelManager.player.GetAttackParameters() * diceRollResult);
                    CheckEnemy();
                    CheckDistanceBetweenEnemy();
                    if (enemyInBattle && !isEnemyChanged)
                    {
                        turn = Turn.ENEMY;
                        Debug.Log("END PLAYER TURN");
                        SwitchTurn();
                    }
                    
                    isWaitingForGiveDamage = false;
                    
                }
            }
        }
    }
    private void StopGamePlayerDead()
    {
        GameManager.onEndGame?.Invoke();
        //enemyInBattle = null;
        isWaitingForGiveDamage = false;
        UIManager.instance.ViewAttackUI(false);
        //UIManager.instance.ShowWinPanel();
        //InfoController.instance.ReceiveMoney(UnityEngine.Random.Range(0, 1));
        UIManager.instance.ShowLosePanel();
        UIManager.instance.isGameEnd = true;
    }
    void IGameStartListener.OnGameStarted()
    {
        CreateLevel();
        isBattleStarted = true;
        turn = Turn.PLAYER;
        onPlayerTurnMaked += SwitchTurn;
        onEnemyChanged += ChangeEnemy;
        enemyInBattle = levelManager.currentEnemy;
        Debug.Log("Battle_Started");
        levelManager.player.MoveTo(enemyInBattle.transform.position);
        StartCoroutine(WaitTostartBattle());
        UIManager.instance.ViewAttackUI(false);
    }
    void IGameFinishedListener.OnGameFinished()
    {
        onPlayerTurnMaked -= SwitchTurn;
        onEnemyChanged -= ChangeEnemy;
    }
    public void Attack(AttackType type=AttackType.SWORD)
    {
        diceRollResult = Dice.instance.GetRoll();
        UIManager.instance.ShowDiceRollResult(diceRollResult.ToString());
        if (turn==Turn.PLAYER)
        {
            switch (type)
            {
                case AttackType.LEG:
                    {
                        levelManager.player.LegAttack();
                        break;
                    }
                case AttackType.SWORD:
                    {
                        levelManager.player.SwordAttack();
                        break;
                    }
            }
            StartCoroutine(Wait1Sec());
        }
        else
        {
            enemyInBattle.animator.SetTrigger("attack");
            StartCoroutine(Wait1Sec());
        }
    }
    public void SwitchTurn()
    {
        if (turn==Turn.PLAYER)
        {
            levelManager.player.isCanMakeTurn = true;
        }
        else
        {
            Attack();
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
        isEnemyChanged = true;
    }
    private bool IsAnimationPlaying(string animationName)
    {
        if (turn==Turn.ENEMY)
        {
            var animatorStateInfo = enemyInBattle.animator.GetCurrentAnimatorStateInfo(0);
            if (animatorStateInfo.IsName(animationName))
                return true;

            return false;
        }
        else
        {
            var animatorStateInfo = levelManager.player.animator.GetCurrentAnimatorStateInfo(0);
            if (animatorStateInfo.IsName(animationName))
                return true;

            return false;
        }   
    }
    private void EndBattle()
    {
        MovePlayerToReward();
        UIManager.instance.ViewAttackUI(false);
        levelManager.player.PlayChestOpenAnim();
        InfoController.instance.ReceiveMoney(UnityEngine.Random.Range(8, 10));
    }
    private void CheckDistanceBetweenEnemy()
    {
        if (levelManager.currentEnemy.transform.position.z- levelManager.player.transform.position.z>3)
        {
            levelManager.player.MoveTo(levelManager.currentEnemy.transform.position);
        }
    }
    private void ContinueFight()
    {
        if (enemyInBattle)
        {
            isEnemyChanged = false;
            turn = Turn.ENEMY;
            onTurnSwitch?.Invoke();
            SwitchTurn();
        }
        else
        {
            levelManager.OpenReward();
        }
    }
    private void MovePlayerToReward()
    {
        levelManager.player.MoveTo(levelManager.finalDestination.position);
    }
    private IEnumerator Wait1Sec()
    {
        yield return new WaitForSeconds(0.5f);
        isWaitingForGiveDamage = true;
    }
    private IEnumerator WaitTostartBattle()
    {
        yield return new WaitForSeconds(2);
        SwitchTurn();
    }
}
