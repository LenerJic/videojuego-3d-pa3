using UnityEngine;
using UnityEngine.InputSystem;

public class Jugador : MonoBehaviour
{
    public float speed = 5f;
    private Rigidbody rb;

    private Vector2 input;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Lo llama PlayerInput cuando cambia la acción "Move"
    public void OnMove(InputValue value) 
    {
        input = value.Get<Vector2>();
    }

    private void FixedUpdate()
    {
        Vector3 movement = new Vector3(
            input.x * speed,
            rb.linearVelocity.y,
            input.y * speed
        );
        
        rb.linearVelocity = movement;
    }


}
