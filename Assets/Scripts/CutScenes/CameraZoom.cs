using Cinemachine;
using UnityEngine;

public class CameraZoom : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera _camera;
    [SerializeField] private float _delay = 1f;
    [SerializeField] private float _cameraZoomValue;
    private bool _isActive;
    private bool _decrease;
    private bool _increase;

    private void FixedUpdate()
    {
        if (_isActive)
        {
            if (_increase)
                _camera.m_Lens.OrthographicSize += _delay / 1000;
            else if (_decrease)
                _camera.m_Lens.OrthographicSize -= _delay / 1000;

            if (_camera.m_Lens.OrthographicSize >= _cameraZoomValue && _increase)
            {
                _isActive = false;
                _increase = false;
                gameObject.SetActive(false);
            }
            else if (_camera.m_Lens.OrthographicSize <= _cameraZoomValue && _decrease)
            {
                _isActive = false;
                _decrease = false;
                gameObject.SetActive(false);
            }

        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            if (_camera.m_Lens.OrthographicSize < _cameraZoomValue)
                _increase = true;
            else if (_camera.m_Lens.OrthographicSize > _cameraZoomValue)
                _decrease = true;

            _isActive = true;
            GetComponent<BoxCollider2D>().enabled = false;
        }
    }
}
