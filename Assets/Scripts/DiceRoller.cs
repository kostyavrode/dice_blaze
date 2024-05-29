using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class DiceRoller : MonoBehaviour
{
    public GameObject head;
    public void Roll(int rand)
    {
        switch(rand)
        {
            case 1:
                transform.DORotate(new Vector3(-90f, 0, 0), 0.01f);
                break;
            case 2:
                transform.DORotate(new Vector3(0, 0, -180), 0.01f);
                break;
            case 3:
                transform.DORotate(new Vector3(0, 0, -90), 0.01f);
                break;
            case 4:
                transform.DORotate(new Vector3(0, 0, -270), 0.01f);
                break;
            case 5:
                transform.DORotate(new Vector3(0, 0, 0), 0.01f);
                break;
            case 6:
                transform.DORotate(new Vector3(-270, 0, 0), 0.01f);
                break;
        }
        transform.DOShakeRotation(3).OnComplete(DisableSelf);
        
    }
    private void DisableSelf()
    {
        head.SetActive(false);
    }
}
