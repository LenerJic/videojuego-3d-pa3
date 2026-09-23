using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.AI;



public class Enemigo : MonoBehaviour
{
    private UnityEngine.AI.NavMeshAgent navMeshAgent;
    private Transform playerTransform;

    void Start()
    {
        navMeshAgent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        playerTransform = FindAnyObjectByType<Jugador>().transform;
    }

    
    void Update()
    {
        navMeshAgent.destination = playerTransform.position;
    }



    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}
