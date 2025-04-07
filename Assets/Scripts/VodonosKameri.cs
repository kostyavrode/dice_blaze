using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.Serialization;

public class VodonosKameri : MonoBehaviour, IGameStartListener
{
    [FormerlySerializedAs("trackingSpeed")] [SerializeField] private float skorostPresledovaniya = 2f;

    public string goal;
    
    [FormerlySerializedAs("offsetZ")] [SerializeField] private float xhetoZ = 3f;
    [FormerlySerializedAs("offsetY")] [SerializeField] private float chetoY = 3f;
    [FormerlySerializedAs("offsetX")] [SerializeField] private float chetoX = 0.66f;
    
    
    [FormerlySerializedAs("target")] [SerializeField] private Transform cel;
    [FormerlySerializedAs("secondPlace")] [SerializeField] private Transform vtoroeMesto;
    [FormerlySerializedAs("startPlace")] [SerializeField] private Transform nachalo;
    private bool isSeconsShowed;

    private void Awake()
    {
        goal=PlayerPrefs.GetString("goal");
    }

    private void Start()
    {
        transform.DOMove(nachalo.position, 2f).SetEase(Ease.Flash);
        transform.DORotateQuaternion(nachalo.rotation, 1f);
    }
    public void StartShow()
    {
        Debug.Log("Camera MOVE");
        transform.DOMove(vtoroeMesto.position, 0.3f).SetEase(Ease.Flash).OnComplete(StopShow);
        transform.DORotateQuaternion(vtoroeMesto.rotation, 1f);
    }
    public void StopShow()
    {
        StartCoroutine(ShowBananaDelay());
    }
    public void GameCam()
    {
        transform.DOMove(vtoroeMesto.position, 0.3f).OnComplete(StartFollow);
    }
    public void StartFollow()
    {
        isSeconsShowed = true;
    }
    private void Update()
    {
        if (isSeconsShowed)
        {
            Vector3 tempPosition = new Vector3(cel.position.x + chetoX, cel.position.y + chetoY, cel.position.z - xhetoZ);
            transform.position = Vector3.Lerp(transform.position, tempPosition, skorostPresledovaniya * Time.deltaTime);
        }
    }
    private IEnumerator ShowBananaDelay()
    {
        yield return new WaitForSeconds(0.3f);
        GameCam();
    }

    void IGameStartListener.OnGameStarted()
    {
        StartShow();
    }
}