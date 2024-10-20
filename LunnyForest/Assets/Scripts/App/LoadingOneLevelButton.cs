using System;
using UnityEngine.UI;
using Zenject;

public class LoadingOneLevelButton : IInitializable, IDisposable
{
    private readonly Button _button;
    private readonly LoadingOneLevel _loadingOneLevel;

    public LoadingOneLevelButton(Button button, LoadingOneLevel loadingOneLevel)
    {
        this._button = button;
        this._loadingOneLevel = loadingOneLevel;
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
        this._loadingOneLevel.LoadLevel();
    }
}
