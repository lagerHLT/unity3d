using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour
{
    public float jumpForce = 20f;
    public float movementSpeed = 10f;

    private bool _onGround = false;
    private bool _isMovingRight = true;
    private Vector2 _velocity;
    private bool _jumpTriggered = false;
    private Rigidbody2D _playerRigidBody2D;
    private Transform _groundCheck, _wallCheckRight, _wallCheckLeft;
    private float collisionRadius = .01f;
    [SerializeField] private LayerMask whatIsGround;
    [SerializeField] private LayerMask whatIsWall;

    public void Start(){
        _groundCheck = transform.Find("GroundCheck");
        _wallCheckRight = transform.Find("WallCheckRight");
        _wallCheckLeft = transform.Find("WallCheckLeft");
        _playerRigidBody2D = GetComponent<Rigidbody2D>();
        _velocity = new Vector2(movementSpeed, 0);
    }
    public void Update(){
        if (!_jumpTriggered && _onGround) {
            _jumpTriggered = Input.GetMouseButton(0);//Input.GetKey(KeyCode.Space);
            if (_jumpTriggered)
                Debug.Log("Jump triggered");
        }
        //Alternate code for touchscreen
        /*var fingerCount = 0;
        for (var touch : Touch in Input.touches) {
            if (touch.phase != TouchPhase.Ended && touch.phase != TouchPhase.Canceled)
                fingerCount++;
        }
        if (fingerCount > 0)
            print("User has " + fingerCount + " finger(s) touching the screen");*/
    }
    public void FixedUpdate() {
        if (_jumpTriggered) {
            Jump();
            _onGround = false;
            _jumpTriggered = false;
            Debug.Log("Jump removed");
        }
        else
            _onGround = Physics2D.OverlapCircle(_groundCheck.position, collisionRadius, whatIsGround);

        var isCollidingWall = _isMovingRight ? Physics2D.OverlapCircle(_wallCheckRight.position, collisionRadius, whatIsWall) : Physics2D.OverlapCircle(_wallCheckLeft.position, collisionRadius, whatIsWall);
        if (isCollidingWall) {
            _velocity.x = -_velocity.x;
            _isMovingRight = !_isMovingRight;
        }
        Move();
    }
    private void Jump() {
        _playerRigidBody2D.AddForce(new Vector2(0, jumpForce));
    }
    private void Move() {
        transform.Translate(_velocity * movementSpeed * Time.deltaTime);
    }
}
