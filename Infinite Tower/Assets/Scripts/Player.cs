using UnityEngine;

[RequireComponent(typeof(Controller2D))]
public class Player : MonoBehaviour
{
    public float jumpHeight = 4;
    public float timeToJumpApex = .4f;

    float accelerationTimeAirborne = .2f;
    float accelerationTimeGrounded = .1f;
    float moveSpeed = 6;
    float gravityNormal, gravityFloat;
    float jumpVelocity;
    float velocityXSmoothing;
    bool goingDown = false;
    Vector3 velocity;
    Controller2D controller;

    void Start() {
        controller = GetComponent<Controller2D>();
        gravityNormal = -(2 * jumpHeight) / Mathf.Pow(timeToJumpApex, 2);
        gravityFloat = gravityNormal / 3;
        jumpVelocity = Mathf.Abs(gravityNormal) * timeToJumpApex;
    }

    void Update() {
        if (controller.collisions.above || controller.collisions.below)
            velocity.y = 0;

        Vector2 input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        //input.x = 1; //constant movement forward
        if (Input.GetKeyDown(KeyCode.Space) && controller.collisions.below)
            velocity.y = jumpVelocity;

        float targetVelocityX = input.x * moveSpeed;
        velocity.x = Mathf.SmoothDamp(velocity.x, targetVelocityX, ref velocityXSmoothing, (controller.collisions.below) ? accelerationTimeGrounded : accelerationTimeAirborne);
        if (!goingDown)
            velocity.y += gravityNormal * Time.deltaTime;
        else
            velocity.y += gravityFloat * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }
}