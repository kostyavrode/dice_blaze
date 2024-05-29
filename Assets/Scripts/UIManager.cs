using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
public class UIManager : MonoBehaviour
{
    public static UIManager instance;
    [SerializeField] private BattleManager battleManager;
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
        BattleManager.onTurnSwitch += CheckAttackUI;
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
        BattleManager.onTurnSwitch -= CheckAttackUI;
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
        GameManager.instance.StartGame();
        inGameUI.SetActive(true);
    }
    public void LegAttackButton()
    { 
       battleManager.Attack(AttackType.LEG);
    }
    public void SwordAttackButton()
    {
        battleManager.Attack(AttackType.SWORD);
    }
    private void CheckAttackUI()
    {
        if (battleManager.Turn==Turn.PLAYER && !isFirstTime)
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
