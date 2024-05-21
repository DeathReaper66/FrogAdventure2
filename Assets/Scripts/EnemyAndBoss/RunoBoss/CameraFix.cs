using Cinemachine;
using UnityEngine;

public class CameraFix : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera _camera;
    [SerializeField] private GameObject _groundCollider;
    [SerializeField, Range(0.1f, 1f)] private float _speed = 0.1f;
    [SerializeField] private bool _isWork = true; 
    private bool _onTrigger = false;

    private void FixedUpdate()
    {
        if (_isWork)
        {
            if (_onTrigger)
            {
                _camera.transform.position = Vector3.MoveTowards(_camera.transform.position, transform.position, _speed);

                if (_camera.transform.position == transform.position)
                {
                    _groundCollider.SetActive(true);
                    gameObject.SetActive(false);
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            _camera.Follow = null;
            _onTrigger = true;
        }
    }
}
