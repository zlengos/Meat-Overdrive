using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : ButtonSelecter
{
    private const string LEVEL_SELECTER = "LevelSelecter";
    private const string SETTINGS_SCENE = "Settings";

    public void GoToLevelSelecter() => SceneManager.LoadScene(LEVEL_SELECTER);

    public void GoToSettings() => SceneManager.LoadScene(SETTINGS_SCENE);

    public void Exit() => Application.Quit();

    private void LateUpdate()
    {
        if (Input.GetKey(KeyCode.Escape))
            Exit();
    }
}
