using UnityEngine;

public class SeguimientoCamara : MonoBehaviour
{
    [Header("Objetivo a Seguir")]
    public Transform target;

    [Header("Configuración de Posición")]
    public Vector3 offset = new Vector3(-6f, 10f, -8f);
    public float velocidadSuave = 5f;

    void Start()
    {
        // Si no asignas el objetivo manualmente en el Inspector, busca al Jugador al iniciar
        if (target == null)
        {
            Jugador jugador = FindAnyObjectByType<Jugador>();
            if (jugador != null)
            {
                target = jugador.transform;
            }
        }
    }

    // Se usa FixedUpdate para que el movimiento de la cámara vaya a la par con la física del Rigidbody
    void FixedUpdate()
    {
        if (target == null) return;

        // Posición a la que debe llegar la cámara
        Vector3 posicionDeseada = target.position + offset;

        // Interpolación fluida que evita tirones y desincronizaciones de FPS
        transform.position = Vector3.Lerp(transform.position, posicionDeseada, velocidadSuave * Time.fixedDeltaTime);
    }
}