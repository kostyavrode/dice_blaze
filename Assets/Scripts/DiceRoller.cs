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
                transform.DORotate(new Vector3(-360f, 0, 0), 0.01f);
                break;
            case 2:
                transform.DORotate(new Vector3(-180f, 0, 0), 0.01f);
                break;
            case 3:
                transform.DORotate(new Vector3(-360, 0, -90), 0.01f);
                break;
            case 4:
                transform.DORotate(new Vector3(-90, 0, -90), 0.01f);
                break;
            case 5:
                transform.DORotate(new Vector3(-180, 0, -90), 0.01f);
                break;
            case 6:
                transform.DORotate(new Vector3(90, 0, 0), 0.01f);
                break;
        }
        transform.DOShakeRotation(3).OnComplete(DisableSelf);
        
    }
    private void DisableSelf()
    {
        head.SetActive(false);
    }
}
