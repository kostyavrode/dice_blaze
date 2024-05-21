using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dice : MonoBehaviour
{
    public static Dice instance;
    private void Awake()
    {
        instance = this;
    }
    public int GetRoll()
    {
        return Random.Range(0, 7);
    }
}
