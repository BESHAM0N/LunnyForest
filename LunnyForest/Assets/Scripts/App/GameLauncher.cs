using UnityEngine.SceneManagement;

public sealed class GameLauncher
{
    public void StartGame()
    {
        SceneManager.LoadScene("MainScene");
    }
}
