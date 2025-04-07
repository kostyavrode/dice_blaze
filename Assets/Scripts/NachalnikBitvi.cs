using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Serialization;

public enum Ochered
{
    PLAYER = 0,
    ENEMY=1
}
public enum KakBit
{
    LEG=0,
    SWORD=1
}

public class NachalnikBitvi : MonoBehaviour, IGameStartListener, IGameFinishedListener
{

    public int rega;
    
    public static Action ChybrikShodil;
    public static Action<Protivnik> ZelebobaPomenyalsya;

    public float f;
    
    public static Action HodPomenyalsya;
    public static Action VseZelebobiYmerli;
    
    [FormerlySerializedAs("levelManager")] [SerializeField] private NachalnikYrovnei nachalnikYrovnei;
    private Ochered _ochered;
    
    private bool isBattleStarted;
    private Protivnik protivnik;
    
    private bool zhdemNanesenieYrona;
    private bool protivnikPomenyalsya;
    private int resultatBroska;
    public Ochered Ochered
    {
        get { return _ochered; }
    }
    private void Awake()
    {
        f=PlayerPrefs.GetFloat("money");
        if (!nachalnikYrovnei)
        {
            nachalnikYrovnei = GetComponent<NachalnikYrovnei>();
        }
        Igrok.onDestinationArrived += ContinueFight;
        VseZelebobiYmerli += EndBattle;
        Igrok.onPlayerDead += StopGamePlayerDead;
    }
    private void OnDisable()
    {
        VseZelebobiYmerli -= EndBattle;
        Igrok.onDestinationArrived -= ContinueFight;
        Igrok.onPlayerDead -= StopGamePlayerDead;
    }
    private void Update()
    {
        if (zhdemNanesenieYrona)
        {
            if (IsAnimationPlaying("idle"))
            {
                if (_ochered == Ochered.ENEMY)
                {
                    nachalnikYrovnei.igrok.ReceiveDamage(nachalnikYrovnei.currentProtivnik.GetDamage() * resultatBroska);
                    Debug.Log("END ENEMY TURN");
                    _ochered = Ochered.PLAYER;
                    nachalnikYrovnei.igrok.StartTurn();
                    zhdemNanesenieYrona = false;
                    HodPomenyalsya?.Invoke();
                    SwitchTurn();
                }
                else
                {
                    protivnik.ReceiveDamage(nachalnikYrovnei.igrok.GetAttackParameters() * resultatBroska);
                    CheckEnemy();
                    CheckDistanceBetweenEnemy();
                    if (protivnik && !protivnikPomenyalsya)
                    {
                        _ochered = Ochered.ENEMY;
                        Debug.Log("END PLAYER TURN");
                        SwitchTurn();
                    }
                    
                    zhdemNanesenieYrona = false;
                    
                }
            }
        }
    }
    private void StopGamePlayerDead()
    {
        NachalnikIGRI.onEndGame?.Invoke();
        //enemyInBattle = null;
        zhdemNanesenieYrona = false;
        NachalnikUI.instance.ViewAttackUI(false);
        //UIManager.instance.ShowWinPanel();
        //InfoController.instance.ReceiveMoney(UnityEngine.Random.Range(0, 1));
        NachalnikUI.instance.ShowLosePanel();
        NachalnikUI.instance.isGameEnd = true;
    }
    void IGameStartListener.OnGameStarted()
    {
        CreateLevel();
        isBattleStarted = true;
        _ochered = Ochered.PLAYER;
        ChybrikShodil += SwitchTurn;
        ZelebobaPomenyalsya += ChangeEnemy;
        protivnik = nachalnikYrovnei.currentProtivnik;
        Debug.Log("Battle_Started");
        nachalnikYrovnei.igrok.MoveTo(protivnik.transform.position);
        StartCoroutine(WaitTostartBattle());
        NachalnikUI.instance.ViewAttackUI(false);
    }
    void IGameFinishedListener.OnGameFinished()
    {
        ChybrikShodil -= SwitchTurn;
        ZelebobaPomenyalsya -= ChangeEnemy;
    }
    public void Attack(KakBit type=KakBit.SWORD)
    {
        resultatBroska = Kost.instance.GetRoll();
        NachalnikUI.instance.ShowDiceRollResult(resultatBroska.ToString());
        if (_ochered==Ochered.PLAYER)
        {
            switch (type)
            {
                case KakBit.LEG:
                    {
                        nachalnikYrovnei.igrok.LegAttack();
                        break;
                    }
                case KakBit.SWORD:
                    {
                        nachalnikYrovnei.igrok.SwordAttack();
                        break;
                    }
            }
            StartCoroutine(Wait1Sec());
        }
        else
        {
            protivnik.animator.SetTrigger("attack");
            StartCoroutine(Wait1Sec());
        }
    }
    public void SwitchTurn()
    {
        if (_ochered==Ochered.PLAYER)
        {
            nachalnikYrovnei.igrok.isCanMakeTurn = true;
        }
        else
        {
            Attack();
        }
    }
    private void CreateLevel()
    {
        nachalnikYrovnei.SpawnEnemies();
    }
    private void CheckEnemy()
    {
        nachalnikYrovnei.GetCurrentEnemy();
    }
    private void ChangeEnemy(Protivnik en)
    {
        protivnik = en;
        Debug.Log("Enemy changed");
        protivnikPomenyalsya = true;
    }
    private bool IsAnimationPlaying(string animationName)
    {
        if (_ochered==Ochered.ENEMY)
        {
            var animatorStateInfo = protivnik.animator.GetCurrentAnimatorStateInfo(0);
            if (animatorStateInfo.IsName(animationName))
                return true;

            return false;
        }
        else
        {
            var animatorStateInfo = nachalnikYrovnei.igrok.animator.GetCurrentAnimatorStateInfo(0);
            if (animatorStateInfo.IsName(animationName))
                return true;

            return false;
        }   
    }
    private void EndBattle()
    {
        MovePlayerToReward();
        NachalnikUI.instance.ViewAttackUI(false);
        nachalnikYrovnei.igrok.PlayChestOpenAnim();
        NachalnikInformation.instance.ReceiveMoney(UnityEngine.Random.Range(8, 10));
    }
    private void CheckDistanceBetweenEnemy()
    {
        if (nachalnikYrovnei.currentProtivnik.transform.position.z- nachalnikYrovnei.igrok.transform.position.z>3)
        {
            nachalnikYrovnei.igrok.MoveTo(nachalnikYrovnei.currentProtivnik.transform.position);
        }
    }
    private void ContinueFight()
    {
        if (protivnik)
        {
            protivnikPomenyalsya = false;
            _ochered = Ochered.ENEMY;
            HodPomenyalsya?.Invoke();
            SwitchTurn();
        }
        else
        {
            nachalnikYrovnei.OpenReward();
        }
    }
    private void MovePlayerToReward()
    {
        nachalnikYrovnei.igrok.MoveTo(nachalnikYrovnei.finalDestination.position);
    }
    private IEnumerator Wait1Sec()
    {
        yield return new WaitForSeconds(0.5f);
        zhdemNanesenieYrona = true;
    }
    private IEnumerator WaitTostartBattle()
    {
        yield return new WaitForSeconds(2);
        SwitchTurn();
    }
}
