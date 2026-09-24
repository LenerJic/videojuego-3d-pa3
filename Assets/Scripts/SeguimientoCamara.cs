using UnityEngine;

public class SeguimientoCamara : MonoBehaviour
{
    [Header("Objetivo a Seguir")]
    public Transform target;

    [Header("Configuración de Posición")]
    public Vector3 offset = new Vector3(-6f, 10f, -8f);
    public float tiempoSuavizado = 0.15f; // Tiempo de respuesta (entre 0.05f y 0.3f)

    private Vector3 velocidadActual = Vector3.zero;

    void Start()
    {
        // Si no asignas el objetivo manualmente, busca automáticamente al Jugador
        if (target == null)
        {
            Jugador jugador = FindAnyObjectByType<Jugador>();
            if (jugador != null)
            {
                target = jugador.transform;
            }
        }
    }

    void LateUpdate()
    {
        if (target == null) return;

        Vector3 posicionDeseada = target.position + offset;

        // SmoothDamp ajusta automáticamente la aceleración y desaceleración sin tirones
        transform.position = Vector3.SmoothDamp(transform.position, posicionDeseada, ref velocidadActual, tiempoSuavizado);

        // Mantiene la cámara mirando directamente a la esfera
        transform.LookAt(target);
    }
}