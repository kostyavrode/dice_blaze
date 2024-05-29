using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dice : MonoBehaviour
{
    public static Dice instance;
    public DiceRoller diceRoller;
    public GameObject diceObj;
    private void Awake()
    {
        instance = this;
    }
    public int GetRoll()
    {
        int r = Random.Range(1, 6);
        diceObj.SetActive(true);
        diceRoller.Roll(r);
        return r;
    }
}
