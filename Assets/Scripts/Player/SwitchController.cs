using UnityEngine;

public class SwitchController : MonoBehaviour
{
    [SerializeField] private GameObject[] UIs;
    private bool _isActive = false;
    public static bool MobileControllerEnable = true;

    private void Awake()
    {
        if (!MobileControllerEnable)
        {
            _isActive = true;
            GetComponent<PlayerController>().enabled = true;
            GetComponent<MobilePlayerController>().enabled = false;
        }
    }

    public void Switch()
    {
        MobileControllerEnable = !MobileControllerEnable;
    }

    private void LateUpdate()
    {
        if (Input.GetKeyDown(KeyCode.Y) && !_isActive)
        {
            Switch();

            gameObject.GetComponent<PlayerController>().enabled = true;
            gameObject.GetComponent<MobilePlayerController>().enabled = false;

            foreach (GameObject UI in UIs)
                UI.SetActive(false);

            _isActive = true;
        }
        else if (Input.GetKeyDown(KeyCode.Y) && _isActive)
        {
            Switch();

            gameObject.GetComponent<PlayerController>().enabled = false;
            gameObject.GetComponent<MobilePlayerController>().enabled = true;

            foreach (GameObject UI in UIs)
                UI.SetActive(true);

            _isActive = false;
        }
    }
}
