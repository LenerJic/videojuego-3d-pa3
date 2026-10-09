using UnityEngine;

public class LivesUI : MonoBehaviour
{
    [Header("Corazones")]
    public GameObject[] hearts;

    public void UpdateHearts(int currentLives)
    {
        for (int i = 0; i < hearts.Length; i++)
        {
            hearts[i].SetActive(i < currentLives);
        }
    }
}
