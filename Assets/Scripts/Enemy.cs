using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour,IWarrior
{
    [SerializeField] private Animator animator;
    private int damage;
    private int hp;
    private int armor;

    public void Attack(int multi)
    {
        Debug.Log("Attaka=" + multi);
    }
    public void ReceiveDamage()
    {
        throw new System.NotImplementedException();
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
}
