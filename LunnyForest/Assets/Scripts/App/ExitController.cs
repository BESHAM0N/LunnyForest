using UnityEngine;
using Zenject;

public class ExitController : ITickable
{
    private readonly ExitGame _exitGame;

    public ExitController(ExitGame exitGame)
    {
        this._exitGame = exitGame;
    }

    void ITickable.Tick()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            this._exitGame.Finish();
        }
    }
}
