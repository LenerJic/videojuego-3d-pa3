using UnityEngine;
using UnityEngine.InputSystem;

public class Jugador : MonoBehaviour
{
    public float speed = 10f;
    public float gravity = -30f;
    public float jumpHeight = 5f;

    private CharacterController controller;
    private Vector2 moveInput;
    private float verticalVelocity;
    private bool jumpPressed;

    private void Start()
    {
        controller = GetComponent<CharacterController>();
    }
    private void Update()
    {
        bool isGrounded = controller.isGrounded;
        if (isGrounded && verticalVelocity <0)
        {
            verticalVelocity = -2f;
        }

        if (jumpPressed && isGrounded)
        {
            verticalVelocity = Mathf.Sqrt(jumpHeight * -2f * gravity);
            jumpPressed = false;
        }

        verticalVelocity += gravity * Time.deltaTime;

        Vector3 move = new Vector3(moveInput.x, 0f, moveInput.y).normalized;
        move.y = verticalVelocity / speed;

        controller.Move(move * speed * Time.deltaTime);
    }

    public void OnMove(InputValue value) 
    {
        moveInput = value.Get<Vector2>();
    }

    public void OnJump(InputValue value)
    {
        if (value.isPressed)
        {
            jumpPressed = true;
        }
    }

}
