using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SleepAndAwakePanel : MonoBehaviour
{
    [SerializeField] private int _sceneIndex;
    public void DisableAwakePanel()
    {
        gameObject.SetActive(false);
    }

    public void SleepPanelLoadScene()
    {
        SceneManager.LoadScene(_sceneIndex);
    }
}
