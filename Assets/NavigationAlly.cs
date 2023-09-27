using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavigationAlly : MonoBehaviour
{
    private Transform powerUp;
    private NavMeshAgent agent;
    private Vector3 destination;
    private Vector3 originalPosition;
    // Start is called before the first frame update
    private void Start()
    {
        powerUp = GameObject.FindGameObjectWithTag("Powerup").transform;
        agent = GetComponent<NavMeshAgent>();
        destination = powerUp.position;
        originalPosition = transform.position;
    }
    
    // Update is called once per frame
    private void Update()
    {
        agent.destination = destination;
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Powerup"))
        {
            StartCoroutine(MoveToPowerUp());
        }
    }

    private IEnumerator MoveToPowerUp()
    {
        yield return new WaitForSeconds(2f); // Esperar 2 segundos

        destination = originalPosition;
    }
}
