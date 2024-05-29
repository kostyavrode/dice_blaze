using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfoController : MonoBehaviour, IGameFinishedListener
{
    public static InfoController instance;
    public void Start()
    {
        instance = this;
        if (!PlayerPrefs.HasKey("Money"))
        {
            PlayerPrefs.SetInt("Money", 0);
            UIManager.instance.ShowMoney("0");
            PlayerPrefs.Save();
        }
        else
        {
            UIManager.instance.ShowMoney(PlayerPrefs.GetInt("Money").ToString());
        }
    }
    public void ReceiveMoney(int m)
    {
        int tempMoney = PlayerPrefs.GetInt("Money");
        PlayerPrefs.SetInt("Money", tempMoney + m);
        PlayerPrefs.Save();
        UIManager.instance.ShowEarnedGold(m.ToString());
        UIManager.instance.ShowMoney((tempMoney + m).ToString());
    }

    void IGameFinishedListener.OnGameFinished()
    {
        
    }
}
