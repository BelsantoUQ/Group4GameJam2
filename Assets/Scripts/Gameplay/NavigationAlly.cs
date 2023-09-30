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
    private bool isMovingToPowerUp;
    [SerializeField] private Animator animator;
    
    // Start is called before the first frame update
    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        originalPosition = transform.position;
        destination = originalPosition;
        isMovingToPowerUp = false;
    }
    
    // Update is called once per frame
    private void Update()
    {
        
        agent.destination = destination;
        if (Input.GetMouseButtonDown(1) && !isMovingToPowerUp) // Verificar si se presionó el botón derecho del mouse
        {
            isMovingToPowerUp = true;
            powerUp = GameObject.FindGameObjectWithTag("Powerup").transform;
            destination = powerUp.position;
        }
        if (isMovingToPowerUp)
        {
            if (agent.remainingDistance <= agent.stoppingDistance)
            {
                isMovingToPowerUp = false;
            }
        }
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
