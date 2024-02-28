using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private GameObject _pauseMenu;
    [SerializeField] private GameObject _mecanicsButtons;
    [SerializeField] private GameObject _sleepPanelToMainMenu;

    public void PauseMenuButton()
    {
        Time.timeScale = 0.001f;
        _pauseMenu.SetActive(true);
        _mecanicsButtons.SetActive(false);
    }

    public void ClosePauseMenuButton()
    {
        _pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        _mecanicsButtons.SetActive(true);
    }

    public void GoToMainMenu()
    {
        _pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        _sleepPanelToMainMenu.SetActive(true);
    }
}
