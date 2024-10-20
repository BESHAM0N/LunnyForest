using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class LoadLevelInstaller : MonoInstaller
{
    [SerializeField] private Button _levelOne;
    [SerializeField] private Button _levelTwo;

    public override void InstallBindings()
    {
        this.Container.BindInterfacesTo<LoadingOneLevelButton>().AsSingle().WithArguments(this._levelOne).NonLazy();
        this.Container.BindInterfacesTo<LoadingTwoLevelButton>().AsSingle().WithArguments(this._levelTwo).NonLazy();
    }
}
