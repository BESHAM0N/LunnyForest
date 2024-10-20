using System;
using UnityEngine.UI;
using Zenject;

public class ExitGameButton : IInitializable, IDisposable
{
     private readonly Button _button;
     private readonly ExitGame _exitGame;

     public ExitGameButton(Button button, ExitGame exitGame)
     {
         this._button = button;
         this._exitGame = exitGame;
     }

     void IInitializable.Initialize()
     {
         this._button.onClick.AddListener(this.OnButtonClicked);
     }

     void IDisposable.Dispose()
     {
         this._button.onClick.RemoveListener(this.OnButtonClicked);
     }

     private void OnButtonClicked()
     {
         this._exitGame.Finish();
     }
}
