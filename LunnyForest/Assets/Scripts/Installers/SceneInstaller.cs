using UnityEngine;
using Zenject;

public sealed  class SceneInstaller : MonoInstaller
{
    [SerializeField]
    private Shop _shop;

    public override void InstallBindings()
    {
        this.Container
            .Bind<Shop>()
            .FromInstance(this._shop)
            .AsSingle();
    }
    
}
