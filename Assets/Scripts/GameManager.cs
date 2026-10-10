using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public AudioSource audioSource;
    public TMP_Text collectionNumberText;
    private int collectionNumber;
    public TMP_Text totalCollectionNumberText;
    private int totalCollectionNumber;

    [Header("UI de Contador a Ocultar")]
    public GameObject UIContadorMonedas;

    [Header("Configuración de la Llave")]
    public GameObject keyPrefab;
    public Transform[] keySpawnPoints; 
    private bool keySpawned = false;

    [Header("Mensaje en Pantalla")] // Texto o panel de la UI para mostrar los mensajes al jugador
    public TMP_Text subtextMessage; 
    [TextArea] public string mensajeLlave = "¡Llave activada! Encuéntrala para escapar.";

    private void Start()
    {
        totalCollectionNumber = transform.childCount; 
        if (totalCollectionNumberText != null)
        {
            totalCollectionNumberText.text = totalCollectionNumber.ToString(); 
        }
    }

    public void AddCollection()
    {
        if (audioSource != null) audioSource.Play();

        collectionNumber++;

        if (collectionNumberText != null)
        {
            collectionNumberText.text = collectionNumber.ToString();
        }

        // Al recolectar la última moneda:
        if (collectionNumber >= totalCollectionNumber)
        {
            Portal portal = FindAnyObjectByType<Portal>();

            // Prioridad 1: Si hay Portal en la escena (Nivel 2)
            if (portal != null)
            {
                portal.ActivarPortal();
            }
            // Prioridad 2: Si no hay Portal pero sí Llave (Nivel 1)
            else if (keyPrefab != null && !keySpawned)
            {
                SpawnKey();
            }
            else
            {
                GanarJuego();
            }
        }
    }

    private void SpawnKey()
    {
        keySpawned = true;

        if (keySpawnPoints != null && keySpawnPoints.Length > 0)
        {
            int randomIndex = Random.Range(0, keySpawnPoints.Length);
            Transform selectedPoint = keySpawnPoints[randomIndex];
            Instantiate(keyPrefab, selectedPoint.position, selectedPoint.rotation);
        }

        if (subtextMessage != null)
        {
            subtextMessage.text = mensajeLlave;
            subtextMessage.gameObject.SetActive(true);
        }

        if (UIContadorMonedas != null)
        {
            UIContadorMonedas.SetActive(false);
        }
    }

    public void GanarJuego()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}