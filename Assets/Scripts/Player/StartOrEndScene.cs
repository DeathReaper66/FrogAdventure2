using Cinemachine;
using UnityEngine;

public class StartOrEndScene : MonoBehaviour
{
    [SerializeField] private GameObject _sleepPanel;
    [SerializeField] private GameObject _triggerCollider;
    [SerializeField] private GameObject _mecanicsButtons;
    [SerializeField] private CinemachineVirtualCamera _cinemachineVirtualCamera;
    private MobilePlayerController _mobilePlayerController;
    private bool _isEnter;

    private void Awake()
    {
        _mobilePlayerController = GetComponent<MobilePlayerController>();
    }
    private void FixedUpdate()
    {
        if (_isEnter)
            transform.Translate(0.11f, 0, 0);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Start")
        {
            _mecanicsButtons.SetActive(false);
            _isEnter = true;
            _cinemachineVirtualCamera.Follow = null;
            _mobilePlayerController.Anim.SetTrigger("startOrEnd");
        }
        if (collision.tag == "End")
        {
            PlayerPrefs.SetInt("Coin", Coin.NumberOfCoin);
            _mecanicsButtons.SetActive(false);
            _isEnter = true;
            _cinemachineVirtualCamera.Follow = null;
            _mobilePlayerController.Anim.SetTrigger("startOrEnd");
            _sleepPanel.SetActive(true);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        _isEnter = false;
        _mecanicsButtons.SetActive(true);
        _triggerCollider.SetActive(false);
        _cinemachineVirtualCamera.Follow = gameObject.transform;
    }
}
