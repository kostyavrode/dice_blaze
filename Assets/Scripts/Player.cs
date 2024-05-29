using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;
public class Player : MonoBehaviour, IWarrior
{
    public static Action onDestinationArrived;
    public static Action onPlayerDead;
    public Animator animator;
    private int damage=5;
    public int hp=50;
    private int armor;
    public bool isCanMakeTurn;
    private bool isFirstDestination;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isCanMakeTurn)
        {
            Attack(1);
        }
        if (Input.GetKeyDown(KeyCode.Q))
        {
            LegAttack();
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            SwordAttack();
        }
    }
    public void Attack(int multi)
    {
        isCanMakeTurn = false;
        BattleManager.onPlayerTurnMaked?.Invoke();
    }
    public void LegAttack()
    {
        animator.SetTrigger("leg");
    }
    public void SwordAttack()
    {
        animator.SetTrigger("sword");
    }
    public int GetDamage()
    {
        return damage;
    }
    public void ReceiveDamage(int minushp)
    {
        hp -= minushp;
        Debug.Log("Player HP last:" + hp);
        UIManager.instance.ChangeHP(hp);
        if (hp <= 0)
        {
            Dead();
        }
    }
    public int GetAttackParameters()
    {
        return damage;
    }
    public void Init(int damage, int hp, int armor)
    {
        this.damage = damage;
        this.hp = hp;
        this.armor = armor;
    }
    public void StartTurn()
    {
        Debug.Log("Player Turn");
        isCanMakeTurn = true;
    }
    public void MoveTo(Vector3 destination)
    {
        animator.SetBool("run",true);
        transform.DOMove(new Vector3(destination.x+0.2f,destination.y,destination.z-0.5f), 3).SetEase(Ease.Linear).OnComplete(StopRun);
    }
    public void PlayChestOpenAnim()
    {
        animator.SetBool("chest", true);
    }
    public void StopRun()
    {
        animator.SetBool("run", false);
        if (!isFirstDestination)
        onDestinationArrived?.Invoke();
        else
        isFirstDestination = true;
    }    
    public void Dead()
    {
        Debug.Log("Player Dead");
        onPlayerDead?.Invoke();
        animator.SetBool("dead", true);
    }    
}
