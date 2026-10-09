using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverButtons : MonoBehaviour
{
    public void Retry()
    {
        SceneManager.LoadScene("Nivel1");
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
