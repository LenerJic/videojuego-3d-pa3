using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.AI;



public class Enemigo : MonoBehaviour
{
    private UnityEngine.AI.NavMeshAgent navMeshAgent;
    private Transform playerTransform;

    private Vector3 startPosition;
    private Quaternion startRotation;

    private bool gameOver = false;

    void Start()
    {
        navMeshAgent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        playerTransform = FindAnyObjectByType<Jugador>().transform;

        startPosition = transform.position;
        startRotation = transform.rotation;
    }


    void Update()
    {
        if (gameOver) return;

        navMeshAgent.destination = playerTransform.position;
    }

    public void ResetEnemyPosition()
    {
        navMeshAgent.isStopped = true;
        navMeshAgent.ResetPath();
        navMeshAgent.Warp(startPosition);
        transform.rotation = startRotation;

        navMeshAgent.isStopped = false;
    }

    public void SetGameOver()
    {
        gameOver = true;

        navMeshAgent.isStopped = true;
        navMeshAgent.ResetPath();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            PlayerLives playerLives = collision.gameObject.GetComponent<PlayerLives>();

            if (playerLives != null)
            {
                playerLives.LoseLife();
                ResetEnemyPosition();
            }
        }
    }
}
