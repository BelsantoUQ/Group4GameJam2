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
    private bool isMovingToDie;
    [SerializeField] private Animator animator;
    
    // Start is called before the first frame update
    private void Start()
    {
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        originalPosition = transform.position;
        destination = originalPosition;
        isMovingToPowerUp = false;
        isMovingToDie = false;
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
            animator.SetBool("Running", true);
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
        if (other.CompareTag("Enemy") && isMovingToDie)
        {
            StartCoroutine(DestroyAfterDelay());
        }
    }
    
    private void OnCollisionEnter(Collision other)
    {
        Debug.Log("Colision");
        if (other.gameObject.CompareTag("Enemy") && isMovingToDie)
        {
            animator.SetBool("Death", true);
            StartCoroutine(DestroyAfterDelay());
        }
    }

    private IEnumerator DestroyAfterDelay()
    {
        yield return new WaitForSeconds(1.3f); // Esperar 2 segundos
        Destroy(gameObject);
    }

    private IEnumerator MoveToPowerUp()
    {
        yield return new WaitForSeconds(2f); // Esperar 2 segundos
        destination = originalPosition;
    }

    public void SetDeath(GameObject enemy)
    {
        isMovingToDie = true;
        destination = enemy.transform.position;
    }
}
