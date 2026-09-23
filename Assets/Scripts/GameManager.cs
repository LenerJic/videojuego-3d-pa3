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
        totalCollectionNumberText.text = totalCollectionNumber.ToString();  
    }

    private void Update()
    {
        if (transform.childCount <= 0)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);

        }
    }

    public void AddCollection()
    {
        audioSource.Play();
        collectionNumber++;
        collectionNumberText.text = collectionNumber.ToString();

    }
}
