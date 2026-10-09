using UnityEngine;
using UnityEngine.SceneManagement;

public class Killzone: MonoBehaviour
{
    //private void OnTriggerStay(Collider other)
    //{
    //    if (other.CompareTag("Player"))
    //    {
    //        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    //        Debug.Log("Prueba de muerte");
    //    }
    //}

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerLives playerLives = other.GetComponent<PlayerLives>();

            if (playerLives != null)
            {
                playerLives.LoseLife();
            }
            else
                Debug.Log("Falta el componente PlayerLives");
        }
    }
}
