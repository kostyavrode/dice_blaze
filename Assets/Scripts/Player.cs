using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour, IWarrior
{
    public Animator animator;
    private int damage=5;
    private int hp=50;
    private int armor;
    public bool isCanMakeTurn;

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
    public void Dead()
    {
        Debug.Log("Player Dead");
    }    
}
