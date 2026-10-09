using UnityEngine;

public class PlayerLives : MonoBehaviour
{
    [Header("Lives")]
    public int maxLives = 3;

    private int currentLives;
    private Vector3 startPosition;
    private Jugador jugador;
    private Enemigo enemigo;
    public GameOverUI gameOverUI;
    public LivesUI livesUI;

    private void Start()
    {
        jugador = GetComponent<Jugador>();
        enemigo = FindAnyObjectByType<Enemigo>();

        currentLives = maxLives;
        startPosition = transform.position;

        if (livesUI != null)
        {
            livesUI.UpdateHearts(currentLives);
        }

        Debug.Log("Vidas: " + currentLives);
    }

    public void LoseLife()
    {
        currentLives--;

        currentLives = Mathf.Max(currentLives, 0);

        if (livesUI != null)
        {
            livesUI.UpdateHearts(currentLives);
        }

        Debug.Log("Vidas restantes: " + currentLives);

        if (currentLives <= 0) 
        {
            GameOver();
        }
        else {
            Respawn();

            if (enemigo != null) enemigo.ResetEnemyPosition();
        }
        ;
    }

    private void Respawn()
    {
        Rigidbody rb = GetComponent<Rigidbody>();

        if (rb != null)
        {
            rb.position = startPosition;
            rb.linearVelocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
        }
        else
        {
            transform.position = startPosition;
        }
    }

    private void GameOver()
    {
        jugador.SetGameOver();

        if (enemigo != null)
        {
            enemigo.SetGameOver();
        }

        gameOverUI.ShowGameOver();
    }

    public int GetLives()
    {
        return currentLives;
    }
}
