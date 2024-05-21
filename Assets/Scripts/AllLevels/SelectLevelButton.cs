using UnityEngine;

public class SelectLevelButton : MonoBehaviour
{
    [SerializeField] private GameObject _lockImage;
    [SerializeField] private SleepAndAwakePanel _sleepPanel;
    [SerializeField] private int _numberOfScene;

    private void FixedUpdate()
    {
        if (PlayerPrefs.GetInt("NumberOfScene") >= _numberOfScene)
            _lockImage.SetActive(false);
        else
            _lockImage.SetActive(true);
    }

    public void LoadSelectedScene()
    {
        _sleepPanel.gameObject.SetActive(true);
        _sleepPanel._sceneIndex = _numberOfScene;
        PlayerPrefs.SetInt("Checkpoint", 0);
    }
}
