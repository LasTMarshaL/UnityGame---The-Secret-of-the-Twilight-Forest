using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathMenu : MonoBehaviour
{
    [SerializeField] private GameObject _menusGroup;

    [SerializeField] private GameSaver _gameSaver;

    private void Start()
    {
        Time.timeScale = 0;
        PauseLogic.ChangePauseState(true);
    }

    public void BackToMenu()
    {
        PauseLogic.ChangePauseState(true);
        Time.timeScale = 1;

        _menusGroup.SetActive(false);

        SceneManager.LoadScene("MainMenu");
    }

    public void LastSave()
    {
        PauseLogic.ChangePauseState(false);
        Time.timeScale = 1;

        _menusGroup.SetActive(false);

        SceneManager.LoadScene("Level1");

        _gameSaver.LoadGame();
    }
}
