using UnityEngine;

public class Collection : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
      
            FindAnyObjectByType<GameManager>().AddCollection();

            Destroy(gameObject);
        }
    }
}
