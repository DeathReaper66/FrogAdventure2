using UnityEngine;

public class MultiPlayerButton : MonoBehaviour
{
    [SerializeField] private GameObject _multiplayButton;

    private void Awake()
    {
        if (PlayerPrefs.GetInt("Multiplay") != 0)
            _multiplayButton.SetActive(true);
    }
}
