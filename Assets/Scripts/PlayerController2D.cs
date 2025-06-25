using UnityEngine;
using UnityEngine.Serialization;

[RequireComponent(typeof(Rigidbody2D), typeof(Collider2D))]
public class PlayerController2D : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float speed=5f;
    [SerializeField] private float jumpForce=12f;
    [Header("Ground Check")]
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private float groundCheckRadius;
    [FormerlySerializedAs("_groundCheckPoint")] [SerializeField] private Transform groundCheckPoint;
    [Header("Jump")]
    [SerializeField] private float fallMultiplier = 2.5f;
    [SerializeField] private float lowJumpMultiplier = 5f;
    [SerializeField] private float coyoteTime = 0.2f;
    [SerializeField] private float jumpBufferTime = 0.1f;
    
    private Rigidbody2D _rb;
    private bool _isGrounded;
    private float _horizontalImput;
    private float _jumpBufferCounter;
    private float _coyoteCounter;
    private bool _jumpHeld;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }
    

   
    void Update()
    {
        ReadImput();
        HandleJump();
    }

    void FixedUpdate()
    {
        CheckGround();
        ApplyJump();
        HandleImput();
        BetterJumpPhysics();
    }
    
    private void ReadImput()
    {
        _horizontalImput = Input.GetAxis("Horizontal");
    }

    private void HandleImput()
    {
        Vector2 vel = _rb.linearVelocity;
        vel.x = _horizontalImput*speed;
        _rb.linearVelocity = vel;
    }

    private void ApplyJump()
    {
        if (_jumpBufferCounter > 0 && _coyoteCounter > 0)
        {
            _rb.linearVelocity = new Vector2(_rb.linearVelocity.x, jumpForce);
            _jumpBufferCounter = 0;
            _coyoteCounter = 0;
        }
    }
    private void HandleJump()
    {
        if (_isGrounded) _coyoteCounter=coyoteTime;
        else _coyoteCounter-= Time.deltaTime;
        if (Input.GetButtonDown("Jump"))
        {
            _jumpBufferCounter=jumpBufferTime;
            Debug.Log("Jump pressed");
        }      
        else 
            _jumpBufferCounter-=Time.deltaTime;
        _jumpHeld=Input.GetButton("Jump");
    }

    private void BetterJumpPhysics()
    {
        if (_rb.linearVelocity.y < 0)
        {
            _rb.linearVelocity += Vector2.up * Physics2D.gravity.y * ((fallMultiplier-1f) * Time.fixedDeltaTime);
        }
        else if (_rb.linearVelocity.y > 0 && !_jumpHeld)
        {
            _rb.linearVelocity += Vector2.up * Physics2D.gravity.y * ((lowJumpMultiplier-1f) * Time.fixedDeltaTime);
        }
            
    }
    private void CheckGround()
    {
        _isGrounded=Physics2D.OverlapCircle(groundCheckPoint.position, groundCheckRadius, groundLayer);
        Debug.Log("Ground Check Distance: " + _isGrounded);
    }

    private void OnDrawGizmos()
    {
        if (groundCheckPoint == null)
            return;
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(groundCheckPoint.position, groundCheckRadius);
    }
}
