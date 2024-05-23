using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour, IWarrior
{
    [SerializeField] private Animator animator;
    private int damage=1;
    private int hp=10;
    private int armor=1;

    public void Attack(int multi)
    {
        Debug.Log("Attaka=" + multi);
    }
    public int GetDamage()
    {
        return damage;
    }
    public void ReceiveDamage(int minushp)
    {
        hp -= minushp;
        Debug.Log("Enemy hp:" + hp);
        if (hp<=0)
        {
            Death();
        }
    }
    public void Init(int damage, int hp,int armor)
    {
        this.damage = damage;
        this.hp = hp;
        this.armor = armor;
    }
    public void StartTurn()
    {
        Debug.Log("Enemy Turn");
        Attack(Dice.instance.GetRoll());
    }
    private void Death()
    {
        //animator.SetTrigger("dead");
        Debug.Log("Enemy deaed");
        LevelManager.onEnemyDeath?.Invoke(this);
        gameObject.SetActive(false);
    }
}
