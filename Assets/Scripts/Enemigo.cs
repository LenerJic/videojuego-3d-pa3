using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.AI;

public class Enemigo : MonoBehaviour
{
    private NavMeshAgent navMeshAgent;

    [Header("Referencia del Jugador")]
    public Transform playerTransform;

    [Header("Configuracion de Zona")]
    [Tooltip("Distancia para activar la persecucion por primera vez")]
    public float distanciaDeteccion = 18f;

    [Tooltip("Distancia maxima desde su punto de origen para seguir persiguiendo")]
    public float radioMaximoZona = 20f;

    private Vector3 posicionInicial;
    private bool estaPersiguiendo = false;

    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();

        // Guarda el puesto original donde lo colocaste
        posicionInicial = transform.position;

        if (playerTransform == null)
        {
            Jugador jugadorScript = FindAnyObjectByType<Jugador>();
            if (jugadorScript != null) playerTransform = jugadorScript.transform;
            else if (GameObject.FindWithTag("Player") != null) playerTransform = GameObject.FindWithTag("Player").transform;
        }
    }

    void Update()
    {
        if (playerTransform == null) return;

        float distanciaAlJugador = Vector3.Distance(transform.position, playerTransform.position);
        float distanciaDelJugadorAlOrigen = Vector3.Distance(posicionInicial, playerTransform.position);

        // 1. Activar persecución si el jugador entra dentro del radio de visión
        if (!estaPersiguiendo && distanciaAlJugador <= distanciaDeteccion)
        {
            estaPersiguiendo = true;
        }

        // 2. Si el jugador cruzó a la otra isla o se alejó demasiado de su puesto, PERDER PERSECUCIÓN
        if (estaPersiguiendo && distanciaDelJugadorAlOrigen > radioMaximoZona)
        {
            estaPersiguiendo = false; // Regresa a estado pasivo
        }

        // 3. Comportamiento en base al estado
        if (navMeshAgent != null && navMeshAgent.isOnNavMesh)
        {
            if (estaPersiguiendo)
            {
                navMeshAgent.destination = playerTransform.position;
            }
            else
            {
                // Regresar a su puesto original en lugar de campear el borde
                navMeshAgent.destination = posicionInicial;
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("Player") || collision.gameObject.GetComponent<Jugador>() != null)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, distanciaDeteccion);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radioMaximoZona);
    }
}