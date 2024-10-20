using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class MenuSceneInstaller : MonoInstaller
{
    [SerializeField] private Button _startButton;
    [SerializeField] private Button _exitButton;

    public override void InstallBindings()
    {
        this.Container.BindInterfacesTo<StartGameButton>().AsSingle().WithArguments(this._startButton).NonLazy();
        this.Container.BindInterfacesTo<ExitGameButton>().AsSingle().WithArguments(this._exitButton).NonLazy();
    }
}
