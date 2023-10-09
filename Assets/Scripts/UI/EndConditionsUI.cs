using UnityEngine;
using UnityEngine.SceneManagement;

public class EndConditionsUI : MonoBehaviour
{
    public static EndConditionsUI Instance { get; private set; }

    [SerializeField] private GameObject _caughtPanel;
    [SerializeField] private GameObject _winPanel;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void ShowCaughtPanel()
    {
        _caughtPanel.SetActive(true);
    }

    public void ShowWinPanel()
    {
        _winPanel.SetActive(true);
    }

    #region Buttons

    public void RestartGame()
    {
        GameRestartManager.isRestart = true;
        SceneManager.LoadScene(1);
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void BackToMenu()
    {
        SceneManager.LoadScene(0);
    }

    #endregion
}
