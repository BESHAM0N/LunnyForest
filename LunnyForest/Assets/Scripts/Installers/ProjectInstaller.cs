using UnityEngine;
using Zenject;

[CreateAssetMenu(fileName = "ProjectInstaller", menuName = "Installers/New ProjectInstaller")]
public sealed class ProjectInstaller : ScriptableObjectInstaller
{
    public override void InstallBindings()
    {
        this.Container.Bind<ExitGame>().AsSingle();
        this.Container.Bind<GameLauncher>().AsSingle();
        this.Container.Bind<LoadingOneLevel>().AsSingle();
        this.Container.Bind<LoadingTwoLevel>().AsSingle();
    }
}
