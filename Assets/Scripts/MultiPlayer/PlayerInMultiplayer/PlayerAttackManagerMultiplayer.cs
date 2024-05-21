using Photon.Pun;
using UnityEngine;

public class PlayerAttackManagerMultiplayer : MonoBehaviour
{
    [SerializeField] private GameObject _bullet;
    [SerializeField] private Transform _bulletPoint;
    [SerializeField] private float _attackCooldown = 3f;
    private float _attackTimer = 0f;

    private PhotonView _view;

    private void Awake()
    {
        _view = GetComponent<PhotonView>();
    }

    private void FixedUpdate()
    {
        if (_view.IsMine)
        {
            _attackTimer += Time.deltaTime;

            if (Input.GetKey(KeyCode.Q))
                Attack();
        }
    }

    public void Attack()
    {
        if (_attackTimer >= _attackCooldown && _view.IsMine)
        {
            _attackTimer = 0f;
            GameObject _newBullet = Instantiate(_bullet);
            _newBullet.SetActive(true);

            if (gameObject.transform.localScale.x < 0)
                _newBullet.transform.localScale = new Vector3(-_bulletPoint.localScale.x, _bulletPoint.localScale.y, _bulletPoint.localScale.z);
            else
                _newBullet.transform.localScale = _bulletPoint.localScale;

            _newBullet.transform.position = _bulletPoint.position;
            _newBullet.transform.rotation = _bulletPoint.rotation;
        }
    }
}
