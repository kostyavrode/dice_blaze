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
    [SerializeField] private LevelManager levelManager;
    private Turn turn;
    private bool isBattleStarted;
    private Enemy enemyInBattle;
    private bool isWaitingForGiveDamage;
    private bool isEnemyChanged;
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
    }
    private void OnDisable()
    {
        Player.onDestinationArrived -= ContinueFight;
    }
    private void Update()
    {
        if (isWaitingForGiveDamage)
        {
            if (IsAnimationPlaying("idle"))
            {
                if (turn == Turn.ENEMY)
                {
                    levelManager.player.ReceiveDamage(levelManager.currentEnemy.GetDamage() * Dice.instance.GetRoll());
                    Debug.Log("END ENEMY TURN");
                    turn = Turn.PLAYER;
                    levelManager.player.StartTurn();
                    isWaitingForGiveDamage = false;
                    onTurnSwitch?.Invoke();
                    SwitchTurn();
                }
                else
                {
                    enemyInBattle.ReceiveDamage(levelManager.player.GetAttackParameters() * Dice.instance.GetRoll());
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
    void IGameStartListener.OnGameStarted()
    {
        CreateLevel();
        isBattleStarted = true;
        turn = Turn.PLAYER;
        
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
    public void Attack(AttackType type=AttackType.SWORD)
    {
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
        Debug.Log("SWITCH TURN");
        if (turn==Turn.PLAYER)
        {
            Debug.Log("START PLAYER TURN");
            levelManager.player.isCanMakeTurn = true;
            //enemyInBattle.ReceiveDamage(levelManager.player.GetAttackParameters() * Dice.instance.GetRoll());
            //CheckEnemy();
            //if (enemyInBattle)
            //{
            //    turn = Turn.ENEMY;
            //    Debug.Log("END PLAYER TURN");
            //    SwitchTurn();
            //}
        }
        else
        {
            //Debug.Log("START ENEMY TURN");
            Attack();
            //levelManager.player.ReceiveDamage(levelManager.currentEnemy.GetDamage() * Dice.instance.GetRoll());
            //Debug.Log("END ENEMY TURN");
            //turn = Turn.PLAYER;
            //levelManager.player.StartTurn();
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
    private void CheckDistanceBetweenEnemy()
    {
        if (levelManager.currentEnemy.transform.position.z- levelManager.player.transform.position.z>3)
        {
            levelManager.player.MoveTo(levelManager.currentEnemy.transform.position);
        }
    }
    private void ContinueFight()
    {
        isEnemyChanged = false;
        turn = Turn.ENEMY;
        onTurnSwitch?.Invoke();
        SwitchTurn();
        
    }
    private IEnumerator Wait1Sec()
    {
        yield return new WaitForSeconds(0.5f);
        isWaitingForGiveDamage = true;
    }
}
