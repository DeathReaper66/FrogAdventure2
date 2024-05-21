using UnityEngine;

public class EnableMechanic : MonoBehaviour
{
    [SerializeField] private GameObject _mechanicButton;
    [SerializeField] private GameObject _tutorial;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
            EnableTutorial();
    }

    public void EnableTutorial()
    {
        _mechanicButton.SetActive(true);
        _tutorial.SetActive(true);
        Destroy(gameObject);
    }
}
