using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class UIManager : MonoBehaviour
{
    public static UIManager instance;
    [SerializeField] private BattleManager battleManager;
    [SerializeField] private GameObject inGameUI;
    [SerializeField] private GameObject attackUI;
    [SerializeField] private Image hpBar;
    [SerializeField] private TMP_Text diceRollResultText;
    private void Awake()
    {
        instance = this;
        BattleManager.onTurnSwitch += CheckAttackUI;
    }
    private void OnDestroy()
    {
        BattleManager.onTurnSwitch -= CheckAttackUI;
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
        if (battleManager.Turn==Turn.PLAYER)
        {
            attackUI.SetActive(true);
        }
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
    private IEnumerator WaitToCloseDiceRoll()
    {
        yield return new WaitForSeconds(1);
        diceRollResultText.gameObject.SetActive(false);
    }
}
