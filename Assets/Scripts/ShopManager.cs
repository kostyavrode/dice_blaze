using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopManager : MonoBehaviour
{
    public int cost1;
    public int cost2;
    public int cost3;
    public int cost4;
    public GameObject buyButton1;
    public GameObject buyButton2;
    public GameObject buyButton3;
    public GameObject buyButton4;
    private void OnEnable()
    {
        CheckBuy();
    }
    private void CheckBuy()
    {
        if (PlayerPrefs.HasKey("Buy1"))
        {
            buyButton1.SetActive(false);
        }
        if (PlayerPrefs.HasKey("Buy2"))
        {
            buyButton2.SetActive(false);
        }
        if (PlayerPrefs.HasKey("Buy3"))
        {
            buyButton3.SetActive(false);
        }
        if (PlayerPrefs.HasKey("Buy4"))
        {
            buyButton4.SetActive(false);
        }
        UIManager.instance.ShowMoney(PlayerPrefs.GetInt("Money").ToString());
        //Checkerss.instance.CheckBuy();
    }
    public void Buy1()
    {
        if (PlayerPrefs.GetInt("Money") >= cost1)
        {
            PlayerPrefs.SetInt("Money", PlayerPrefs.GetInt("Money") - cost1);
            PlayerPrefs.SetString("Buy1", "true");
            PlayerPrefs.Save();
            CheckBuy();
        }
    }
    public void Buy2()
    {
        if (PlayerPrefs.GetInt("Money") >= cost2)
        {
            PlayerPrefs.SetInt("Money", PlayerPrefs.GetInt("Money") - cost2);
            PlayerPrefs.SetString("Buy2", "true");
            PlayerPrefs.Save();
            CheckBuy();
        }
    }
    public void Buy3()
    {
        if (PlayerPrefs.GetInt("Money") >= cost3)
        {
            PlayerPrefs.SetInt("Money", PlayerPrefs.GetInt("Money") - cost3);
            PlayerPrefs.SetString("Buy3", "true");
            PlayerPrefs.Save();
            CheckBuy();
        }
    }
    public void Buy4()
    {
        if (PlayerPrefs.GetInt("Money") >= cost4)
        {
            PlayerPrefs.SetInt("Money", PlayerPrefs.GetInt("Money") - cost4);
            PlayerPrefs.SetString("Buy4", "true");
            PlayerPrefs.Save();
            CheckBuy();
        }
    }
}
