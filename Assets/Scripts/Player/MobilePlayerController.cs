using UnityEngine;

public class MobilePlayerController : MonoBehaviour
{
    [Header("Params")]
    [SerializeField] private float _speed;
    [SerializeField] private float _jumpForce;
    [SerializeField] private bool _canSwapGravity;
    private float _coyoteTimer;
    private float _beforesprint;
    private float _swapGravityTimer;
    private float _mobileHorizontalInput;

    [Header("DashParams")]
    [SerializeField] float _dashPowerX;
    [SerializeField] float _dashPowerY;
    private float _dashCooldownTimer;

    [Header("OtherParams")]
    [SerializeField] private LayerMask _groundLayer;
    [SerializeField] private LayerMask _platformLayer;
    [SerializeField] private Joystick _joystick;
    private Rigidbody2D _body;
    private BoxCollider2D _box;
    public Animator Anim { get; private set; }

    private void Awake()
    {
        _body = GetComponent<Rigidbody2D>();
        Anim = GetComponent<Animator>();
        _box = GetComponent<BoxCollider2D>();

        _beforesprint = _speed;
        _swapGravityTimer = 0f;
        _dashCooldownTimer = 0f;
        _coyoteTimer = 0f;
    }

    private void FixedUpdate()
    {
        Move();

        _dashCooldownTimer += Time.deltaTime;

        if (!IsGrounded())
            _coyoteTimer += Time.deltaTime;
        else
            _coyoteTimer = 0f;

        _swapGravityTimer += Time.deltaTime;
        if (_joystick.Vertical >= 0.7f && _swapGravityTimer > 1f)
            SwapGravity();

        if (_joystick.Vertical <= -0.5f)
            Physics2D.IgnoreLayerCollision(8, 7, true);
        else
            Physics2D.IgnoreLayerCollision(8, 7, false);

        if (IsGrounded())
            _body.gravityScale = 2f;

        Anim.SetBool("moving", _mobileHorizontalInput != 0);
        Anim.SetBool("isGrounded", IsGrounded());
    }

    private void Move()
    {
        _mobileHorizontalInput = _joystick.Horizontal;

        if (_mobileHorizontalInput < 0)
            transform.localScale = new Vector3(-7, transform.localScale.y, transform.localScale.z);
        else if (_mobileHorizontalInput > 0)
            transform.localScale = new Vector3(7, transform.localScale.y, transform.localScale.z);

        if (_joystick.Horizontal >= 0.5f || _joystick.Horizontal <= 0.5f)
            _body.velocity = new Vector2(_mobileHorizontalInput * _speed, _body.velocity.y);
    }

    private void SwapGravity()
    {
        if (_canSwapGravity)
        {
            _swapGravityTimer = 0;
            transform.localScale = new Vector3(transform.localScale.x, -transform.localScale.y, transform.localScale.z);
            _body.gravityScale *= -1;
        }
    }

    public void Jump()
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

    public void Sprint()
    {
        if (_speed < 10f)
            _speed *= 1.3f;
        else
            _speed = _beforesprint;
    }

    public void Dash()
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

    public float GetSpeed()
    {
        return _speed;
    }
}
