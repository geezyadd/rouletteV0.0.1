using UnityEngine;
using Zenject;

public class LoadingInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.Bind<StartMenuButtonsClickHandler>().FromComponentInHierarchy().AsSingle();
        Container.BindInterfacesAndSelfTo<StartMenuSystem>().AsSingle();
    }
}
