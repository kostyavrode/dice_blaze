using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour, IWarrior
{
    [SerializeField] private Animator animator;
    private int damage=5;
    private int hp=50;
    private int armor;
    private bool isCanMakeTurn;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isCanMakeTurn)
        {
            Attack(1);
        }
    }
    public void Attack(int multi)
    {
        isCanMakeTurn = false;
        BattleManager.onPlayerTurnMaked?.Invoke();
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
