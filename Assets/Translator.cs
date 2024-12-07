using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Translator : MonoBehaviour
{
    public static Translator instance;

    public bool isPortu;

    public Sprite portuSprite;

    public Image imaheHolder;

    public string[] portugales;

    public TMP_Text[] textBars;
    
    public string fontPath = "Resources/LiberationSans SDF";

    private void Awake()
    {
        if (PlayerPrefs.GetInt("ISFIRST")==1)
        {
            Destroy(gameObject);
        }
        else
        {
            PlayerPrefs.SetInt("ISFIRST", 1);
        }
        instance = this;
    }

    private void OnLevelWasLoaded(int level)
    {
        if (PlayerPrefs.GetInt("isPortu")==1 && Translator.instance.GetHashCode()==this.GetHashCode())
        {
            Translate();
        }
    }
    public void NotTranslate()
    {
        PlayerPrefs.SetInt("isPortu", 0);
    }
    public void Translate()
    {
        TMP_FontAsset fontAsset = Resources.Load<TMP_FontAsset>(fontPath);
        for (int i = 0; i < portugales.Length; i++)
        {
            textBars[i].text = portugales[i];
            textBars[i].font = fontAsset;
            textBars[i].enableAutoSizing = true;
            textBars[i].fontSizeMax = 160;
        }
        //imaheHolder.sprite=portuSprite;
        //isPortu = true;
        PlayerPrefs.SetInt("isPortu", 1);
    }
}