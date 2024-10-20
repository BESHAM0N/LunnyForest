using System;
using UnityEngine.UI;
using Zenject;

public class LoadingTwoLevelButton : IInitializable, IDisposable
{
    private readonly Button _button;
    private readonly LoadingTwoLevel _loadingTwoLevel;

    public LoadingTwoLevelButton(Button button, LoadingTwoLevel loadingTwoLevel)
    {
        this._button = button;
        this._loadingTwoLevel = loadingTwoLevel;
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
        this._loadingTwoLevel.LoadLevel();
    }
}
