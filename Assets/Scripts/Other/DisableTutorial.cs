using UnityEngine;

public class DisableTutorial : MonoBehaviour
{
    [SerializeField] private GameObject _tutorial;

    public void ButtonForDisable()
    {
        _tutorial.GetComponent<Animator>().SetTrigger("Disable");
    }

    public void Disable()
    {
        gameObject.SetActive(false);
    }
}
