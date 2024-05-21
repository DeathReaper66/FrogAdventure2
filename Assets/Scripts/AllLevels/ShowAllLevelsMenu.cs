using UnityEngine;

public class ShowAllLevelsMenu : MonoBehaviour
{
    [SerializeField] private GameObject _allLevelsMenuButton;
    [SerializeField] private GameObject _allLevelsMenu;

    private void Awake()
    {
        if (PlayerPrefs.GetInt("NumberOfScene") != 0)
        {
            _allLevelsMenuButton.SetActive(true);
            _allLevelsMenu.SetActive(false);
        }
    }

    public void ShowButton()
    {
        _allLevelsMenu.SetActive(true);
    }

    public void CloseButton()
    {
        _allLevelsMenu.GetComponent<Animator>().SetTrigger("OnClick");
    }

    public void Close()
    {
        _allLevelsMenu.SetActive(false);
    }
}
