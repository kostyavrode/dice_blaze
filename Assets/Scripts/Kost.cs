using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Kost : MonoBehaviour
{
    public static Kost instance;
    [FormerlySerializedAs("diceRoller")] public KostKrytka kostKrytka;
    public GameObject diceObj;
    private void Awake()
    {
        instance = this;
    }
    public int GetRoll()
    {
        int r = Random.Range(1, 6);
        diceObj.SetActive(true);
        kostKrytka.Roll(r);
        return r;
    }
}
