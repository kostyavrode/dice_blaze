using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

public class NachalnikUI : MonoBehaviour
{
    public static NachalnikUI instance;
    [FormerlySerializedAs("battleManager")] [SerializeField] private NachalnikBitvi nachalnikBitvi;
    [SerializeField] private GameObject inGameUI;
    [SerializeField] private GameObject attackUI;
    [SerializeField] private TMP_Text goldToEarnText;
    [SerializeField] private Image hpBar;
    [SerializeField] private TMP_Text moneyBar;
    [SerializeField] private TMP_Text diceRollResultText;
    [SerializeField] private GameObject winPanel;
    [SerializeField] private GameObject losePanel;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] public GameObject[] elements;
    [SerializeField] private GameObject blackWindow;
    [SerializeField] private AudioSource source;
    private bool isFirstTime=true;
    public bool isGameEnd;
    private void Awake()
    {
        instance = this;
        NachalnikBitvi.HodPomenyalsya += CheckAttackUI;
        if (PlayerPrefs.GetString("Sound") == "true")
        {
            audioSource.Play();
        }
        else
        {
            audioSource.Pause();
        }
    }
    private void OnDestroy()
    {
        NachalnikBitvi.HodPomenyalsya -= CheckAttackUI;
    }
    private void FixedUpdate()
    {
        if (isGameEnd)
        {
            attackUI.SetActive(false);
        }
    }
    public void CloseUI()
    {
        source.Pause();
        foreach (GameObject obj in elements)
        {
            obj.SetActive(false);
        }
        blackWindow.SetActive(true);

    }
    public void StartGame()
    {
        NachalnikIGRI.instance.StartGame();
        inGameUI.SetActive(true);
    }
    public void LegAttackButton()
    { 
       nachalnikBitvi.Attack(KakBit.LEG);
    }
    public void SwordAttackButton()
    {
        nachalnikBitvi.Attack(KakBit.SWORD);
    }
    private void CheckAttackUI()
    {
        if (nachalnikBitvi.Ochered==Ochered.PLAYER && !isFirstTime)
        {
            attackUI.SetActive(true);
        }
        else
        {
            isFirstTime = false;
        }
    }
    public void ShowWinPanel()
    {
        winPanel.SetActive(true);
    }
    public void ChangeHP(int newhp)
    {
        float temp = newhp/50.0f;
        Debug.Log(temp);
        hpBar.fillAmount = temp;
    }
    public void ShowDiceRollResult(string result)
    {
        diceRollResultText.text = result;
        diceRollResultText.gameObject.SetActive(true);
        StartCoroutine(WaitToCloseDiceRoll());
    }
    public void ShowLosePanel()
    {
        losePanel.SetActive(true);
        attackUI.SetActive(false);
    }
    public void ViewAttackUI(bool isActive)
    {
        attackUI.SetActive(isActive);
    }
    public void BackToMenuButton()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void ShowEarnedGold(string t)
    {
        goldToEarnText.text = t;
        goldToEarnText.gameObject.SetActive(true);
    }
    public void ShowMoney(string m)
    {
        moneyBar.text = m;
    }
    public void SoundOn()
    {
        PlayerPrefs.SetString("Sound", "true");
        PlayerPrefs.Save();
        audioSource.Play();
    }
    public void SoundOff()
    {
        PlayerPrefs.SetString("Sound", "false");
        PlayerPrefs.Save();
        audioSource.Pause();
    }
    private IEnumerator WaitToCloseDiceRoll()
    {
        yield return new WaitForSeconds(1);
        diceRollResultText.gameObject.SetActive(false);
    }
}
