using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using Photon.Pun;

public class PlayerHealthMultiplayer : MonoBehaviour
{
    private int _health = 5;
    private Animator _anim;
    private Rigidbody2D _rigidbody2D;
    private PhotonView _view;

    private void Awake()
    {
        _view = GetComponent<PhotonView>();

        if (_view.IsMine)
        {
            Physics2D.IgnoreLayerCollision(8, 10, false);
            _anim = GetComponent<Animator>();
            _rigidbody2D = GetComponent<Rigidbody2D>();
        }
    }

    public void TakeDamage(int _damage)
    {
        if (_view.IsMine)
        {
            _health -= _damage;

            StartCoroutine(Invulnerability());

            if (_health <= 0)
                Death();
            else
                _anim.SetTrigger("hurt");
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (_view.IsMine)
        {
            if (collision.collider.tag == "Bullet")
            {
                TakeDamage(1);
                Destroy(collision.collider.gameObject);
            }
        }
    }

    private IEnumerator Invulnerability()
    {
        Physics2D.IgnoreLayerCollision(8, 10, true);
        yield return new WaitForSeconds(1f);
        Physics2D.IgnoreLayerCollision(8, 10, false);
    }

    public void Death()
    {
        _anim.SetTrigger("die");
        _rigidbody2D.gravityScale = 0f;
    }

    //for die animation
    public void EnableDeathMenu()
    {
        Destroy(gameObject);
    }
}
