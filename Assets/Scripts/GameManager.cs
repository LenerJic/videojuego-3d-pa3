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

    private void Update()
    {
        if (transform.childCount <= 0 && !keySpawned)
        {
            SpawnKey();

        // Si NO hay script Portal en la escena, cambia de nivel automáticamente al recolectar todo
        if (FindAnyObjectByType<Portal>() == null)
        {
            if (transform.childCount <= 0) 
            {
                GanarJuego();
            }
        }
    }

    public void AddCollection()
    {
        audioSource.Play();
        collectionNumber++;
        collectionNumberText.text = collectionNumber.ToString();
    }

    private void SpawnKey()
    {
        keySpawned = true; 

        if (keyPrefab == null)
        {
            Debug.LogError("¡No has asignado el Prefab de la Llave en el Inspector!");
            return;
        }

        if (keySpawnPoints == null || keySpawnPoints.Length == 0)
        {
            Debug.LogError("¡No has asignado Puntos de Spawn para la llave en el Inspector!");
            return;
        }

       // llave en un punto aleatorio
        int randomIndex = Random.Range(0, keySpawnPoints.Length);
        Transform selectedPoint = keySpawnPoints[randomIndex];
        Instantiate(keyPrefab, selectedPoint.position, selectedPoint.rotation);

        // Mostrar el mensaje en la UI
        if (subtextMessage != null)
        {
            subtextMessage.text = mensajeLlave;
            subtextMessage.gameObject.SetActive(true); // Asegura que el mensaje sea visible
        }

        // Ocultar el contador de monedas
        if (UIContadorMonedas != null)
        {
            UIContadorMonedas.SetActive(false);
        }

        Debug.Log($"¡Todas las monedas recolectadas! Llave aparecida en: {selectedPoint.name}");
        if (audioSource != null) audioSource.Play(); 

        collectionNumber++; 
        if (collectionNumberText != null)
        {
            collectionNumberText.text = collectionNumber.ToString(); 
        }

        //  Si SÍ existe el portal en la escena, lo activa al llegar al total de monedas
        Portal portal = FindAnyObjectByType<Portal>();
        if (portal != null && collectionNumber >= totalCollectionNumber)
        {
            portal.ActivarPortal();
        }
    }

    public void GanarJuego()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}