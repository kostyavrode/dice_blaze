using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfoHandler : MonoBehaviour
{
    public int level;
    public int hp;
    public int armor;
    public int damage;
    private void Awake()
    {
        if (!PlayerPrefs.HasKey("level"))
        {
            PlayerPrefs.SetInt("level", 0);
            PlayerPrefs.SetInt("hp", 10);
            PlayerPrefs.SetInt("armor", 0);
            PlayerPrefs.SetInt("damage", 2);
            PlayerPrefs.Save();
        }
        else
        {
            level = PlayerPrefs.GetInt("level");
            hp = PlayerPrefs.GetInt("hp");
            armor = PlayerPrefs.GetInt("armor");
            damage = PlayerPrefs.GetInt("damage");
        }
    }

}
