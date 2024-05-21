using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuButtons : MonoBehaviour
{
    [SerializeField] private GameObject _exitSleepPanel;
    [SerializeField] private GameObject _continueSleepPanel;
    [SerializeField] private GameObject _newGameSleepPanel;

    public void NewGameButton()
    {
        _newGameSleepPanel.SetActive(true);
    }
    public void ContinueGameButton()
    {
        _continueSleepPanel.SetActive(true);
    }
    public void ExitGameButton()
    {
        _exitSleepPanel.SetActive(true);
    }
    public void ExitGame()
    {
        Application.Quit();
    }

    public void ContinueGame()
    {
        SceneManager.LoadScene(PlayerPrefs.GetInt("CurrentScene"));   
    }

    public void NewGame()
    {
        SwitchController.MobileControllerEnable = true;
        PlayerPrefs.SetInt("Coin", 0);
        PlayerPrefs.SetInt("Checkpoint", 0);
        PlayerPrefs.SetInt("NumberOfScene", 0);
        SceneManager.LoadScene(1);
    }
}
