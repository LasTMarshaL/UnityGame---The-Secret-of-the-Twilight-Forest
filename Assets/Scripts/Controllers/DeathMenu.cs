using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathMenu : MonoBehaviour // This class shows death menu after player is dead.
{
    [SerializeField] private GameObject _menusGroup;

    [SerializeField] private GameSaver _gameSaver;

    private void Start()
    {
        Time.timeScale = 0;
        PauseLogic.ChangePauseState(true);
    }
    /// <summary>
    /// Sends player to the main menu.
    /// </summary>
    public void BackToMenu()
    {
        PauseLogic.ChangePauseState(true);
        Time.timeScale = 1;

        _menusGroup.SetActive(false);

        SceneManager.LoadScene("MainMenu");
    }

    /// <summary>
    /// Loads the last player's save.
    /// </summary>
    public void LastSave()
    {
        PauseLogic.ChangePauseState(false);
        Time.timeScale = 1;

        _menusGroup.SetActive(false);

        SceneManager.LoadScene("Level1");

        _gameSaver.LoadGame();
    }
}
