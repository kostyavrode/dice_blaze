using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class CameraFollower : MonoBehaviour, IGameStartListener
{
    [SerializeField] private float trackingSpeed = 2f;
    [SerializeField] private float offsetZ = 3f;
    [SerializeField] private float offsetY = 3f;
    [SerializeField] private float offsetX = 0.66f;
    [SerializeField] private Transform target;
    [SerializeField] private Transform secondPlace;
    [SerializeField] private Transform startPlace;
    private bool isSeconsShowed;
    private void Start()
    {
        transform.DOMove(startPlace.position, 2f).SetEase(Ease.Flash);
        transform.DORotateQuaternion(startPlace.rotation, 1f);
    }
    public void StartShow()
    {
        Debug.Log("Camera MOVE");
        transform.DOMove(secondPlace.position, 0.3f).SetEase(Ease.Flash).OnComplete(StopShow);
        transform.DORotateQuaternion(secondPlace.rotation, 1f);
    }
    public void StopShow()
    {
        StartCoroutine(ShowBananaDelay());
    }
    public void GameCam()
    {
        transform.DOMove(secondPlace.position, 0.3f).OnComplete(StartFollow);
    }
    public void StartFollow()
    {
        isSeconsShowed = true;
    }
    private void Update()
    {
        if (isSeconsShowed)
        {
            Vector3 tempPosition = new Vector3(target.position.x + offsetX, target.position.y + offsetY, target.position.z - offsetZ);
            transform.position = Vector3.Lerp(transform.position, tempPosition, trackingSpeed * Time.deltaTime);
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