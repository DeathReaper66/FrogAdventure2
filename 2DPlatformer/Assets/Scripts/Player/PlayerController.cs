using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    [Header("Params")]
    [SerializeField] private float _speed;
    [SerializeField] private float _jumpForce;
    private float _coyoteTimer;
    private float _swapGravityTimer;
    private float _horizontalInput;

    [Header("DashParams")]
    [SerializeField] float _dashPowerX;
    [SerializeField] float _dashPowerY;
    private float _dashCooldownTimer;

    [Header("OtherParams")]
    [SerializeField] private LayerMask _groundLayer;
    [SerializeField] private LayerMask _platformLayer;
    private Rigidbody2D _body;
    private BoxCollider2D _box;
    private Animator _anim;

    private void Awake()
    {
        _body = GetComponent<Rigidbody2D>();
        _box = GetComponent<BoxCollider2D>();
        _anim = GetComponent<Animator>();

        _swapGravityTimer = 0f;
        _dashCooldownTimer = 0f;
        _coyoteTimer = 0f;
    }

    private void Update()
    {
        Move();

        if (!IsGrounded())
            _coyoteTimer += Time.deltaTime;
        else
            _coyoteTimer = 0f;

        _anim.SetBool("moving", _horizontalInput != 0);

        _anim.SetBool("isGrounded", IsGrounded());

        Mechanics();

    }
    private void Mechanics()
    {

        if (Input.GetKey(KeyCode.Space))
            Jump();

        if (Input.GetKey(KeyCode.LeftShift))
            Sprint();

        _swapGravityTimer += Time.deltaTime;
        if (Input.GetKey(KeyCode.W) && _swapGravityTimer > 1f)
            SwapGravity();

        _dashCooldownTimer += Time.deltaTime;
        if (Input.GetKey(KeyCode.LeftControl) && _dashCooldownTimer > 1.5f)
        {
            Dash();
            _anim.SetTrigger("dash");
        }

        if (Input.GetKey(KeyCode.S))
            Physics2D.IgnoreLayerCollision(8, 7, true);
        else
            Physics2D.IgnoreLayerCollision(8, 7, false);
    }

    private void Move()
    {
        _horizontalInput = Input.GetAxis("Horizontal");

        if (_horizontalInput < 0)
            transform.localScale = new Vector3(-7, transform.localScale.y, transform.localScale.z);
        else if (_horizontalInput > 0)
            transform.localScale = new Vector3(7, transform.localScale.y, transform.localScale.z);

        _body.velocity = new Vector2(_horizontalInput * _speed, _body.velocity.y);
    }
    private void Jump()
    {
        if (IsGrounded() || _coyoteTimer < 0.5f)
        {
            if (_body.gravityScale > 0)
                _body.velocity = new Vector2(_body.velocity.x, _jumpForce);
            else if (_body.gravityScale < 0)
                _body.velocity = new Vector2(_body.velocity.x, -_jumpForce);
        }
            _coyoteTimer = 1f;
    }
    private void Sprint()
    {
        _body.velocity = new Vector2(_horizontalInput * _speed * 1.3f, _body.velocity.y);
    }
    private void SwapGravity()
    {
        _swapGravityTimer = 0;
        transform.localScale = new Vector3(transform.localScale.x, -transform.localScale.y, transform.localScale.z);
        _body.gravityScale *= -1;
    }
    private void Dash()
    {

        if (transform.localScale.y > 0)
        {
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
        else
        {
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

        _dashCooldownTimer = 0;
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
