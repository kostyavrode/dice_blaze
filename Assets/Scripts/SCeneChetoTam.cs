using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class SCeneChetoTam : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.Bind<NachalnikBitvi>().FromComponentInHierarchy().AsSingle();
        Container.Bind<NachalnikUI>().FromComponentInHierarchy().AsSingle();
    }
}
