using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.UI.Image;

public class Jugador : MonoBehaviour
{
    public float gravityMultiplier = 3f;

    [Header("Movimiento")]
    public float speed = 10f;
    public float acceleration = 40f;

    [Header("Salto")]
    public float jumpHeight = 10f;
    public float groundCheckDistance = 0.15f;

    private Rigidbody rb;
    private Vector2 moveInput;
    private bool jumpPressed;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();

        if (rb == null) Debug.LogError("El Player necesita un Rigidbody.");

        rb.useGravity = false;
    }

    private void FixedUpdate()
    {
        rb.AddForce(
            Physics.gravity * gravityMultiplier,
            ForceMode.Acceleration
        );

        Vector3 direction = new Vector3(
            moveInput.x,
            0f,
            moveInput.y
        );

        if (direction.magnitude > 1f)
            direction.Normalize();

        Vector3 targetVelocity = direction * speed;

        // Conservamos la velocidad vertical
        targetVelocity.y = rb.linearVelocity.y;

        rb.linearVelocity = Vector3.MoveTowards(
            rb.linearVelocity,
            targetVelocity,
            acceleration * Time.fixedDeltaTime
        );


        if (jumpPressed && IsGrounded())
        {
            float jumpVelocity = Mathf.Sqrt(
                jumpHeight * -2f * Physics.gravity.y
            );

            Vector3 velocity = rb.linearVelocity;
            velocity.y = jumpVelocity;

            rb.linearVelocity = velocity;

            jumpPressed = false;
        }
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

    private bool IsGrounded()
    {
        Vector3 checkPosition = transform.position + Vector3.down * 0.55f;

        return Physics.CheckSphere(
            checkPosition,
            groundCheckDistance
        );
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;

        Vector3 checkPosition = transform.position + Vector3.down * 0.55f;

        Gizmos.DrawWireSphere(
            checkPosition,
            groundCheckDistance
        );
    }
}
