using UnityEngine;
using UnityEngine.SceneManagement;

public class Killzone: MonoBehaviour
{
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

    //private void OnCollisionEnter(Collision collision)
    //{
    //    if (collision.transform.CompareTag("Player"))
    //    {
    //        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    //    }
    //}

}
