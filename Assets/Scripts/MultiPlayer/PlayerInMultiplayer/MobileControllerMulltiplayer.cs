using Photon.Pun;
using UnityEngine;

public class MobileControllerMulltiplayer : MonoBehaviour
{
    [Header("Params")]
    [SerializeField] private float _speed;
    private float _normalSpeed;
    [SerializeField] private float _jumpForce;
    private float _coyoteTimer;

    [Header("DashParams")]
    [SerializeField] float _dashPowerX;
    [SerializeField] float _dashPowerY;
    private float _dashCooldownTimer;

    [Header("OtherParams")]
    [SerializeField] private LayerMask _groundLayer;
    [SerializeField] private LayerMask _platformLayer;
    private Rigidbody2D _body;
    private BoxCollider2D _box;
    private PhotonView _view;
    public Animator Anim { get; private set; }

    private void Awake()
    {
        _body = GetComponent<Rigidbody2D>();
        Anim = GetComponent<Animator>();
        _box = GetComponent<BoxCollider2D>();
        _view = GetComponent<PhotonView>();

        _normalSpeed = _speed;
        _speed = 0f;
        _dashCooldownTimer = 0f;
        _coyoteTimer = 0f;
    }

    private void FixedUpdate()
    {
        if (_view.IsMine)
        {
            Move();

            _dashCooldownTimer += Time.deltaTime;

            if (!IsGrounded())
                _coyoteTimer += Time.deltaTime;
            else
                _coyoteTimer = 0f;

            if (IsGrounded())
                _body.gravityScale = 2f;

            Anim.SetBool("moving", _speed != 0);
            Anim.SetBool("isGrounded", IsGrounded());
        }
    }

    public void OnLeftButtonDown()
    {
        if (_view.IsMine && _speed >= 0f)
            _speed = -_normalSpeed;
    }

    public void OnRightButtonDown()
    {
        if (_view.IsMine && _speed <= 0f)
            _speed = _normalSpeed;
    }

    public void OnButtonUp()
    {
        if (_view.IsMine)
            _speed = 0f; 
    }

    private void Move()
    {
        if (_view.IsMine)
        {
            _body.velocity = new Vector2(_speed, _body.velocity.y);

            if (_speed < 0)
                transform.localScale = new Vector3(-7, transform.localScale.y, transform.localScale.z);
            else if (_speed > 0)
                transform.localScale = new Vector3(7, transform.localScale.y, transform.localScale.z);
        }
    }

    public void Jump()
    {
        if (_view.IsMine)
        {
            if (IsGrounded() || _coyoteTimer < 0.2f)
            {
                if (_body.gravityScale > 0)
                    _body.velocity = new Vector2(_body.velocity.x, _jumpForce);
                else if (_body.gravityScale < 0)
                    _body.velocity = new Vector2(_body.velocity.x, -_jumpForce);
            }
            _coyoteTimer = 1f;
        }

    }

    public void Dash()
    {
        if (_view.IsMine)
        {
            if (transform.localScale.y > 0 && _dashCooldownTimer >= 1.5f)
            {
                Anim.SetTrigger("dash");
                _dashCooldownTimer = 0;
                _body.gravityScale = 2.5f;

                if (transform.localScale.x > 0)
                {
                    _body.AddForce(Vector2.right * _dashPowerX);
                    _body.AddForce(Vector2.up * _dashPowerY);
                }
                else
                {
                    _body.AddForce(Vector2.left * _dashPowerX);
                    _body.AddForce(Vector2.up * _dashPowerY);
                }
            }
            else if (_dashCooldownTimer >= 1.5f)
            {
                Anim.SetTrigger("dash");
                _dashCooldownTimer = 0;
                _body.gravityScale = 2.5f;

                if (transform.localScale.x > 0)
                {
                    _body.AddForce(Vector2.right * _dashPowerX);
                    _body.AddForce(Vector2.down * _dashPowerY);
                }
                else
                {
                    _body.AddForce(Vector2.left * _dashPowerX);
                    _body.AddForce(Vector2.down * _dashPowerY);
                }
            }
        }
    }

    private bool IsGrounded()
    {
        if (transform.localScale.y > 0)
        {
            RaycastHit2D raycastHit = Physics2D.BoxCast(_box.bounds.center, _box.bounds.size, 0, Vector2.down, 0.1f, _groundLayer);
            RaycastHit2D raycastHit1 = Physics2D.BoxCast(_box.bounds.center, _box.bounds.size, 0, Vector2.down, 0.1f, _platformLayer);

            if (raycastHit.collider == null)
            {
                return raycastHit1.collider != null;
            }
            return raycastHit.collider != null;
        }
        else
        {
            RaycastHit2D raycastHit = Physics2D.BoxCast(_box.bounds.center, _box.bounds.size, 0, Vector2.up, 0.1f, _groundLayer);
            RaycastHit2D raycastHit1 = Physics2D.BoxCast(_box.bounds.center, _box.bounds.size, 0, Vector2.up, 0.1f, _platformLayer);

            if (raycastHit.collider == null)
            {
                return raycastHit1.collider != null;
            }
            return raycastHit.collider != null;
        }
    }
}
