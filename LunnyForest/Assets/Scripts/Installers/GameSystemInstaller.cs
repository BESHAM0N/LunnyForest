using UnityEngine;
using Zenject;

[CreateAssetMenu(fileName = "GameSystemInstaller", menuName = "Installers/New GameSystemInstaller")] 
public class GameSystemInstaller : ScriptableObjectInstaller
{
   public override void InstallBindings()
   {
      this.Container.BindInterfacesTo<ExitController>().AsCached();
   }
}
