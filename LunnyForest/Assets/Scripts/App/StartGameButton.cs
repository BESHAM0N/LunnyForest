using System;
using UnityEngine.UI;
using Zenject;

public class StartGameButton : IInitializable, IDisposable
{
    private readonly Button _button;
    private readonly GameLauncher _gameLauncher;

    public StartGameButton(Button button, GameLauncher gameLauncher)
    {
        this._button = button;
        this._gameLauncher = gameLauncher;
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
        this._gameLauncher.StartGame();
    }
    
    
}