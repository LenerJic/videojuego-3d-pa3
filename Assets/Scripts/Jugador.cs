using UnityEngine;
using UnityEngine.InputSystem;

public class Jugador : MonoBehaviour
{
    public float gravityMultiplier = 3f;

    [Header("Movimiento")]
    public float speed = 10f;
    public float acceleration = 40f;

    [Header("Salto")]
    public float jumpForce = 15f;

    private Rigidbody rb;
    private Vector2 moveInput;
    private bool jumpPressed;
    private bool isGrounded;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.useGravity = false;
    }

    private void FixedUpdate()
    {
        // Gravedad
        rb.AddForce(
            Physics.gravity * gravityMultiplier,
            ForceMode.Acceleration
        );

        // Movimiento
        Vector3 direction = new Vector3(
            moveInput.x,
            0f,
            moveInput.y
        );

        if (direction.magnitude > 1f)
            direction.Normalize();

        Vector3 targetVelocity = direction * speed;
        targetVelocity.y = rb.linearVelocity.y;

        rb.linearVelocity = Vector3.MoveTowards(
            rb.linearVelocity,
            targetVelocity,
            acceleration * Time.fixedDeltaTime
        );

        // Salto
        if (jumpPressed && isGrounded)
        {
            rb.linearVelocity = new Vector3(
                rb.linearVelocity.x,
                jumpForce,
                rb.linearVelocity.z
            );

            isGrounded = false;
        }

        jumpPressed = false;
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

    private void OnCollisionStay(Collision collision)
    {
        foreach (ContactPoint contact in collision.contacts)
        {
            if (contact.normal.y > 0.5f)
            {
                isGrounded = true;
                return;
            }
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        isGrounded = false;
    }
}
