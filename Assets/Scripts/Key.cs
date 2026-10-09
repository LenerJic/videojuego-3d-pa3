using UnityEngine;
using UnityEngine.SceneManagement;

public class Key : MonoBehaviour
{
    public string playerTag = "Player";
    [SerializeField] private AudioClip sonidoLlave;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(playerTag))
        {
            CargarSiguienteNivel();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag(playerTag))
        {
            CargarSiguienteNivel();
        }
    }

    private void CargarSiguienteNivel()
    {
        // Llama al reproductor persistente
        ReproductorSonido.Reproducir(sonidoLlave, transform.position);

        // Carga la escena inmediatamente
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}