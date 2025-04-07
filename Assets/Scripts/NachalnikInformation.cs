using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NachalnikInformation : MonoBehaviour, IGameFinishedListener
{
    public static NachalnikInformation instance;
    public void Start()
    {
        instance = this;
        if (!PlayerPrefs.HasKey("Money"))
        {
            PlayerPrefs.SetInt("Money", 0);
            NachalnikUI.instance.ShowMoney("0");
            PlayerPrefs.Save();
        }
        else
        {
            NachalnikUI.instance.ShowMoney(PlayerPrefs.GetInt("Money").ToString());
        }
    }
    public void ReceiveMoney(int m)
    {
        int tempMoney = PlayerPrefs.GetInt("Money");
        PlayerPrefs.SetInt("Money", tempMoney + m);
        PlayerPrefs.Save();
        NachalnikUI.instance.ShowEarnedGold(m.ToString());
        NachalnikUI.instance.ShowMoney((tempMoney + m).ToString());
    }

    void IGameFinishedListener.OnGameFinished()
    {
        
    }
}
