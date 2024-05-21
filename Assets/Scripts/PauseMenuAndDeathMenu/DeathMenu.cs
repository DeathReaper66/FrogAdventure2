using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathMenu : MonoBehaviour
{
    [SerializeField] private GameObject _deathMenu;
    [SerializeField] private GameObject _mecanicsButtons;
    [SerializeField] private GameObject _sleepPanelToMainMenu;
    [SerializeField] private GameObject _restartSleepPanel;

    public void ShowDeathMenu()
    {
        Time.timeScale = 0.001f;
        _deathMenu.SetActive(true);
        _mecanicsButtons.SetActive(false);
    }

    public void RestartScene()
    {
        _deathMenu.SetActive(false);
        Time.timeScale = 1f;
        _restartSleepPanel.SetActive(true);
    }

    public void GoToMainMenu()
    {
        _deathMenu.SetActive(false);
        Time.timeScale = 1f;
        _sleepPanelToMainMenu.SetActive(true);
    }
}
