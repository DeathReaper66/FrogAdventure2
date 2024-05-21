using UnityEngine;

public class ActivateCutSceneFrame : MonoBehaviour
{
    [SerializeField] private GameObject _player;
    [SerializeField] private GameObject[] _buttons;
    [SerializeField] private float _timerValue = 10f;
    private float _timer;
    private bool _buttonsActive = true;

    private void Update()
    {
        if (_buttonsActive)
            Activate();

        _timer += Time.deltaTime;

        if (_timer >= _timerValue && _player.GetComponent<PlayerController>().enabled)
        {
            _player.GetComponent<PlayerController>().enabled = false;
            _player.GetComponent<MobilePlayerController>().enabled = true;

            Deactivate();
        }
        else if (_timer >= _timerValue && _player.GetComponent<MobilePlayerController>().enabled)
        {
            _player.GetComponent<PlayerController>().enabled = true;
            _player.GetComponent<MobilePlayerController>().enabled = false;

            Deactivate();
        }
    }

    private void Deactivate()
    {
        foreach (GameObject button in _buttons)
            button.SetActive(true);

        gameObject.SetActive(false);
    }

    private void Activate()
    {
        foreach (GameObject button in _buttons)
            button.SetActive(false);

        if (_player.GetComponent<MobilePlayerController>().enabled)
        {
            _player.GetComponent<MobilePlayerController>().enabled = false;
            _player.GetComponent<PlayerController>().enabled = true;
        }
        else if (_player.GetComponent<PlayerController>().enabled)
        {
            _player.GetComponent<PlayerController>().enabled = false;
            _player.GetComponent<MobilePlayerController>().enabled = true;
        }

        _buttonsActive = false;
    }
}
