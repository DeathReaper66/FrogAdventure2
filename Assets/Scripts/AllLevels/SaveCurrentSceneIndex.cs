using UnityEngine;
using UnityEngine.SceneManagement;

public class SaveCurrentSceneIndex : MonoBehaviour
{
    private void Awake()
    {
        if (PlayerPrefs.GetInt("NumberOfScene") <= SceneManager.GetActiveScene().buildIndex)
            PlayerPrefs.SetInt("NumberOfScene", SceneManager.GetActiveScene().buildIndex);
    }
}
