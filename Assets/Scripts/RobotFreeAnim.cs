using UnityEngine;

public class RobotController : MonoBehaviour
{
    private Animator anim;
    private Rigidbody playerRb;

    void Awake()
    {
        anim = GetComponent<Animator>();
        playerRb = GetComponentInParent<Rigidbody>();
    }

    void Update()
    {
        if (anim == null || playerRb == null) return;

        Vector3 horizontalVelocity = new Vector3(playerRb.linearVelocity.x, 0f, playerRb.linearVelocity.z);
        bool estaMoviendose = horizontalVelocity.magnitude > 0.1f;

        anim.SetBool("Roll_Anim", estaMoviendose);
        anim.SetBool("Open_Anim", !estaMoviendose);

        if (estaMoviendose)
        {
            Quaternion targetRotation = Quaternion.LookRotation(horizontalVelocity);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, 15f * Time.deltaTime);
        }
    }
}