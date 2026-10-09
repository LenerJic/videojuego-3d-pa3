using UnityEngine;
using TMPro; // Necesario para controlar TextMeshPro

public class Portal : MonoBehaviour
{
    [Header("Referencias Visuales")]
    public GameObject puertaMadera;
    public GameObject luzPortal;

    [Header("Audio")]
    public AudioSource audioSource;
    public AudioClip sonidoApertura;   // Sonido al recolectar todas las monedas
    public AudioClip sonidoAbsorcion;  // Sonido cuando la esfera entra a la puerta

    [Header("Mensaje UI en Pantalla")]
    public TMP_Text mensajePortalText;
    public string mensajeApertura = "¡EL PORTAL SE HA ABIERTO!";
    public float duracionMensaje = 3f;

    [Header("Configuración de Iluminación")]
    public Color colorLuzActivada = Color.lightPink;

    [Header("Configuración de Puerta")]
    public Color colorPuertaActivada = Color.black;

    private Collider triggerVictoria;
    private bool estaAbierto = false;
    private bool cambiandoEscena = false; // Controla que no se ejecute dos veces

    void Start()
    {
        // Obtiene exclusivamente el BoxCollider del Portal 
        triggerVictoria = GetComponent<BoxCollider>();

        if (triggerVictoria != null)
        {
            triggerVictoria.enabled = false;
        }

        if (luzPortal != null)
        {
            luzPortal.SetActive(false); // Luz apagada al inicio
        }

        if (audioSource == null)
        {
            audioSource = GetComponent<AudioSource>();
        }

        if (mensajePortalText != null)
        {
            mensajePortalText.gameObject.SetActive(false);
        }
    }

    public void ActivarPortal()
    {
        if (estaAbierto) return;
        estaAbierto = true;

        // Mostrar mensaje en pantalla
        if (mensajePortalText != null)
        {
            mensajePortalText.text = mensajeApertura;
            mensajePortalText.gameObject.SetActive(true);
            Invoke("OcultarMensaje", duracionMensaje); // Lo oculta tras X segundos
        }

        // Reproducir el sonido de apertura
        if (audioSource != null && sonidoApertura != null)
        {
            audioSource.PlayOneShot(sonidoApertura);
        }

        // Encender luz
        if (luzPortal != null)
        {
            Light luzComp = luzPortal.GetComponent<Light>();
            if (luzComp != null)
            {
                luzComp.color = colorLuzActivada;
            }
            luzPortal.SetActive(true);
        }

        // Cambiar apariencia de la puerta a negro y quitar collider
        if (puertaMadera != null)
        {
            Renderer rend = puertaMadera.GetComponent<Renderer>();
            if (rend != null)
            {
                rend.material.mainTexture = null;
                rend.material.color = colorPuertaActivada;
            }

            Collider colMadera = puertaMadera.GetComponent<Collider>();
            if (colMadera != null)
            {
                colMadera.enabled = false;
            }
        }

        if (triggerVictoria != null)
        {
            triggerVictoria.enabled = true;
        }
    }

    private void OcultarMensaje()
    {
        if (mensajePortalText != null)
        {
            mensajePortalText.gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (estaAbierto && !cambiandoEscena && (other.CompareTag("Player") || other.GetComponent<Jugador>() != null))
        {
            cambiandoEscena = true;

            // 1. Reproducir el sonido de absorción
            if (audioSource != null && sonidoAbsorcion != null)
            {
                audioSource.PlayOneShot(sonidoAbsorcion);
            }

            MeshRenderer rendJugador = other.GetComponent<MeshRenderer>();
            if (rendJugador != null)
            {
                rendJugador.enabled = false;
            }

 
            Rigidbody rbJugador = other.GetComponent<Rigidbody>();
            if (rbJugador != null)
            {
                rbJugador.linearVelocity = Vector3.zero;
            }

            Invoke("CompletarVictoria", 1.0f);
        }
    }

    private void CompletarVictoria()
    {
        GameManager gm = FindAnyObjectByType<GameManager>();
        if (gm != null)
        {
            gm.GanarJuego(); // Carga la pantalla/escena de victoria
        }
    }
}