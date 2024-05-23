using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IWarrior
{
    void Attack(int multi);
    void ReceiveDamage(int minushp);
}
