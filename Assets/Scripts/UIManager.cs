using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private BattleManager battleManager;
    [SerializeField] private GameObject inGameUI;
    [SerializeField] private GameObject attackUI;
    private void Awake()
    {
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
}
