using Photon.Pun;
using UnityEngine;

public class MultiplayerCamera : MonoBehaviour
{
    [SerializeField] private Transform _followingTarget;
    [SerializeField, Range(0f, 1f)] private float _parralaxStranght;
    private Vector3 _targetPreviousPosition;
    private PhotonView _view;

    private void Awake()
    {
        _view = GetComponent<PhotonView>();
        _targetPreviousPosition = _followingTarget.position;
    }

    private void FixedUpdate()
    {
        if (_view.IsMine)
        {
            var deltaVector3 = _followingTarget.position - _targetPreviousPosition;
            _targetPreviousPosition = _followingTarget.position;
            transform.position += deltaVector3 * _parralaxStranght;
        }
    }
}
