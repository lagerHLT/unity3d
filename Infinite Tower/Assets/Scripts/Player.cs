using UnityEngine;
using System.Collections;


public class Player : MonoBehaviour
{
    public float jumpForce = 100f;
    public float movementSpeed = 10f;

    private float _movementSpeed { get { return this.movementSpeed * 0.2f; } }
    private bool _onGround = false;
    private int _isMovingRight = 1;
    //private Vector2 _velocity;
    private bool _jumpTriggered = false;
    private Rigidbody2D _playerRigidBody2D;
    private Transform _groundCheck, _wallCheckRight, _wallCheckLeft;
    private float collisionRadius = .02f;
    [SerializeField] public LayerMask whatIsGround;
    [SerializeField] public LayerMask whatIsWall;

    public void Start(){
        _groundCheck = transform.Find("GroundCheck");
        _wallCheckRight = transform.Find("WallCheckRight");
        _wallCheckLeft = transform.Find("WallCheckLeft");
        _playerRigidBody2D = GetComponent<Rigidbody2D>();
        //_velocity = new Vector2(movementSpeed, 0);
    }
    public void Update() {
        if (!_jumpTriggered && _onGround)
            _jumpTriggered = Input.GetMouseButton(0);//Input.GetKeyDown(KeyCode.Space);//
    }
    public void FixedUpdate() {
        //_velocity.x = _movementSpeed;
        if (_jumpTriggered) {
            _onGround = false;
            _jumpTriggered = false;
            Jump();
        }
        else
            _onGround = Physics2D.OverlapCircle(_groundCheck.position, collisionRadius, whatIsGround);
            //_onGround = Physics2D.CircleCast(_groundCheck.position, collisionRadius, whatIsGround);

            var isCollidingWall = (_isMovingRight == 1) ? Physics2D.OverlapCircle(_wallCheckRight.position, collisionRadius, whatIsWall) : Physics2D.OverlapCircle(_wallCheckLeft.position, collisionRadius, whatIsWall);
        if (isCollidingWall)
            _isMovingRight *= -1;
        Move();
    }
    private void Jump() {
        _playerRigidBody2D.AddForce(new Vector2(0, jumpForce));
    }
    private void Move() {
        _playerRigidBody2D.velocity = new Vector2(movementSpeed * _isMovingRight, _playerRigidBody2D.velocity.y);
    }
}
