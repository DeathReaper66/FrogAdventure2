using Cinemachine;
using UnityEngine;

public class CutSceneAtPoints : MonoBehaviour
{
    [SerializeField] private GameObject _wall;
    [SerializeField] private CinemachineVirtualCamera _camera;
    [SerializeField] private Transform[] _points;
    [SerializeField] private float _frame = 0.05f;
    private bool _onTrigger = false;
    private bool _isFirst = true;
    private bool _onPoint = false;

    private void FixedUpdate()
    {
        if (_onTrigger)
        {
            if (_isFirst)
            {
                if (!_onPoint)
                {
                    _camera.transform.position = Vector3.MoveTowards(_camera.transform.position, _points[0].position, _frame);

                    if (_camera.transform.position == _points[0].position)
                        _onPoint = true;
                }
                else
                {
                    _camera.transform.position = Vector3.MoveTowards(_camera.transform.position, _points[1].position, _frame);

                    if (_camera.transform.position == _points[1].position)
                    {
                        _onPoint = false;
                        _isFirst = false;
                    }
                }
            }
            else if (!_isFirst)
            {
                if (!_onPoint)
                {
                    _camera.transform.position = Vector3.MoveTowards(_camera.transform.position, _points[2].position, _frame);

                    if (_camera.transform.position == _points[2].position)
                        _onPoint = true;
                }
                else if (_onPoint)
                {
                    _camera.transform.position = Vector3.MoveTowards(_camera.transform.position, _points[3].position, _frame);

                    if (_camera.transform.position.x == _points[3].position.x)
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
            _wall.SetActive(true);
        }
    }
}
