using UnityEngine;

public class Collection : MonoBehaviour
{
    [Header("Efectos Visuales")]
    public GameObject efectoParticulasPrefab; 

    private void OnTriggerEnter(Collider other)
    {
        Recolectar(other.gameObject);
    }

    // Se activa en los niveles donde la moneda tiene físicas normales y gravedad
    private void OnCollisionEnter(Collision collision)
    {
        Recolectar(collision.gameObject);
    }

    private void Recolectar(GameObject objetoQueToco)
    {
        if (objetoQueToco.CompareTag("Player") || objetoQueToco.GetComponent<Jugador>() != null)
        {
            // Instanciar las partículas en la posición de la moneda
            if (efectoParticulasPrefab != null)
            {
                Instantiate(efectoParticulasPrefab, transform.position, Quaternion.identity);
            }

            // Notifica al GameManager para el sonido
            FindAnyObjectByType<GameManager>()?.AddCollection();

            // Elimina la moneda
            Destroy(gameObject);
        }
    }
}
