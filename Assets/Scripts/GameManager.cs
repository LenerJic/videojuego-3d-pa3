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