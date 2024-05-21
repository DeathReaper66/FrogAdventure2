using Photon.Pun;
using UnityEngine;

public class PlayerBulletMultiplayer : MonoBehaviour
{
    private float _lifeTime = 3f;
    private int _damage = 1;
    private float _lifeTimeTimer = 0f;
    private PhotonView _view;

    private void Awake()
    {
        _view = GetComponent<PhotonView>();
    }

    private void FixedUpdate()
    {
        if (_view.IsMine)
        {
            _lifeTimeTimer += Time.deltaTime;

            if (_lifeTimeTimer >= _lifeTime)
            {
                _lifeTimeTimer = 0f;
                Destroy(gameObject);
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (_view.IsMine)
        {
            if (collision.collider.tag == "Player")
            {
                collision.collider.GetComponent<PlayerHealthMultiplayer>().TakeDamage(_damage);
                Destroy(gameObject);
            }
            else if (collision.collider.tag == "Ground")
                Destroy(gameObject);
        }
    }
}
