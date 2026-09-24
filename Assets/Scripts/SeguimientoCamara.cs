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
        // Si no asignas el objetivo manualmente, busca automáticamente al Jugador en la escena
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

        Vector3 posicionSuave = Vector3.Lerp(transform.position, posicionDeseada, velocidadSuave * Time.deltaTime);
        transform.position = posicionSuave;

        // Mantiene la cámara mirando directamente a la esfera
        transform.LookAt(target);
    }
}
