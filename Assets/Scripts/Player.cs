using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour, IWarrior
{
    [SerializeField] private Animator animator;
    private int damage;
    private int hp;
    private int armor;
    private bool isCanMakeTurn;

    private void FixedUpdate()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isCanMakeTurn)
        {
            Attack(1);
        }
    }
    public void Attack(int multi)
    {
        Dice.instance.GetRoll();
        isCanMakeTurn = false;
        Debug.Log("AttakaPlayer=" + multi);
    }
    public void ReceiveDamage()
    {
        throw new System.NotImplementedException();
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
}
