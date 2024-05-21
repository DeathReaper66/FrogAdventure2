using UnityEngine;
using UnityEngine.SceneManagement;

public class SleepAndAwakePanel : MonoBehaviour
{
    [SerializeField] public int _sceneIndex;
    public void DisableAwakePanel()
    {
        gameObject.SetActive(false);
    }

    public void SleepPanelLoadScene()
    {
        SceneManager.LoadScene(_sceneIndex);
    }
}
