using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ RequireComponent(typeof(NachalnikIGRI))]
public class YstanovshikSlyshatelei : MonoBehaviour
{
    [SerializeField] private VodonosKameri component;

    private void Awake()
    {
        NachalnikIGRI nachalnikIgri = GetComponent<NachalnikIGRI>();
        IGameListener[] listeners = GetComponentsInChildren<IGameListener>();

        foreach (var listener in listeners)
        {
            nachalnikIgri.AddListener(listener);
        }
        nachalnikIgri.AddListener(component.GetComponent<IGameStartListener>());
    }
}
